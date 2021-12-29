using TfsCmdlets.Cmdlets.Team.TeamAdmin;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Controllers.Team.TeamAdmin
{
    [CmdletController(typeof(Models.TeamAdmin))]
    partial class RemoveTeamAdminController
    {
        public override IEnumerable<Models.TeamAdmin> Invoke()
        {
            var admin = Parameters.GetRaw<object>(nameof(RemoveTeamAdmin.Admin));

            object pipelineParms = admin switch
            {
                WebApiIdentity identity => new { Team = identity.Properties["TeamId"], Project = identity.Properties["ProjectId"] },
                _ => null
            };

            var tp = Data.GetProject(pipelineParms);
            var t = Data.GetTeam(pipelineParms);

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