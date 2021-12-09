using System.Management.Automation;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Removes an administrator from a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsTeamAdmin", SupportsShouldProcess = true)]
    [OutputType(typeof(TeamAdmins))]
    [TfsCmdlet(CmdletScope.Team)]
    partial class RemoveTeamAdmin
    {
        /// <summary>
        /// Specifies the administrator to remove from the team.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Admin { get; set; }
    }
}