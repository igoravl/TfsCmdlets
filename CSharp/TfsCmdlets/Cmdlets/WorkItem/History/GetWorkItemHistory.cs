using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.History
{
    /// <summary>
    /// Gets the history of changes of a work item.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, NoAutoPipeline = true, OutputType = typeof(Models.WorkItemHistoryEntry))]
    partial class GetWorkItemHistory
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }
    }
}