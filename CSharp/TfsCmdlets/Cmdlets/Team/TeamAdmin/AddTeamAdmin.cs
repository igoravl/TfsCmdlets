using System.Management.Automation;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Adds a new administrator to a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "TfsTeamAdmin", SupportsShouldProcess = true)]
    [OutputType(typeof(TeamAdmins))]
    [TfsCmdlet(CmdletScope.Team)]
    partial class AddTeamAdmin
    {
        /// <summary>
        /// Specifies the administrator to add to the given team.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Admin { get; set; }
    }
}