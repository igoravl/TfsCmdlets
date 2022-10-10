using System.Management.Automation;

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
}