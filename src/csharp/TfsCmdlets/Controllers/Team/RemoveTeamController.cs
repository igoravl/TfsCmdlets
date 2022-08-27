using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Controllers.Team
{
    [CmdletController(typeof(Models.Team))]
    partial class RemoveTeamController
    {
        protected override IEnumerable Run()
        {
            var client = Data.GetClient<TeamHttpClient>();

            foreach (var team in Items)
            {
                if (!PowerShell.ShouldProcess($"[Project: {team.ProjectName}]/[Team: {team.Name}]", $"Delete team")) continue;

                client.DeleteTeamAsync(team.ProjectName, team.Name)
                    .Wait($"Error deleting team {team.Name}");
            }

            return null;
        }
    }
}