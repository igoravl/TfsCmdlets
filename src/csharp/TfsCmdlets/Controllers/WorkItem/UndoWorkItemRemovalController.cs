using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class UndoWorkItemRemovalController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<WorkItemTrackingHttpClient>();

            foreach (var wi in GetItems<WebApiWorkItem>(new { Deleted = true }))
            {
                if (!PowerShell.ShouldProcess($"[Organization: {Collection.DisplayName}]/[Work Item: {wi.Id}]", $"Restore work item")) continue;

                client.RestoreWorkItemAsync(new Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemDeleteUpdate()
                {
                    IsDeleted = false
                }, (int) wi.Id).GetResult($"Error restoring work item {wi.Id}");
            }

            return null;
        }
    }
}