using System.Management.Automation;
using TfsCmdlets.HttpClients;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Removes an administrator from a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, SupportsShouldProcess = true, DataType = typeof(Models.TeamAdmin),
        OutputType = typeof(WebApiIdentity))]
    partial class RemoveTeamAdmin
    {
        /// <summary>
        /// Specifies the administrator to remove from the team.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Admin { get; set; }
    }

    [CmdletController(typeof(Models.TeamAdmin), Client=typeof(TeamAdminHttpClient))]
    partial class RemoveTeamAdminController
    {
        protected override IEnumerable Run()
        {
            var admin = Parameters.GetRaw<object>(nameof(RemoveTeamAdmin.Admin));

            Models.Team t = (admin is WebApiIdentity identity)? 
                Data.GetItem<Models.Team>(new { Team = identity.Properties["TeamId"], Project = identity.Properties["ProjectId"] }) : 
                Data.GetTeam();

            var adminIdentity = Data.GetItem<Models.TeamAdmin>(new { Identity = admin });

            if (!PowerShell.ShouldProcess($"Team '{t.Name}'",
                $"Remove administrator '{adminIdentity.DisplayName} ({adminIdentity.UniqueName})'")) return null;

            if (!Client.RemoveTeamAdmin(t.ProjectName, t.Id, adminIdentity.Id))
            {
                throw new Exception($"Error removing team administrator '{admin}'");
            }

            return null;
        }
    }
}