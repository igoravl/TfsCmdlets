using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using TfsCmdlets.Extensions;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    /// <summary>
    /// Adds new members to a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "TfsTeamMember", SupportsShouldProcess = true)]
    [OutputType(typeof(TeamAdmins))]
    [TfsCmdlet(CmdletScope.Team)]
    partial class AddTeamMember
    {
        /// <summary>
        /// Specifies the member (user or group) to add to the given team.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Member { get; set; }
    }
}