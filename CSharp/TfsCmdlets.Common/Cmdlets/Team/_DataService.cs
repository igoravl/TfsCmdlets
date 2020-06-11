using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Team
{
    [Exports(typeof(WebApiTeam))]
    internal class TeamDataService : BaseDataService<WebApiTeam>
    {
        protected override IEnumerable<WebApiTeam> DoGetItems()
        {
            var team = GetParameter<object>("Team");
            var current = GetParameter<bool>("Current");

            if (team == null || current)
            {
                Logger.Log("Get currently connected team");

                var c = CurrentConnections.Team;
                if (c != null) yield return c;

                yield break;
            }

            var (tpc, tp) = Cmdlet.GetCollectionAndProject();
            var client = GetClient<Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient>();

            while (true)
            {
                switch (team)
                {
                    case WebApiTeam t:
                        {
                            yield return t;
                            yield break;
                        }
                    case Guid g:
                        {
                            team = g.ToString();
                            continue;
                        }
                    case string s when !s.IsWildcard():
                        {
                            yield return client.GetTeamAsync(tp.Name, s).GetResult($"Error getting team '{s}'");
                            yield break;
                        }
                    case string s:
                        {
                            foreach (var repo in client.GetTeamsAsync(tp.Name).GetResult($"Error getting team(ies) '{s}'").Where(r => r.Name.IsLike(s)))
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
}