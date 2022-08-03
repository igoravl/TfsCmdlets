using TfsCmdlets.Cmdlets.Team.TeamAdmin;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Controllers.Team.TeamAdmin
{
    [CmdletController(typeof(Models.TeamAdmin))]
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

            var client = Data.GetClient<TeamAdminHttpClient>();

            if (!client.RemoveTeamAdmin(t.ProjectName, t.Id, adminIdentity.Id))
            {
                throw new Exception($"Error removing team administrator '{admin}'");
            }

            return null;
        }
    }
}