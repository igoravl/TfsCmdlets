using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using TfsCmdlets.Cmdlets.WorkItem;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class RemoveWorkItemController
    {
        public override IEnumerable<WebApiWorkItem> Invoke()
        {
            var wis = Data.GetItems<WebApiWorkItem>();
            var tpc = Data.GetCollection();
            var tp = Data.GetProject();
            var destroy = Parameters.Get<bool>(nameof(RemoveWorkItem.Destroy));
            var force = Parameters.Get<bool>(nameof(RemoveWorkItem.Force));
            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            foreach (var wi in wis)
            {
                if (!PowerShell.ShouldProcess(tpc, $"{(destroy ? "Destroy" : "Delete")} work item {wi.Id}")) continue;

                if (destroy && !(force || PowerShell.ShouldContinue("Are you sure you want to destroy work item {wi.id}?"))) continue;

                client.DeleteWorkItemAsync(tp.Name, (int)wi.Id, destroy)
                    .GetResult($"Error {(destroy ? "destroying" : "deleting")} work item {wi.Id}");
            }
            
            return null;
        }
    }
}