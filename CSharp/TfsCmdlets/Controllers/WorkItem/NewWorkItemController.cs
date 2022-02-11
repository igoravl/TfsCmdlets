using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class NewWorkItemController
    {
        [Import]
        private INodeUtil NodeUtil { get; }

        protected override IEnumerable Run()
        {
            var type = Data.GetItem<WebApiWorkItemType>();

            if (type == null) throw new ArgumentException($"Invalid or non-existent work item type {type}");

            if (!PowerShell.ShouldProcess(Project, $"Create new '{type}' work item")) yield break;

            var patch = new JsonPatchDocument();

            foreach (var argName in Parameters.Keys.Where(f => Parameters.HasParameter(f) && !Fields.ContainsKey(f) && SetWorkItemController.WellKnownFields.ContainsKey(f)))
            {
                var refName = GetWorkItemController.SimpleQueryFields[argName].Item2;

                var value = refName switch {
                    "System.AreaPath" => NodeUtil.NormalizeNodePath(Parameters.Get<string>(argName), Project.Name, includeTeamProject: true),
                    "System.IterationPath" => NodeUtil.NormalizeNodePath(Parameters.Get<string>(argName), Project.Name, includeTeamProject: true),
                    _ =>  Parameters.Get<object>(argName)
                };

                patch.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = $"/fields/{refName}",
                    Value = value is IEnumerable<string> enumerable ? string.Join(";", enumerable) : value
                });
            }

            foreach(DictionaryEntry de in Fields)
            {
                patch.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = $"/fields/{de.Key}",
                    Value = de.Value is IEnumerable<string> enumerable ? string.Join(";", enumerable) : de.Value
                });
            }

            if(!patch.Any(p => p.Path.EndsWith("System.AreaPath")))
            {
                patch.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.AreaPath",
                    Value = Project.Name
                });
            }

            if(!patch.Any(p => p.Path.EndsWith("System.IterationPath")))
            {
                patch.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.IterationPath",
                    Value = Project.Name
                });
            }

            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            var wi = client.CreateWorkItemAsync(patch, Project.Name, type.Name, false, BypassRules)
                .GetResult("Error creating work item");

            // New board column

            if (Has_BoardColumn || Has_BoardColumnDone || Has_BoardLane)
            {
                wi = Data.SetItem<WebApiWorkItem>(new
                {
                    WorkItem = wi,
                    BoardColumn = BoardColumn,
                    BoardColumnDone = BoardColumnDone,
                    BoardLane = BoardLane,
                    BypassRules = BypassRules
                });
            }

            yield return wi;
        }
    }
}