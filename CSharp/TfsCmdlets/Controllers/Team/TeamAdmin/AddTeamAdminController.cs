using TfsCmdlets.Cmdlets.Team.TeamAdmin;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Controllers.Team.TeamAdmin
{
    [CmdletController(typeof(Models.TeamAdmin))]
    partial class AddTeamAdminController
    {
        protected override IEnumerable Run()
        {
            var team = Data.GetTeam();
            var member = Parameters.Get<object>(nameof(AddTeamAdmin.Admin));

            var identities = Data.GetItems<Models.Identity>(new { Identity = member })
                .Where(i => !i.IsContainer)
                .ToDictionary(i => i.Id.ToString());

            if (identities.Count == 0)
            {
                Logger.LogWarn($"No identities found matching '{member}'");
                yield break;
            }

            var ids = identities.Values.Select(i => i.Id);
            var uniqueNames = identities.Values.Select(i => i.UniqueName);

            if (!PowerShell.ShouldProcess(team, $"Add team administrator(s) {string.Join(", ", uniqueNames)}")) yield break;

            var client = Data.GetClient<TeamAdminHttpClient>();
            var result = client.AddTeamAdmin(team.ProjectId, team.Id, ids);

            foreach (var addedAdmin in result)
            {
                if (!identities.ContainsKey(addedAdmin.TeamFoundationId)) continue;

                yield return new Models.TeamAdmin(identities[addedAdmin.TeamFoundationId].InnerObject, team);
            }
        }
    }
}