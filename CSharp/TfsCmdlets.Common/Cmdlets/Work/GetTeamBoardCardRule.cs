using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Work
{
    /// <summary>
    /// Gets one or more team board card rules.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamBoardCardRule")]
    [OutputType(typeof(BoardCardRuleSettings))]
    public class GetTeamBoardCardRule : GetCmdletBase<BoardCardRuleSettings>
    {
        /// <summary>
        /// Specifies the board name. Wildcards are supported. When omitted, returns card rules 
        /// for all boards in the given team.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
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

    [Exports(typeof(BoardCardRuleSettings))]
    internal partial class BoardCardRuleSettingsDataService : BaseDataService<BoardCardRuleSettings>
    {
        protected override IEnumerable<BoardCardRuleSettings> DoGetItems()
        {
            var boards = GetItems<Board>();
            var (_, tp, t) = GetCollectionProjectAndTeam();
            var ctx = new TeamContext(tp.Name, t.Name);
            var client = GetClient<WorkHttpClient>();

            foreach(var b in boards)
            {
                yield return client.GetBoardCardRuleSettingsAsync(ctx, b.Name)
                    .GetResult("Error getting board card rules");
            }
        }
    }
}
