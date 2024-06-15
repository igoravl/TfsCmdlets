using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using TfsCmdlets.Cmdlets.WorkItem;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class RemoveWorkItemController
    {
        protected override IEnumerable Run()
        {
            var wis = Data.GetItems<WebApiWorkItem>();
            var tpc = Data.GetCollection();
            var destroy = Parameters.Get<bool>(nameof(RemoveWorkItem.Destroy));
            var force = Parameters.Get<bool>(nameof(RemoveWorkItem.Force));
            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            foreach (var wi in wis)
            {
                if (!PowerShell.ShouldProcess($"[Organization: {tpc.DisplayName}]/[Work Item: {wi.Id}]", $"{(destroy ? "Destroy" : "Delete")} work item")) continue;

                if (destroy && !(force || PowerShell.ShouldContinue($"Are you sure you want to destroy work item {wi.Id}?"))) continue;

                client.DeleteWorkItemAsync((int)wi.Id, destroy)
                    .GetResult($"Error {(destroy ? "destroying" : "deleting")} work item {wi.Id}");
            }
            
            return null;
        }
    }
}