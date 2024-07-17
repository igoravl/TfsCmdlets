using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Gets information about one or more teams.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, DefaultParameterSetName = "Get by team", OutputType = typeof(Models.Team))]
    partial class GetTeam 
    {
        /// <summary>
        /// Specifies the team to return. Accepted values are its name, its ID, or a
        /// Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object. Wildcards are supported.
        /// When omitted, all teams in the given team project are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName="Get by team")]
        [Parameter(Position = 0, ParameterSetName="Cached credentials")]
        [Parameter(Position = 0, ParameterSetName="User name and password")]
        [Parameter(Position = 0, ParameterSetName="Credential object")]
        [Parameter(Position = 0, ParameterSetName="Personal Access Token")]
        [Parameter(Position = 0, ParameterSetName="Prompt for credential")]
        [Alias("Name")]
        [SupportsWildcards]
        public object Team { get; set; } = "*";

        /// <summary>
        /// Get team members (fills the Members property with a list of
        /// Microsoft.VisualStudio.Services.WebApi.TeamMember objects).
        /// When omitted, only basic team information (such as name, description and ID) are returned.
        /// </summary>
        [Parameter(ParameterSetName="Get by team")]
        [Parameter(ParameterSetName="Cached credentials")]
        [Parameter(ParameterSetName="User name and password")]
        [Parameter(ParameterSetName="Credential object")]
        [Parameter(ParameterSetName="Personal Access Token")]
        [Parameter(ParameterSetName="Prompt for credential")]
        [Parameter(ParameterSetName="Get default team")]
        [Alias("QueryMembership")]
        public SwitchParameter IncludeMembers { get; set; }

        /// <summary>
        /// Gets team settings (fills the Settings, TeamField, and IterationPaths properties).
        /// </summary>
        [Parameter(ParameterSetName="Get by team")]
        [Parameter(ParameterSetName="Cached credentials")]
        [Parameter(ParameterSetName="User name and password")]
        [Parameter(ParameterSetName="Credential object")]
        [Parameter(ParameterSetName="Personal Access Token")]
        [Parameter(ParameterSetName="Prompt for credential")]
        [Parameter(ParameterSetName="Get default team")]
        public SwitchParameter IncludeSettings { get; set; }

        /// <summary>
        /// Returns the team specified in the last call to Connect-TfsTeam (i.e. the "current" team)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }

        /// <summary>
        /// Returns the default team in the given team project.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName="Get default team")]
        public SwitchParameter Default { get; set; }
    }

    [CmdletController(typeof(Models.Team))]
    partial class GetTeamController
    {
        [Import]
        private ICurrentConnections CurrentConnections { get; }

        [Import]
        private IPaginator Paginator { get; }

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
                    team = new Guid((string) props.Where(p => p.Name.Equals("System.Microsoft.TeamFoundation.Team.Default"))
                        .FirstOrDefault()?.Value);
                }

                if (Current || (team is string s1 && string.IsNullOrEmpty(s1)))
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

                            foreach (var result in Paginator.Paginate(
                                    (top, skip) => client.GetTeamsAsync(Project.Name, top: top, skip: skip).GetResult($"Error getting team(s) '{s}'"))
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