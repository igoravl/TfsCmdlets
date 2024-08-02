using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Deletes a work item from a team project collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
    partial class RemoveWorkItem
    {
        /// <summary>
        /// Specifies the work item to remove.
        /// </summary>
        /// <seealso cref="Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem">
        /// A WorkItem object
        /// </seealso>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Permanently deletes the work item, without sending it to the recycle bin.
        /// </summary>
        [Parameter]
        public SwitchParameter Destroy { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(WebApiWorkItem), Client=typeof(IWorkItemTrackingHttpClient))]
    partial class RemoveWorkItemController
    {
        protected override IEnumerable Run()
        {
            var wis = Data.GetItems<WebApiWorkItem>();
            var tpc = Data.GetCollection();
            var destroy = Parameters.Get<bool>(nameof(RemoveWorkItem.Destroy));
            var force = Parameters.Get<bool>(nameof(RemoveWorkItem.Force));

            foreach (var wi in wis)
            {
                if (!PowerShell.ShouldProcess($"[Organization: {tpc.DisplayName}]/[Work Item: {wi.Id}]", $"{(destroy ? "Destroy" : "Delete")} work item")) continue;

                if (destroy && !(force || PowerShell.ShouldContinue($"Are you sure you want to destroy work item {wi.Id}?"))) continue;

                Client.DeleteWorkItemAsync((int)wi.Id, destroy)
                    .GetResult($"Error {(destroy ? "destroying" : "deleting")} work item {wi.Id}");
            }
            
            return null;
        }
    }
}