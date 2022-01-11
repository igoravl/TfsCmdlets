using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using TfsCmdlets.Cmdlets.Team.Board;

namespace TfsCmdlets.Controllers.Team.Board
{
    [CmdletController(typeof(Models.CardRule))]
    partial class SetTeamBoardCardRuleController
    {
        protected override IEnumerable Run()
        {
            var board = Data.GetItem<Models.Board>(nameof(GetTeamBoard.Board));

            var bulkRules = Parameters.Get<BoardCardRuleSettings>(nameof(SetTeamBoardCardRule.Rules), new BoardCardRuleSettings());
            var cardStyleRuleName = Parameters.Get<string>(nameof(SetTeamBoardCardRule.CardStyleRuleName));
            var cardStyleRuleFilter = Parameters.Get<string>(nameof(SetTeamBoardCardRule.CardStyleRuleFilter));
            var cardStyleRuleSettings = Parameters.Get<Hashtable>(nameof(SetTeamBoardCardRule.CardStyleRuleSettings));
            var tagStyleRuleName = Parameters.Get<string>(nameof(SetTeamBoardCardRule.TagStyleRuleName));
            var tagStyleRuleFilter = Parameters.Get<string>(nameof(SetTeamBoardCardRule.TagStyleRuleFilter));
            var tagStyleRuleSettings = Parameters.Get<Hashtable>(nameof(SetTeamBoardCardRule.TagStyleRuleSettings));

            var tp = Data.GetProject();
            var t = Data.GetTeam();

            if (!PowerShell.ShouldProcess(board.Name, "Set board card rule settings")) yield break;

            var ctx = new TeamContext(tp.Name, t.Name);
            var client = Data.GetClient<WorkHttpClient>();

            var currentSettings = TaskExtensions.GetResult<BoardCardRuleSettings>(client.GetBoardCardRuleSettingsAsync(ctx, board.Name), "Error getting board card rules");

            if (Parameters.HasParameter(nameof(SetTeamBoardCardRule.Rules)))
            {
                // currentSettings.rules.CopyFrom(bulkRules);
            }
            else
            {
                // currentSettings.rules 
            }

            throw new NotImplementedException();
        }
    }
}