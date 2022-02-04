using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Gets information about one or more teams.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, DefaultParameterSetName = "Get by team", OutputType = typeof(Models.Team))]
    partial class GetTeam 
    {
        /// <summary>
        /// Specifies the team to return. Accepted values are its name, its ID, or a
        /// Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object. Wildcards are supported.
        /// When omitted, all teams in the given team project are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName="Get by team")]
        [Parameter(Position = 0, ParameterSetName="Cached credentials")]
        [Parameter(Position = 0, ParameterSetName="User name and password")]
        [Parameter(Position = 0, ParameterSetName="Credential object")]
        [Parameter(Position = 0, ParameterSetName="Personal Access Token")]
        [Parameter(Position = 0, ParameterSetName="Prompt for credential")]
        [Alias("Name")]
        [SupportsWildcards]
        public object Team { get; set; } = "*";

        /// <summary>
        /// Get team members (fills the Members property with a list of
        /// Microsoft.VisualStudio.Services.WebApi.TeamMember objects).
        /// When omitted, only basic team information (such as name, description and ID) are returned.
        /// </summary>
        [Parameter]
        public SwitchParameter QueryMembership { get; set; }

        /// <summary>
        /// Gets the team's backlog settings (fills the Settings property with a
        /// Microsoft.TeamFoundation.Work.WebApi.TeamSetting object)
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeSettings { get; set; }

        /// <summary>
        /// Returns the team specified in the last call to Connect-TfsTeam (i.e. the "current" team)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }

        /// <summary>
        /// Returns the default team in the given team project.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName="Get default team")]
        public SwitchParameter Default { get; set; }
    }
}