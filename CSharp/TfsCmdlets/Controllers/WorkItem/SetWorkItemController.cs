using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Cmdlets.WorkItem;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class SetWorkItemController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        protected override IEnumerable Run()
        {
            var wi = Data.GetItem<WebApiWorkItem>();
            var tpc = Data.GetCollection();
            var fields = Parameters.Get<Hashtable>(nameof(SetWorkItem.Fields), new Hashtable());
            var bypassRules = Parameters.Get<bool>(nameof(SetWorkItem.BypassRules));
            var boardColumn = Parameters.Get<string>(nameof(SetWorkItem.BoardColumn));
            var boardColumnDone = Parameters.Get<bool>(nameof(SetWorkItem.BoardColumnDone));
            var boardLane = Parameters.Get<string>(nameof(SetWorkItem.BoardLane));
            var fieldTypes = new Dictionary<string, string>();

            foreach (var argName in Parameters.Keys.Where(f => Parameters.HasParameter(f) && WellKnownFields.ContainsKey(f)))
            {
                fields[WellKnownFields[argName].Item2] = GetFieldValue(argName, (string)wi.Fields["System.TeamProject"]);
                fieldTypes[WellKnownFields[argName].Item2] = WellKnownFields[argName].Item1;
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
                    switch (field.Key)
                    {
                        case "System.Tags" when field.Value is string s && string.IsNullOrEmpty(s):
                        case "System.Tags" when field.Value is ICollection<string> tags && tags.Count == 0:
                            {
                                patch.Add(new JsonPatchOperation()
                                {
                                    Operation = Operation.Remove,
                                    Path = "/fields/System.Tags"
                                });
                                break;
                            }
                        case string key when fieldTypes[key].Equals("Identifier"):
                            {
                                if (field.Value is string s && s.Equals(string.Empty))
                                {
                                    patch.Add(new JsonPatchOperation()
                                    {
                                        Operation = Operation.Remove,
                                        Path = $"/fields/{field.Key}"
                                    });
                                    break;
                                }

                                var identity = GetItem<Models.Identity>(new { Identity = field.Value });

                                patch.Add(new JsonPatchOperation()
                                {
                                    Operation = Operation.Add,
                                    Path = $"/fields/{field.Key}",
                                    Value = identity.DisplayName
                                });

                                break;
                            }
                        default:
                            {
                                patch.Add(new JsonPatchOperation()
                                {
                                    Operation = Operation.Add,
                                    Path = $"/fields/{field.Key}",
                                    Value = field.Value is IEnumerable<string> enumerable ?
                                        string.Join(";", enumerable) :
                                        field.Value
                                });
                                break;
                            }
                    }
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
            var boards = Data.GetItems<Models.Board>(new
            {
                Board = "*",
                Team = team,
                Project = tp,
                Collection = tpc
            }).ToList();

            foreach (var b in boards)
            {
                var keys = b.InnerObject.AllowedMappings.Values.SelectMany(o => o.Keys).ToList();

                if (keys.Any(t => t.Equals(workItemType, StringComparison.OrdinalIgnoreCase)))
                {
                    return b.InnerObject;
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


        internal static readonly Dictionary<string, Tuple<string, string>> WellKnownFields = new()
        {
            ["AreaPath"] = new Tuple<string, string>("Tree", "System.AreaPath"),
            ["AssignedTo"] = new Tuple<string, string>("Identifier", "System.AssignedTo"),
            ["ChangedBy"] = new Tuple<string, string>("Identifier", "System.ChangedBy"),
            ["ChangedDate"] = new Tuple<string, string>("Date", "System.ChangedDate"),
            ["CreatedBy"] = new Tuple<string, string>("Identifier", "System.CreatedBy"),
            ["CreatedDate"] = new Tuple<string, string>("Date", "System.CreatedDate"),
            ["Description"] = new Tuple<string, string>("Text", "System.Description"),
            ["IterationPath"] = new Tuple<string, string>("Tree", "System.IterationPath"),
            ["Priority"] = new Tuple<string, string>("Number", "Microsoft.VSTS.Common.Priority"),
            ["Project"] = new Tuple<string, string>("Project", "System.TeamProject"),
            ["Reason"] = new Tuple<string, string>("Text", "System.Reason"),
            ["State"] = new Tuple<string, string>("Text", "System.State"),
            ["StateChangeDate"] = new Tuple<string, string>("Date", "Microsoft.VSTS.Common.StateChangeDate"),
            ["Tags"] = new Tuple<string, string>("LongText", "System.Tags"),
            ["Title"] = new Tuple<string, string>("Text", "System.Title"),
            ["ValueArea"] = new Tuple<string, string>("Text", "Microsoft.VSTS.Common.ValueArea"),
            ["WorkItemType"] = new Tuple<string, string>("Text", "System.WorkItemType")
        };
    }
}