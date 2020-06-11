using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Gets information about one or more teams.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeam", DefaultParameterSetName = "Get by team")]
    [OutputType(typeof(WebApiTeam))]
    public class GetTeam : BaseCmdlet<Team>
    {
        /// <summary>
        /// Specifies the team to return. Accepted values are its name, its ID, or a
        /// Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object. Wildcards are supported.
        /// When omitted, all teams in the given team project are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by team")]
        [Alias("Name")]
        [SupportsWildcards]
        public object Team { get; set; } = "*";

        /// <summary>
        /// Gets team members (fills the Members property with a list of
        /// Microsoft.VisualStudio.Services.WebApi.TeamMember objects).
        /// When omitted, only basic team information (such as name, description and ID) are returned.
        /// </summary>
        [Parameter]
        public SwitchParameter QueryMembership { get; set; }

        /// <summary>
        /// Gets the team's backlog settings (fills the Settings property with a
        /// Microsoft.TeamFoundation.Work.WebApi.TeamSetting object)
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeSettings { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline = true, ParameterSetName = "Get by team")]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(ParameterSetName = "Get by team")]
        public object Collection { get; set; }

        /// <summary>
        /// Returns the team specified in the last call to Connect-TfsTeam (i.e. the "current" team)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }

        /// <summary>
        /// Returns the default team in the given team project.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get default team")]
        public SwitchParameter Default { get; set; }
    }

    [Exports(typeof(Team))]
    internal class TeamDataService : BaseDataService<Team>
    {
        protected override IEnumerable<Team> DoGetItems()
        {
            var team = GetParameter<object>(nameof(GetTeam.Team));
            var current = GetParameter<bool>(nameof(GetTeam.Current));
            var includeMembers = GetParameter<bool>(nameof(GetTeam.QueryMembership));
            var includeSettings = GetParameter<bool>(nameof(GetTeam.IncludeSettings));
            var defaultTeam = GetParameter<bool>(nameof(GetTeam.Default));

            var (_, tp) = Cmdlet.GetCollectionAndProject();
            var client = GetClient<TeamHttpClient>();

            while (true) switch (team)
            {
                case PSObject pso:
                {
                    team = pso.BaseObject;
                    continue;
                }
                case Guid g:
                {
                    team = g.ToString();
                    continue;
                }
                case {} when defaultTeam:
                {
                    Logger.Log("Get default team");
                    team = tp.DefaultTeam.Id.ToString();
                    defaultTeam = false;
                    continue;
                }
                case null:
                case {} when current:
                {
                    Logger.Log("Get currently connected team");

                    if (CurrentConnections.Team == null) yield break;
                    team = CurrentConnections.Team.Id;
                    current = false;
                    continue;
                }
                case WebApiTeam t:
                {
                    team = t.Id.ToString();
                    continue;
                }
                case string s when !s.IsWildcard():
                {
                    var result = client.GetTeamAsync(tp.Name, s)
                        .GetResult($"Error getting team '{s}'");
                    yield return CreateTeamObject(result, includeMembers, includeSettings);
                    yield break;
                }
                case string s:
                {
                    foreach (var result in client.GetTeamsAsync(tp.Name)
                        .GetResult($"Error getting team(s) '{s}'")
                        .Where(t => t.Name.IsLike(s)))
                    {
                        yield return CreateTeamObject(result, includeMembers, includeSettings);
                    }
                    yield break;
                }
                default:
                {
                    throw new ArgumentException($"Invalid or non-existent team {team}");
                }
            }
        }

        private Team CreateTeamObject(WebApiTeam innerTeam, bool includeMembers, bool includeSettings)
        {
            var team = new Team(innerTeam);

            if (includeMembers)
            {
                var client = GetClient<TeamHttpClient>();
                Logger.Log($"Retrieving team membership information for team '{team.Name}'");

                var members = client.GetTeamMembersWithExtendedPropertiesAsync(team.ProjectName, team.Name)
                    .GetResult($"Error retrieving membership information for team {team.Name}");

                team.TeamMembers = members;
            }

            if (includeSettings)
            {
                Logger.Log($"Retrieving team settings for team '{team.Name}'");

                var workClient = GetClient<WorkHttpClient>();
                var ctx = new TeamContext(team.ProjectName, team.Name);
                team.Settings = workClient.GetTeamSettingsAsync(ctx)
                    .GetResult($"Error retrieving settings for team {team.Name}");
            }
            
            return team;
        }
    }
}