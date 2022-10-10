using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Adds a link between two work items.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection)]
    partial class AddWorkItemLink
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [Alias("Id", "From")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        [Parameter(Position = 1, Mandatory = true, ParameterSetName = "Link to work item")]
        [Alias("To")]
        [ValidateNotNull()]
        public object TargetWorkItem { get; set; }

        [Parameter(Position = 2, Mandatory = true, ParameterSetName = "Link to work item")]
        [Alias("EndLinkType", "Type")]
        public WorkItemLinkType LinkType { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        /// <value></value>
        [Parameter]
        public SwitchParameter Passthru { get; set; }

        [Parameter]
        public string Comment { get; set; }
    }
}