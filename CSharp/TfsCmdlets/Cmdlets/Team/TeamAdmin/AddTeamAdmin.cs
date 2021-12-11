using System.Management.Automation;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Adds a new administrator to a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, SupportsShouldProcess = true, OutputType = typeof(TeamAdmins))]
    partial class AddTeamAdmin
    {
        /// <summary>
        /// Specifies the administrator to add to the given team.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Admin { get; set; }
    }
}