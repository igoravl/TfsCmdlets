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
        public object SourceWorkItem { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        [Alias("To")]
        [ValidateNotNull()]
        public object TargetWorkItem { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        [Alias("LinkType", "Type")]
        public object EndLinkType { get; set; }

        [Parameter]
        public string Comment { get; set; }

        public SwitchParameter SkipSave { get; set; }

        [Parameter]
        public object Collection { get; set; }
    }
}