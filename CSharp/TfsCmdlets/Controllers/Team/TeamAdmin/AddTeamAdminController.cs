using TfsCmdlets.Cmdlets.Team.TeamAdmin;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Controllers.Team.TeamAdmin
{
    [CmdletController(typeof(Models.TeamAdmin))]
    partial class AddTeamAdminController
    {
        public override IEnumerable<Models.TeamAdmin> Invoke()
        {
            var t = Data.GetTeam();
            var admParam = Parameters.Get<object>(nameof(AddTeamAdmin.Admin));
            var admin = Data.GetItem<Models.Identity>(new { Identity = admParam });

            if (admin.IsContainer)
            {
                Logger.LogError(new ArgumentException($"'{admin.DisplayName}' is a group. Only users can be added as administrators."));
                yield break;
            }

            if (!PowerShell.ShouldProcess(t, $"Add administrator '{admin.DisplayName} ({admin.UniqueName})'")) yield break;

            var client = Data.GetClient<TeamAdminHttpClient>();
            var result = client.AddTeamAdmin(t.ProjectName, t.Id, admin.Id);

            yield return new Models.TeamAdmin(admin, t);
        }
    }
}