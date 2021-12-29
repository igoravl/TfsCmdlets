using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Cmdlets.WorkItem;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class NewWorkItemController
    {
        public override IEnumerable<WebApiWorkItem> Invoke()
        {
           var tp = Data.GetProject();
           var type = Data.GetItem<WebApiWorkItemType>();

           if (type == null) throw new ArgumentException($"Invalid or non-existent work item type {type}");

           if (!PowerShell.ShouldProcess(tp, $"Create new '{type}' work item")) yield break;

           var fields = Parameters.Get<Hashtable>(nameof(SetWorkItem.Fields), new Hashtable());
           var bypassRules = Parameters.Get<bool>(nameof(SetWorkItem.BypassRules));
           var patch = new JsonPatchDocument();

           foreach (var argName in Parameters.Keys.Where(f => Parameters.HasParameter(f) && !fields.ContainsKey(f)))
           {
               var value = Parameters.Get<object>(argName);
               patch.Add(new JsonPatchOperation()
               {
                   Operation = Operation.Add,
                   Path = $"/fields/{Parameters.Get<object>(argName)}",
                   Value = (value is IEnumerable<string> ? string.Join(";", (IEnumerable<string>)value) : value)
               });
           }

           var client = Data.GetClient<WorkItemTrackingHttpClient>();

           yield return client.CreateWorkItemAsync(patch, tp.Name, type.Name, false, bypassRules)
               .GetResult("Error creating work item");

           // TODO: Set board column

           // var boardColumn = Parameters.Get<string>(nameof(SetWorkItem.BoardColumn));
           // var boardColumnDone = Parameters.Get<bool>(nameof(SetWorkItem.BoardColumnDone));
           // var boardLane = Parameters.Get<string>(nameof(SetWorkItem.BoardLane));
       }
    }
}