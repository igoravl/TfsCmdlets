using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using TfsCmdlets.Cmdlets.Team.Board;

namespace TfsCmdlets.Controllers.Team.Board
{
    [CmdletController(typeof(Models.Board))]
    partial class GetTeamBoardController
    {
        public override IEnumerable<Models.Board> Invoke()
        {
            var board = Parameters.Get<object>(nameof(GetTeamBoard.Board));

            var tp = Data.GetProject();
            var t = Data.GetTeam();

            if (board is Guid g) board = g.ToString();

            switch (board)
            {
                case WebApiBoard b:
                    {
                        yield return new Models.Board(b, tp.Name, t.Name);
                        yield break;
                    }
                case string s when !s.IsGuid():
                    {
                        var ctx = new TeamContext(tp.Name, t.Name);
                        var client = Data.GetClient<WorkHttpClient>();

                        foreach (var b in TaskExtensions.GetResult<List<BoardReference>>(client.GetBoardsAsync(ctx), "Error getting team boards")
                            .Where(b => b.Name.IsLike(s)))
                        {
                            yield return new Models.Board(TaskExtensions.GetResult<Microsoft.TeamFoundation.Work.WebApi.Board>(client.GetBoardAsync(ctx, b.Id.ToString()), $"Error getting board '{b.Name}'"), tp.Name, t.Name);
                        }

                        yield break;
                    }
                case string s:
                    {
                        var ctx = new TeamContext(tp.Name, t.Name);
                        var client = Data.GetClient<WorkHttpClient>();

                        yield return new Models.Board(TaskExtensions.GetResult<Microsoft.TeamFoundation.Work.WebApi.Board>(client.GetBoardAsync(ctx, s), $"Error getting board 's'"), tp.Name, t.Name);

                        yield break;
                    }
                default:
                    {
                        Logger.LogError(new ArgumentException($"Invalid or non-existent board '{board}'"));
                        yield break;
                    }
            }
        }
    }
}