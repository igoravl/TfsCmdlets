using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Gets the work item link end types of a team project collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection)]
    partial class GetWorkItemLinkType
    {
        [Parameter(Position = 0)]
        [SupportsWildcards]
        [Alias("Name", "EndLinkType", "Type", "Link")]
        public object LinkType { get; set; } = "*";
    }
}
