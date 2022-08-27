using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    /// <summary>
    /// Imports a work item type definition into a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, DesktopOnly = true)]
    partial class ImportWorkItemType
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true, ParameterSetName = "Import from XML")]
        [ValidateNotNull]
        public string Xml { get; set; }

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Import from file")]
        [ValidateNotNull]
        public string Path { get; set; }
    }
}