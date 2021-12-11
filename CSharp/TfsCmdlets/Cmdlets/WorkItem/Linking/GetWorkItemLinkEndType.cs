using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Gets the work item link end types of a team project collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection)]
    partial class GetWorkItemLinkEndType
    {
        [Parameter(Position = 0)]
        [Alias("Name")]
        [SupportsWildcards]
        public string LinkEndType { get; set; } = "*";
    }
}
