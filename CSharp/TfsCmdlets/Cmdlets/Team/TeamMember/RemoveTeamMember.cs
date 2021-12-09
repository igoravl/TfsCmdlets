using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    /// <summary>
    /// Removes a member from a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsTeamMember", SupportsShouldProcess = true)]
    [TfsCmdlet(CmdletScope.Team)]
    partial class RemoveTeamMember
    {
        /// <summary>
        /// Specifies the member (user or group) to remove from the given team.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Member { get; set; }
    }
}