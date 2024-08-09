using System.Management.Automation;
using WebApiBacklogLevelConfiguration = Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;

namespace TfsCmdlets.Cmdlets.Team.Backlog
{
    /// <summary>
    /// Gets information about one or more backlog levels of a given team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, OutputType = typeof(WebApiBacklogLevelConfiguration))]
    partial class GetTeamBacklogLevel
    {
        /// <summary>
        /// Specifies one or more backlog level configurations to be returned. Valid values 
        /// are the name (e.g. "Stories") or the ID (e.g. "Microsoft.RequirementCategory") of the 
        /// backlog level to return. Wildcards are supported. When omitted, returns all backlogs 
        /// levels of the given team.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Backlog { get; set; } = "*";
    }

    [CmdletController(typeof(Models.BacklogLevelConfiguration), Client=typeof(IWorkHttpClient))]
    partial class GetTeamBacklogLevelController
    {
        protected override IEnumerable Run()
        {
            var backlog = Parameters.Get<object>(nameof(GetTeamBacklogLevel.Backlog), "*");
            var tp = Data.GetProject();
            var t = Data.GetTeam();

            switch (backlog)
            {
                case BacklogLevelConfiguration b:
                    {
                        yield return new Models.BacklogLevelConfiguration(b, tp.Name, t.Name);
                        yield break;
                    }
                case string s:
                    {
                        var ctx = new TeamContext(tp.Name, t.Name);

                        var result = Client.GetBacklogsAsync(ctx)
                            .GetResult($"Error getting backlogs")
                            .Where(b => b.Name.IsLike(s) || b.Id.IsLike(s))
                            .OrderByDescending(b => b.Rank);

                        foreach (var b in result)
                        {
                            yield return new Models.BacklogLevelConfiguration(b, tp.Name, t.Name);
                        }
                        yield break;
                    }
                default:
                    {
                        Logger.LogError(new ArgumentException($"Invalid backlog level {backlog}"));
                        yield break;
                    }
            }
        }
    }
}
