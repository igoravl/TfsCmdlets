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

namespace TfsCmdlets.Cmdlets.Team.Backlog
{
    /// <summary>
    /// Gets information about one or more backlog levels of a given team.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamBacklogLevel")]
    [OutputType(typeof(WebApiBacklogLevelConfiguration))]
    public class GetTeamBacklogLevel : CmdletBase
    {
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
        public object Project { get; set; }
    }

    // TODO

    //[Exports(typeof(Models.BacklogLevelConfiguration))]
    //internal partial class BacklogLevelConfigurationDataService : BaseController<Models.BacklogLevelConfiguration>
    //{
    //    protected override IEnumerable<Models.BacklogLevelConfiguration> DoGetItems()
    //    {
    //        var backlog = parameters.Get<object>(nameof(GetTeamBacklogLevel.Backlog));
    //        var (_, tp, t) = GetCollectionProjectAndTeam();

    //        while(true) switch(backlog)
    //        {
    //            case WebApiBacklogLevelConfiguration b:
    //            {
    //                yield return new Models.BacklogLevelConfiguration(b, tp.Name, t.Name);
    //                yield break;
    //            }
    //            case string s:
    //            {
    //                var client = GetClient<WorkHttpClient>();
    //                var ctx = new TeamContext(tp.Name, t.Name);

    //                var result = client.GetBacklogsAsync(ctx)
    //                    .GetResult($"Error getting backlogs")
    //                    .Where(b => b.Name.IsLike(s) || b.Id.IsLike(s))
    //                    .OrderByDescending(b => b.Rank);

    //                foreach(var b in result)
    //                {
    //                    yield return new Models.BacklogLevelConfiguration(b, tp.Name, t.Name);
    //                }

    //                yield break;
    //            }
    //            default:
    //            {
    //                throw new ArgumentException($"Invalid or non-existent backlog '{backlog}'");
    //            }
    //        }
    //    }
    //}
}
