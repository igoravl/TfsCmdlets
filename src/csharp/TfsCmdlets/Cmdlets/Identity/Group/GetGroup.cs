using Microsoft.VisualStudio.Services.Graph.Client;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Gets one or more Azure DevOps groups.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GraphGroup))]
    partial class GetGroup
    {
        /// <summary>
        /// Specifies the group to be retrieved. Supported values are: 
        /// Group name or ID. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards]
        public object Group { get; set; } = "*";

        /// <summary>
        /// Specifies the scope under which to search for the group. 
        /// When omitted, defaults to the Collection scope.
        /// </summary>
        [Parameter]
        public GroupScope Scope { get; set; } = GroupScope.Collection;

        /// <summary>
        /// Searches recursively for groups in the scopes under the specified scope.
        /// </summary>
        [Parameter]
        public SwitchParameter Recurse { get; set; }
    }
}