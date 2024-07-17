using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Restores a deleted work item.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
    partial class UndoWorkItemRemoval
    {
        /// <summary>
        /// Specifies the ID of the work item to be restored. Can also receive the output of `Get-WorkItem -Deleted`.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }
    }

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