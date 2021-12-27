using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Cmdlets.WorkItem;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using WebApiBoard = Microsoft.TeamFoundation.Work.WebApi.Board;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class SetWorkItemController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        public override IEnumerable<WebApiWorkItem> Invoke()
        {
            var wi = Data.GetItem<WebApiWorkItem>();
            var tpc = Data.GetCollection();
            var fields = Parameters.Get<Hashtable>(nameof(SetWorkItem.Fields), new Hashtable());
            var bypassRules = Parameters.Get<bool>(nameof(SetWorkItem.BypassRules));
            var boardColumn = Parameters.Get<string>(nameof(SetWorkItem.BoardColumn));
            var boardColumnDone = Parameters.Get<bool>(nameof(SetWorkItem.BoardColumnDone));
            var boardLane = Parameters.Get<string>(nameof(SetWorkItem.BoardLane));

            foreach (var argName in Parameters.Keys.Where(f => Parameters.HasParameter(f) && !fields.ContainsKey(f)))
            {
                fields.Add(Parameters[argName], GetFieldValue(argName, (string)wi.Fields["System.TeamProject"]));
            }

            if (fields.Keys.Count > 0)
            {
                var patch = new JsonPatchDocument(){
                            new JsonPatchOperation() {
                                Operation = Operation.Test,
                                Path = "/rev",
                                Value = wi.Rev
                            }
                        };

                foreach (DictionaryEntry field in fields)
                {
                    patch.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = $"/fields/{field.Key}",
                        Value = field.Value is IEnumerable<string> ?
                            string.Join(";", (IEnumerable<string>)field.Value) :
                            field.Value
                    });
                }

                var client = Data.GetClient<WorkItemTrackingHttpClient>();

                wi = client.UpdateWorkItemAsync(patch, (int)wi.Id, false, bypassRules)
                    .GetResult("Error updating work item");

            }

            // Change board status

            if (Parameters.HasParameter(nameof(SetWorkItem.BoardColumn)) ||
                Parameters.HasParameter(nameof(SetWorkItem.BoardColumnDone)) ||
                Parameters.HasParameter(nameof(SetWorkItem.BoardLane)))
            {

                var patch = new JsonPatchDocument(){
                            new JsonPatchOperation() {
                                Operation = Operation.Test,
                                Path = "/rev",
                                Value = wi.Rev
                            }
                        };

                var tp = Data.GetProject();
                var t = Data.GetTeam();
                var board = FindBoard((string)wi.Fields["System.WorkItemType"], tpc, tp, t);

                if (Parameters.HasParameter(nameof(SetWorkItem.BoardColumn)))
                {
                    patch.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = $"/fields/{board.Fields.ColumnField.ReferenceName}",
                        Value = Parameters.Get<string>(nameof(SetWorkItem.BoardColumn))
                    });
                }

                if (Parameters.HasParameter(nameof(SetWorkItem.BoardLane)))
                {
                    patch.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = $"/fields/{board.Fields.RowField.ReferenceName}",
                        Value = Parameters.Get<string>(nameof(SetWorkItem.BoardLane))
                    });
                }

                if (Parameters.HasParameter(nameof(SetWorkItem.BoardColumnDone)))
                {
                    patch.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = $"/fields/{board.Fields.DoneField.ReferenceName}",
                        Value = Parameters.Get<bool>(nameof(SetWorkItem.BoardColumnDone))
                    });
                }

                var client = Data.GetClient<WorkItemTrackingHttpClient>();

                wi = client.UpdateWorkItemAsync(patch, (int)wi.Id, false, bypassRules)
                    .GetResult("Error updating work item");
            }

            yield return wi;
        }

        private WebApiBoard FindBoard(string workItemType, Models.Connection tpc, WebApiTeamProject tp, WebApiTeam team)
        {
            var boards = Data.GetItems<WebApiBoard>(new
            {
                Board = "*",
                Team = team,
                Project = tp,
                Collection = tpc
            }).ToList();

            foreach (WebApiBoard b in boards)
            {
                var keys = b.AllowedMappings.Values.SelectMany(o => o.Keys).ToList();

                if (keys.Any(t => t.Equals(workItemType, StringComparison.OrdinalIgnoreCase)))
                {
                    return b;
                }
            }

            throw new Exception("Unable to find a board belonging to team " +
                $"'{team.Name}' that contains a mapping to the work item type '{workItemType}'");
        }

        private object GetFieldValue(string argName, string projectName)
        {
            object value = Parameters.Get<object>(argName);

            if (string.Equals(argName, "Area", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(argName, "Iteration", StringComparison.OrdinalIgnoreCase))
            {
                return NodeUtil.NormalizeNodePath((string)value, projectName, includeTeamProject: true, includeLeadingSeparator: true);
            }

            return value;
        }
    }
}