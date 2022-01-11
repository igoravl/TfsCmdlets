using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.Team;

namespace TfsCmdlets.Controllers.Team
{
    [CmdletController(typeof(Models.Team))]
    partial class NewTeamController
    {
        protected override IEnumerable Run()
        {
            var team = Parameters.Get<string>(nameof(NewTeam.Team));
            var description = Parameters.Get<string>(nameof(NewTeam.Description));
            var noAreaPath = Parameters.Get<bool>(nameof(NewTeam.NoDefaultArea));
            var defaultAreaPath = Parameters.Get<string>(nameof(NewTeam.DefaultAreaPath), team);
            var noBacklogIteration = Parameters.Get<bool>(nameof(NewTeam.NoBacklogIteration));
            var backlogIteration = Parameters.Get<string>(nameof(NewTeam.BacklogIteration));
            var defaultIterationMacro = Parameters.Get<string>(nameof(NewTeam.DefaultIterationMacro));

            var tp = Data.GetProject();

            if (!PowerShell.ShouldProcess(tp, $"Create team {team}")) yield break;

            var t = Data.GetClient<TeamHttpClient>().CreateTeamAsync(new WebApiTeam()
            {
                Name = team,
                Description = description,
            }, tp.Name).GetResult($"Error creating team {team}");

            if (!noAreaPath || !noBacklogIteration)
            {
                Data.SetItem<Models.Team>(new
                {
                    Team = t,
                    DefaultAreaPath = noAreaPath ? null : defaultAreaPath,
                    BacklogIteration = noBacklogIteration ? null : backlogIteration,
                    DefaultBacklogMacro = defaultIterationMacro
                });
            }

            yield return t;
        }
    }
}