using System.Management.Automation;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;

namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    /// <summary>
    /// Removes a member from a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, SupportsShouldProcess = true, DataType = typeof(Models.TeamMember),
        OutputType = typeof(WebApiIdentity))]
    partial class RemoveTeamMember
    {
        /// <summary>
        /// Specifies the member (user or group) to remove from the given team.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Member { get; set; }
    }
}