using System;
using System.Management.Automation;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Removes an administrator from a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsTeamAdmin", SupportsShouldProcess = true)]
    [OutputType(typeof(TeamAdmins))]
    public class RemoveTeamAdmin : CmdletBase
    {
        /// <summary>
        /// Specifies the administrator to remove from the team.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Admin { get; set; }

        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(Position = 1)]
        public object Team { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        // TODO

        ///// <summary>
        ///// Performs execution of the command
        ///// </summary>
        //protected override void DoProcessRecord()
        //{
        //    if (Admin is TeamAdmin ta)
        //    {
        //        Team = ta.TeamId;
        //        Project = ta.ProjectId;
        //    }

        //    var (_, _, t) = GetCollectionProjectAndTeam();
        //    var admin = GetItem<Models.Identity>(new { Identity = Admin });

        //    if (!PowerShell.ShouldProcess($"Team '{t.Name}'",
        //        $"Remove administrator '{admin.DisplayName} ({admin.UniqueName})'"))
        //    {
        //        return;
        //    }

        //    var client = Data.GetClient<TeamAdminHttpClient>(parameters);

        //    this.Log($"Removing administrator '{admin.DisplayName} ({admin.UniqueName})' from team '{t.Name}'");

        //    if (!client.RemoveTeamAdmin(t.ProjectName, t.Id, admin.Id))
        //    {
        //        throw new Exception("Error removing team administrator");
        //    }
        //}
    }
}