using Microsoft.VisualStudio.Services.Graph.Client;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Removes an Azure DevOps group.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GraphGroup), SupportsShouldProcess = true)]
    partial class RemoveGroup
    {
        /// <summary>
        /// Specifies the group to be removed.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards]
        public object Group { get; set; } = "*";

        /// <summary>
        /// Specifies the scope under which to search for the group. 
        /// When omitted, defaults to the Collection scope.
        /// </summary>
        [Parameter]
        public GroupScope Scope { get; set; } = GroupScope.Collection;
    }
}