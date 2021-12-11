using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    /// <summary>
    /// Imports a work item type definition into a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project)]
    partial class ImportWorkItemType
    {
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Xml { get; set; }
    }
}