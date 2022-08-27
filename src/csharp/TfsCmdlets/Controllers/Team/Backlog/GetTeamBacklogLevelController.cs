using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using TfsCmdlets.Cmdlets.Team.Backlog;

namespace TfsCmdlets.Controllers.Team.Backlog
{
    [CmdletController(typeof(Models.BacklogLevelConfiguration))]
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
                        var client = Data.GetClient<WorkHttpClient>();
                        var ctx = new TeamContext(tp.Name, t.Name);

                        var result = client.GetBacklogsAsync(ctx)
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