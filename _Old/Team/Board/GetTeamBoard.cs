using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using WebApiBoard = Microsoft.TeamFoundation.Work.WebApi.Board;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Team.Board
{
    /// <summary>
    /// Gets one or more team boards.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamBoard")]
    [OutputType(typeof(WebApiBoard))]
    public class GetTeamBoard : GetCmdletBase<WebApiBoard>
    {
        /// <summary>
        /// Specifies the board name. Wildcards are supported. When omitted, returns all boards in 
        /// the given team.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Board { get; set; } = "*";

        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Team { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }
    }

    [Exports(typeof(WebApiBoard))]
    internal partial class BoardDataService : ControllerBase<WebApiBoard>
    {
        protected override IEnumerable<WebApiBoard> DoGetItems()
        {
            var board = GetParameter<object>(nameof(GetTeamBoard.Board));
            var (_, tp, t) = GetCollectionProjectAndTeam();

            while (true) switch (board)
                {
                    case WebApiBoard b:
                        {
                            yield return b;
                            yield break;
                        }
                    case Guid g:
                        {
                            board = g.ToString();
                            continue;
                        }
                    case string s when !s.IsGuid():
                        {
                            var ctx = new TeamContext(tp.Name, t.Name);
                            var client = GetClient<WorkHttpClient>();

                            foreach (var b in client.GetBoardsAsync(ctx)
                                .GetResult("Error getting team boards")
                                .Where(b => b.Name.IsLike(s)))
                            {
                                yield return client.GetBoardAsync(ctx, b.Id.ToString())
                                    .GetResult($"Error getting board '{b.Name}'");
                            }

                            yield break;
                        }
                    case string s:
                        {
                            var ctx = new TeamContext(tp.Name, t.Name);
                            var client = GetClient<WorkHttpClient>();

                            yield return client.GetBoardAsync(ctx, s)
                                .GetResult($"Error getting board 's'");

                            yield break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent board '{board}'");
                        }
                }
        }
    }
}