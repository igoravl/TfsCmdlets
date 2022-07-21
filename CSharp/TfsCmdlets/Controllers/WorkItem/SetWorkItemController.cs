using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Cmdlets.WorkItem;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class SetWorkItemController
    {
        [Import]
        private INodeUtil NodeUtil { get; }

        [Import]
        private IWorkItemPatchBuilder Builder { get; }

        protected override IEnumerable Run()
        {
            var wi = Data.GetItem<WebApiWorkItem>();
            var tpc = Data.GetCollection();
            var fieldValues = Parameters.Get<Hashtable>(nameof(SetWorkItem.Fields), new Hashtable()).ToDictionary<string, object>();
            var bypassRules = Parameters.Get<bool>(nameof(SetWorkItem.BypassRules));
            var boardColumn = Parameters.Get<string>(nameof(SetWorkItem.BoardColumn));
            var boardColumnDone = Parameters.Get<bool>(nameof(SetWorkItem.BoardColumnDone));
            var boardLane = Parameters.Get<string>(nameof(SetWorkItem.BoardLane));
            var fieldTypes = new Dictionary<string, string>();
            var projectName = (string) wi.Fields["System.TeamProject"];

            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            Builder.Initialize(wi);

            wi = client.UpdateWorkItemAsync(Builder.GetJson(), (int)wi.Id, false, bypassRules)
                .GetResult("Error updating work item");

            yield return wi;
        }
    }
}