using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using TfsCmdlets.Cmdlets.Team;

namespace TfsCmdlets.Controllers.Team
{
    [CmdletController(typeof(Models.Team))]
    partial class GetTeamController
    {
        [Import]
        public ICurrentConnections CurrentConnections { get; }

        protected override IEnumerable Run()
        {
            var team = Parameters.Get<object>(nameof(GetTeam.Team));
            var current = Parameters.Get<bool>(nameof(GetTeam.Current));
            var includeMembers = Parameters.Get<bool>(nameof(GetTeam.QueryMembership));
            var includeSettings = Parameters.Get<bool>(nameof(GetTeam.IncludeSettings));
            var defaultTeam = Parameters.Get<bool>(nameof(GetTeam.Default));

            var tp = Data.GetProject();
            var client = Data.GetClient<TeamHttpClient>();

            switch (team)
            {
                case Guid g:
                    {
                        team = g.ToString();
                        break;
                    }
                case { } when defaultTeam:
                    {
                        Logger.Log("Get default team");
                        var projectClient = Data.GetClient<ProjectHttpClient>();
                        var props = projectClient
                            .GetProjectPropertiesAsync(tp.Id)
                            .GetResult("Error retrieving project's default team");
                        team = props.Where(p => p.Name.Equals("System.Microsoft.TeamFoundation.Team.Default"))
                            .FirstOrDefault()?.Value;
                        defaultTeam = false;
                        break;
                    }
                case null:
                case { } when current:
                    {
                        Logger.Log("Get currently connected team");

                        if (CurrentConnections.Team == null) yield break;
                        team = CurrentConnections.Team.Id.ToString();
                        current = false;
                        break;
                    }
                case WebApiTeam t when (includeMembers || includeSettings):
                    {
                        team = t.Id.ToString();
                        break;
                    }
            }

            switch (team)
            {
                case WebApiTeam t:
                    {
                        yield return CreateTeamObject(t);
                        yield break;
                    }
                case string s when !s.IsWildcard():
                    {
                        var result = client.GetTeamAsync(tp.Name, s)
                            .GetResult($"Error getting team '{s}'");
                        yield return CreateTeamObject(result);
                        yield break;
                    }
                case string s:
                    {
                        foreach (var result in client.GetTeamsAsync(tp.Name)
                            .GetResult($"Error getting team(s) '{s}'")
                            .Where(t => t.Name.IsLike(s)))
                        {
                            yield return CreateTeamObject(result);
                        }
                        yield break;
                    }
                default:
                    {
                        throw new ArgumentException($"Invalid or non-existent team {team}");
                    }
            }
        }

        private Models.Team CreateTeamObject(WebApiTeam innerTeam)
        {
            var team = new Models.Team(innerTeam);

            var includeMembers = Parameters.Get<bool>(nameof(GetTeam.QueryMembership));
            var includeSettings = Parameters.Get<bool>(nameof(GetTeam.IncludeSettings));

            if (includeMembers)
            {
                var client = Data.GetClient<TeamHttpClient>();
                Logger.Log($"Retrieving team membership information for team '{team.Name}'");

                var members = client.GetTeamMembersWithExtendedPropertiesAsync(team.ProjectName, team.Name)
                    .GetResult($"Error retrieving membership information for team {team.Name}");

                team.TeamMembers = members;
            }

            if (includeSettings)
            {
                var workClient = Data.GetClient<WorkHttpClient>();
                Logger.Log($"Retrieving team settings for team '{team.Name}'");

                var ctx = new TeamContext(team.ProjectName, team.Name);
                team.Settings = workClient.GetTeamSettingsAsync(ctx)
                    .GetResult($"Error retrieving settings for team {team.Name}");
            }

            return team;
        }
    }
}