using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Controllers.Team
{
    [CmdletController(typeof(WebApiTeam))]
    partial class RemoveTeamController
    {
        public override IEnumerable<WebApiTeam> Invoke()
        {
            var tp = Data.GetProject();
            var teams = Data.GetItems<WebApiTeam>();

            foreach (var t in teams)
            {
                if (!PowerShell.ShouldProcess(tp, $"Delete team '{t.Name}'")) continue;

                Data.GetClient<TeamHttpClient>().DeleteTeamAsync(tp.Name, t.Name)
                    .Wait($"Error deleting team {t.Name}");
            }

            return null;
        }
    }
}