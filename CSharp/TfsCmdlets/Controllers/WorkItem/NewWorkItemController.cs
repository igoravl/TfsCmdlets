using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Cmdlets.WorkItem;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class NewWorkItemController
    {
        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var type = Data.GetItem<WebApiWorkItemType>();

            if (type == null) throw new ArgumentException($"Invalid or non-existent work item type {type}");

            if (!PowerShell.ShouldProcess(tp, $"Create new '{type}' work item")) yield break;

            var fields = Parameters.Get<Hashtable>(nameof(NewWorkItem.Fields), new Hashtable());
            var bypassRules = Parameters.Get<bool>(nameof(NewWorkItem.BypassRules));
            var patch = new JsonPatchDocument();

            foreach (var argName in Parameters.Keys.Where(f => Parameters.HasParameter(f) && !fields.ContainsKey(f) && SetWorkItemController.WellKnownFields.ContainsKey(f)))
            {
                var refName = GetWorkItemController.SimpleQueryFields[argName].Item2;
                var value = Parameters.Get<object>(argName);

                patch.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = $"/fields/{refName}",
                    Value = (value is IEnumerable<string> ? string.Join(";", (IEnumerable<string>)value) : value)
                });
            }

            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            var wi = client.CreateWorkItemAsync(patch, tp.Name, type.Name, false, bypassRules)
                .GetResult("Error creating work item");

            // New board column

            if (Parameters.HasParameter(nameof(NewWorkItem.BoardColumn)) ||
                Parameters.HasParameter(nameof(NewWorkItem.BoardColumnDone)) ||
                Parameters.HasParameter(nameof(NewWorkItem.BoardLane)))
            {
                wi = Data.SetItem<WebApiWorkItem>(new
                {
                    WorkItem = wi,
                    BoardColumn = Parameters.Get<string>(nameof(NewWorkItem.BoardColumn)),
                    BoardColumnDone = Parameters.Get<bool>(nameof(NewWorkItem.BoardColumnDone)),
                    BoardLane = Parameters.Get<string>(nameof(NewWorkItem.BoardLane)),
                    BypassRules = bypassRules
                });
            }

            yield return wi;
        }
    }
}