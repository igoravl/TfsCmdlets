using Microsoft.VisualStudio.Services.Graph.Client;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Creates a new Azure DevOps group.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GraphGroup), SupportsShouldProcess = true)]
    partial class NewGroup
    {
        /// <summary>
        /// Specifies the name of the new group.
        /// </summary>
        [Parameter(Position = 0)]
        public string Group { get; set; }

        /// <summary>
        /// Specifies a description for the new group.
        /// </summary>
        [Parameter()]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the scope under which to create the group. 
        /// When omitted, defaults to the Collection scope.
        /// </summary>
        [Parameter]
        public GroupScope Scope { get; set; } = GroupScope.Collection;
    }
}