using System.Management.Automation;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    /// <summary>
    /// Adds new members to a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, SupportsShouldProcess = true, OutputType = typeof(TeamAdmins))]
    partial class AddTeamMember
    {
        /// <summary>
        /// Specifies the member (user or group) to add to the given team.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Member { get; set; }
    }
}