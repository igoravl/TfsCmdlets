using System.Management.Automation;
using Microsoft.VisualStudio.Services.Graph.Client;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Gets information about an Azure DevOps user.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(GraphGroup), DefaultParameterSetName = "Get by ID or Name")]
    partial class GetGroup
    {
        /// <summary>
        /// Specifies the user or group to be retrieved. Supported values are: 
        /// User/group name, email, or ID
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards]
        public object Group { get; set; } = "*";

        /// <summary>
        /// Specifies the scope under which to search for the group. 
        /// When omitted, defaults to the Server scope.
        /// </summary>
        [Parameter]
        public GroupScope Scope { get; set; } = GroupScope.Server;

        /// <summary>
        /// Searches recursively for groups in the scopes under the specified scope.
        /// </summary>
        [Parameter]
        public SwitchParameter Recurse { get; set; }
    }
}