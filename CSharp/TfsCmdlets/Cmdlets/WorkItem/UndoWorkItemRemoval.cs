using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Deletes a work item from a team project collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
    partial class UndoWorkItemRemoval
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
    }
}