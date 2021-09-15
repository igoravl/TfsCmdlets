using System.Management.Automation;
using TfsCmdlets.HttpClient;
using TfsTeamAdmin = TfsCmdlets.Cmdlets.Team.TeamAdmin.TeamAdmin;
using System;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Adds a new administrator to a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "TfsTeamAdmin", SupportsShouldProcess = true)]
    [OutputType(typeof(TeamAdmins))]
    public class AddTeamAdmin : CmdletBase
    {
        /// <summary>
        /// Specifies the administrator to add to the given team.
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

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        // TODO


        ///// <summary>
        ///// Performs execution of the command
        ///// </summary>
        //protected override void DoProcessRecord()
        //{
        //    var (_, _, t) = GetCollectionProjectAndTeam();
        //    var admin = GetItem<Models.Identity>(new {Identity = Admin});

        //    if(admin.IsContainer)
        //    {
        //        throw new ArgumentException($"'{admin.DisplayName}' is a group. Only users can be added as administrators.");
        //    }

        //    if(!PowerShell.ShouldProcess($"Team '{t.Name}'", 
        //        $"Add administrator '{admin.DisplayName} ({admin.UniqueName})'"))
        //    {
        //        return;
        //    }

        //    this.Log($"Adding administrator '{admin.DisplayName} ({admin.UniqueName})' to team '{t.Name}'");

        //    var client = GetClient<TeamAdminHttpClient>();
        //    var result = client.AddTeamAdmin(t.ProjectName, t.Id, admin.Id); 

        //    if(Passthru) WriteObject(new TeamAdmin(admin, t));
        //}
    }
}