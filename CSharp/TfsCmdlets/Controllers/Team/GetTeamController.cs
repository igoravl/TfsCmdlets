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
            var client = Data.GetClient<TeamHttpClient>();

            foreach (var input in Team)
            {
                var team = input switch
                {
                    string s when s.IsGuid() => new Guid(s),
                    WebApiTeam t when IncludeSettings || IncludeMembers => t.Id,
                    _ => input
                };

                if (Default)
                {
                    Logger.Log("Get default team");

                    var projectClient = Data.GetClient<ProjectHttpClient>();
                    var props = projectClient
                        .GetProjectPropertiesAsync(Project.Id)
                        .GetResult("Error retrieving project's default team");
                    team = props.Where(p => p.Name.Equals("System.Microsoft.TeamFoundation.Team.Default"))
                        .FirstOrDefault()?.Value;
                }

                if (Current)
                {
                    Logger.Log("Get currently connected team");

                    if (CurrentConnections.Team == null) yield break;

                    var t = CurrentConnections.Team;

                    if (!IncludeSettings && !IncludeMembers)
                    {
                        yield return t;
                        yield break;
                    }

                    team = t.Id;
                }

                switch (team)
                {
                    case WebApiTeam t:
                        {
                            yield return CreateTeamObject(t);
                            yield break;
                        }
                    case Guid g:
                        {
                            var result = client.GetTeamAsync(Project.Name, g.ToString())
                                .GetResult($"Error getting team '{g}'");
                            yield return CreateTeamObject(result);
                            yield break;
                        }
                    case string s when !s.IsWildcard():
                        {
                            var result = client.GetTeamAsync(Project.Name, s)
                                .GetResult($"Error getting team '{s}'");
                            yield return CreateTeamObject(result);
                            yield break;
                        }
                    case string s:
                        {
                            foreach (var result in client.GetTeamsAsync(Project.Name)
                                .GetResult($"Error getting team(s) '{s}'")
                                .Where(t => t.Name.IsLike(s)))
                            {
                                yield return CreateTeamObject(result);
                            }
                            yield break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent team {team}"));
                            break;
                        }
                }
            }
        }

        private Models.Team CreateTeamObject(WebApiTeam innerTeam)
        {
            var team = new Models.Team(innerTeam);

            if (IncludeMembers)
            {
                var client = GetClient<TeamHttpClient>();

                Logger.Log($"Retrieving team membership information for team '{team.Name}'");
                team.TeamMembers = client.GetTeamMembersWithExtendedPropertiesAsync(team.ProjectName, team.Name)
                    .GetResult($"Error retrieving membership information for team {team.Name}");
            }

            if (IncludeSettings)
            {
                var workClient = GetClient<WorkHttpClient>();
                var ctx = new TeamContext(team.ProjectName, team.Name);

                Logger.Log($"Retrieving team settings for team '{team.Name}'");
                team.Settings = workClient.GetTeamSettingsAsync(ctx)
                    .GetResult($"Error retrieving settings for team {team.Name}");

                Logger.Log($"Retrieving default team field (area path) for team '{team.Name}'");
                team.TeamField = workClient.GetTeamFieldValuesAsync(ctx)
                    .GetResult($"Error retrieving team field values for team {team.Name}");

                Logger.Log($"Retrieving iterations for team '{team.Name}'");
                team.IterationPaths = workClient.GetTeamIterationsAsync(ctx)
                    .GetResult("Error getting team's current iterations");
            }

            return team;
        }
    }
}