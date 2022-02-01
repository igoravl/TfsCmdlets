using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.Team;

namespace TfsCmdlets.Controllers.Team
{
    [CmdletController(typeof(Models.Team))]
    partial class NewTeamController
    {
        protected override IEnumerable Run()
        {
            if (!PowerShell.ShouldProcess(Project, $"Create team {Team}")) yield break;

            var client = GetClient<TeamHttpClient>();

            var t = client.CreateTeamAsync(new WebApiTeam()
                {
                    Name = Team,
                    Description = Description,
                }, Project.Name)
                .GetResult($"Error creating team {Team}");

            if (NoDefaultArea && NoBacklogIteration)
            {
                yield return t;
                yield break;
            }

            yield return Data.SetItem<Models.Team>(new {
                DefaultAreaPath = DefaultAreaPath ?? t.Name,
                BacklogIteration = BacklogIteration ?? t.Name
            });
        }
    }
}