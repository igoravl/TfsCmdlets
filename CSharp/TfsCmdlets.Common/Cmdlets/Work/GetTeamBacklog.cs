using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Work.WebApi;
using WebApiBacklogLevelConfiguration = Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using Microsoft.TeamFoundation.Core.WebApi.Types;

namespace TfsCmdlets.Cmdlets.Work
{
    /// <summary>
    /// Gets information about one or more backlog levels of a given team.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamBacklogLevel")]
    [OutputType(typeof(WebApiBacklogLevelConfiguration))]
    public class GetTeamBacklogLevel : GetCmdletBase<Models.BacklogLevelConfiguration>
    {
        [Parameter(Position=0)]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Backlog { get; set; } = "*";

        /// <summary>
        /// Specifies one or more backlog level configurations to be returned. Valid values 
        /// are the name (e.g. "Stories") or the ID (e.g. "Microsoft.RequirementCategory") of the 
        /// backlog level to return. Wilcards are supported. When omitted, returns all backlogs 
        /// levels of the given team.
        /// </summary>
        [Parameter(Position=0)]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Backlog { get; set; } = "*";

        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(ValueFromPipeline=true)]
        public object Team { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public virtual object Project { get; set; }
    }

    [Exports(typeof(Models.BacklogLevelConfiguration))]
    internal partial class BacklogLevelConfigurationDataService : BaseDataService<Models.BacklogLevelConfiguration>
    {
        protected override IEnumerable<Models.BacklogLevelConfiguration> DoGetItems()
        {
            var backlog = GetParameter<object>(nameof(GetTeamBacklogLevel.Backlog));
            var (_, tp, t) = GetCollectionProjectAndTeam();

            while(true) switch(backlog)
            {
                case PSObject pso:
                {
                    backlog = pso.BaseObject;
                    continue;
                }
                case WebApiBacklogLevelConfiguration b:
                {
                    yield return new Models.BacklogLevelConfiguration(b, tp.Name, t.Name);
                    yield break;
                }
                case string s:
                {
                    var client = GetClient<WorkHttpClient>();
                    var ctx = new TeamContext(tp.Name, t.Name);

                    var result = client.GetBacklogsAsync(ctx)
                        .GetResult($"Error getting backlogs")
                        .Where(b => b.Name.IsLike(s) || b.Id.IsLike(s))
                        .OrderByDescending(b => b.Rank);

                    foreach(var b in result)
                    {
                        yield return new Models.BacklogLevelConfiguration(b, tp.Name, t.Name);
                    }

                    yield break;
                }
                default:
                {
                    throw new ArgumentException($"Invalid or non-existent backlog '{backlog}'");
                }
            }
        }
    }
}
