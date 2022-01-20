using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Gets information about one or more team projects.
    /// </summary>
    /// <remarks>
    /// The Get-TfsTeamProject cmdlets gets one or more Team Project objects 
    /// (an instance of Microsoft.TeamFoundation.Core.WebApi.TeamProject) from the supplied 
    /// Team Project Collection.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Collection, DefaultParameterSetName = "Get by project", OutputType = typeof(WebApiTeamProject))]
    partial class GetTeamProject
    {
        /// <summary>
        /// Specifies the name of a Team Project. Wildcards are supported. 
        /// When omitted, all team projects in the supplied collection are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by project")]
        [Parameter(Position = 0, ParameterSetName = "Cached credentials")]
        [Parameter(Position = 0, ParameterSetName = "User name and password")]
        [Parameter(Position = 0, ParameterSetName = "Credential object")]
        [Parameter(Position = 0, ParameterSetName = "Personal Access Token")]
        [Parameter(Position = 0, ParameterSetName = "Prompt for credential")]
        public object Project { get; set; } = "*";

        /// <summary>
        /// Lists deleted team projects present in the "recycle bin"
        /// </summary>
        [Parameter(ParameterSetName = "Get by project")]
        [Parameter(ParameterSetName = "Cached credentials")]
        [Parameter(ParameterSetName = "User name and password")]
        [Parameter(ParameterSetName = "Credential object")]
        [Parameter(ParameterSetName = "Personal Access Token")]
        [Parameter(ParameterSetName = "Prompt for credential")]
        public SwitchParameter Deleted { get; set; }

        /// <summary>
        /// Includes details about the team projects, such as the process template name and other properties.
        /// Specifying this argument signficantly increases the time it takes to complete the operation.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeDetails { get; set; }

        /// <summary>
        /// Returns the team project specified in the last call to Connect-TfsTeamProject 
        /// (i.e. the "current" team project)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }
    }
}