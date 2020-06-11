using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Gets information about one or more teams.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeam", DefaultParameterSetName = "Get by team")]
    [OutputType(typeof(WebApiTeam))]
    public class GetTeam : BaseCmdlet
    {
        [Parameter(Position = 0, ParameterSetName = "Get by team")]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Team { get; set; } = "*";

        [Parameter(ParameterSetName = "Get by team")]
        public SwitchParameter IncludeMembers { get; set; }

        [Parameter(ParameterSetName = "Get by team")]
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

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            if (Current)
            {
                WriteObject(CurrentConnections.Team);
                return;
            }

            if (!IncludeMembers && !IncludeSettings)
            {
                WriteObject(this.GetItems<WebApiTeam>(), true);
                return;
            }

            var (tpc, tp) = this.GetCollectionAndProject();
            var client = GetClient<Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient>();
            var workClient = GetClient<Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient>();

            foreach (var t in this.GetItems<WebApiTeam>())
            {
                var pso = new PSObject(t);

                if (IncludeMembers)
                {
                    this.Log($"Retrieving team membership information for team '{t.Name}'");

                    var members = client.GetTeamMembersWithExtendedPropertiesAsync(tp.Name, t.Name)
                        .GetResult($"Error retrieving membership information for team {t.Name}");

                    pso.AddNoteProperty("Members", members);

                }

                if (IncludeSettings.IsPresent)
                {
                    this.Log($"Retrieving team settings for team '{t.Name}'");

                    var ctx = new Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext(tp.Name, t.Name);
                    pso.AddNoteProperty("Settings", workClient.GetTeamSettingsAsync(ctx).GetResult($"Error retrieving settings for team {t.Name}"));
                }

                WriteObject(pso);
            }
        }
    }

    [Exports(typeof(WebApiTeam))]
    internal class TeamDataService : BaseDataService<WebApiTeam>
    {
        protected override IEnumerable<WebApiTeam> DoGetItems()
        {
            var team = GetParameter<object>("Team");
            var current = GetParameter<bool>("Current");
            var includeMembers = GetParameter<bool>("IncludeMembers");
            var includeSettings = GetParameter<bool>("IncludeSettings");
            var defaultTeam = GetParameter<bool>("Default");

            var (tpc, tp) = Cmdlet.GetCollectionAndProject();
            var client = GetClient<Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient>();

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
                    case object o when defaultTeam:
                        {
                            Logger.Log("Get default team");
                            team = tp.DefaultTeam.Id;
                            defaultTeam = false;
                            continue;
                        }
                    case object o when current:
                        {
                            Logger.Log("Get currently connected team");
                            yield return CurrentConnections.Team;
                            yield break;
                        }
                    case WebApiTeam t:
                        {
                            yield return t;
                            yield break;
                        }
                    case string s when !s.IsWildcard():
                        {
                            yield return client.GetTeamAsync(tp.Name, s).GetResult($"Error getting team '{s}'");
                            yield break;
                        }
                    case string s:
                        {
                            foreach (var repo in client.GetTeamsAsync(tp.Name)
                                .GetResult($"Error getting team(ies) '{s}'")
                                .Where(r => r.Name.IsLike(s)))
                            {
                                yield return repo;
                            }
                            yield break;
                        }
                    default:
                        {
                            throw new ArgumentException(nameof(team));
                        }
                }
        }
    }
}