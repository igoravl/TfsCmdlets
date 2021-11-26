using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Controllers.Team
{
    [CmdletController]
    internal class GetTeam : ControllerBase<WebApiTeam>
    {
        public ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<WebApiTeam> Invoke(ParameterDictionary parameters)
        {
            var team = parameters.Get<object>(nameof(Cmdlets.Team.GetTeam.Team));
            var current = parameters.Get<bool>(nameof(Cmdlets.Team.GetTeam.Current));
            var includeMembers = parameters.Get<bool>(nameof(Cmdlets.Team.GetTeam.QueryMembership));
            var includeSettings = parameters.Get<bool>(nameof(Cmdlets.Team.GetTeam.IncludeSettings));
            var defaultTeam = parameters.Get<bool>(nameof(Cmdlets.Team.GetTeam.Default));

            var tp = Data.GetProject(parameters);
            var client = Data.GetClient<TeamHttpClient>(parameters);

            while (true) switch (team)
                {
                    case Guid g:
                        {
                            team = g.ToString();
                            continue;
                        }
                    case { } when defaultTeam:
                        {
                            Logger.Log("Get default team");
                            var projectClient = Data.GetClient<ProjectHttpClient>(parameters);
                            var props = projectClient
                                .GetProjectPropertiesAsync(tp.Id)
                                .GetResult("Error retrieving project's default team");
                            team = props.Where(p => p.Name.Equals("System.Microsoft.TeamFoundation.Team.Default"))
                                .FirstOrDefault()?.Value;
                            defaultTeam = false;
                            continue;
                        }
                    case null:
                    case { } when current:
                        {
                            Logger.Log("Get currently connected team");

                            if (CurrentConnections.Team == null) yield break;
                            team = CurrentConnections.Team.Id;
                            current = false;
                            continue;
                        }
                    case WebApiTeam t:
                        {
                            if (includeMembers || includeSettings)
                            {
                                team = t.Id.ToString();
                                continue;
                            }

                            yield return CreateTeamObject(t, parameters);
                            yield break;
                        }
                    case string s when !s.IsWildcard():
                        {
                            var result = client.GetTeamAsync(tp.Name, s)
                                .GetResult($"Error getting team '{s}'");
                            yield return CreateTeamObject(result, parameters);
                            yield break;
                        }
                    case string s:
                        {
                            foreach (var result in client.GetTeamsAsync(tp.Name)
                                .GetResult($"Error getting team(s) '{s}'")
                                .Where(t => t.Name.IsLike(s)))
                            {
                                yield return CreateTeamObject(result, parameters);
                            }
                            yield break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent team {team}");
                        }
                }
        }

        private Models.Team CreateTeamObject(WebApiTeam innerTeam, ParameterDictionary parameters)
        {
            var team = new Models.Team(innerTeam);

            var includeMembers = parameters.Get<bool>("IncludeMembers");
            var includeSettings = parameters.Get<bool>("IncludeSettings");

            if (includeMembers)
            {
                var client = Data.GetClient<TeamHttpClient>(parameters);
                Logger.Log($"Retrieving team membership information for team '{team.Name}'");

                var members = client.GetTeamMembersWithExtendedPropertiesAsync(team.ProjectName, team.Name)
                    .GetResult($"Error retrieving membership information for team {team.Name}");

                team.TeamMembers = members;
            }

            if (includeSettings)
            {
                Logger.Log($"Retrieving team settings for team '{team.Name}'");

                var workClient = Data.GetClient<WorkHttpClient>(parameters);
                var ctx = new TeamContext(team.ProjectName, team.Name);
                team.Settings = workClient.GetTeamSettingsAsync(ctx)
                    .GetResult($"Error retrieving settings for team {team.Name}");
            }

            return team;
        }

        [ImportingConstructor]
        public GetTeam(ICurrentConnections currentConnections, IPowerShellService powerShell, IDataManager data, ILogger logger)
         : base(powerShell, data, logger)
        {
            CurrentConnections = currentConnections;
        }
    }
}