using System.Management.Automation;
using TfsCmdlets.HttpClient;
using TfsTeamAdmin = TfsCmdlets.Cmdlets.Team.TeamAdmin.TeamAdmin;
using TfsIdentity = TfsCmdlets.Services.Identity;
using System;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Adds a new administrator to a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "TfsTeamAdmin", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(TeamAdmins))]
    public class AddTeamAdmin : BaseCmdlet
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

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            var (_, _, t) = GetCollectionProjectAndTeam();
            var admin = GetItem<TfsIdentity>(new {Identity = Admin});

            if(admin.IsContainer)
            {
                throw new ArgumentException($"'{admin.DisplayName}' is a group. Only users can be added as administrators.");
            }

            if(!ShouldProcess($"Team '{t.Name}'", 
                $"Add administrator '{admin.DisplayName} ({admin.UniqueName})'"))
            {
                return;
            }

            this.Log($"Adding administrator '{admin.DisplayName} ({admin.UniqueName})' to team '{t.Name}'");

            var client = GetClient<TeamAdminHttpClient>();
            var result = client.AddTeamAdmin(t.ProjectName, t.Id, admin.Id); 

            if(Passthru) WriteObject(new TeamAdmin(admin, t));
        }
    }
}