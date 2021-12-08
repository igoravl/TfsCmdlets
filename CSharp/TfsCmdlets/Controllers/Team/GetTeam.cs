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
    [CmdletController(typeof(WebApiTeam))]
    partial class GetTeam
    {
        public ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<WebApiTeam> Invoke()
        {
            var team = Parameters.Get<object>(nameof(Cmdlets.Team.GetTeam.Team));
            var current = Parameters.Get<bool>(nameof(Cmdlets.Team.GetTeam.Current));
            var includeMembers = Parameters.Get<bool>(nameof(Cmdlets.Team.GetTeam.QueryMembership));
            var includeSettings = Parameters.Get<bool>(nameof(Cmdlets.Team.GetTeam.IncludeSettings));
            var defaultTeam = Parameters.Get<bool>(nameof(Cmdlets.Team.GetTeam.Default));

            var tp = Data.GetProject();
            var client = Data.GetClient<TeamHttpClient>();

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
                            var projectClient = Data.GetClient<ProjectHttpClient>();
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

            var includeMembers = Parameters.Get<bool>("IncludeMembers");
            var includeSettings = Parameters.Get<bool>("IncludeSettings");

            if (includeMembers)
            {
                var client = Data.GetClient<TeamHttpClient>(Parameters);
                Logger.Log($"Retrieving team membership information for team '{team.Name}'");

                var members = client.GetTeamMembersWithExtendedPropertiesAsync(team.ProjectName, team.Name)
                    .GetResult($"Error retrieving membership information for team {team.Name}");

                team.TeamMembers = members;
            }

            if (includeSettings)
            {
                Logger.Log($"Retrieving team settings for team '{team.Name}'");

                var workClient = Data.GetClient<WorkHttpClient>(Parameters);
                var ctx = new TeamContext(team.ProjectName, team.Name);
                team.Settings = workClient.GetTeamSettingsAsync(ctx)
                    .GetResult($"Error retrieving settings for team {team.Name}");
            }

            return team;
        }
    }
}