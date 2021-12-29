using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using TfsCmdlets.Cmdlets.Team.Board;

namespace TfsCmdlets.Controllers.Team.Backlog
{
    [CmdletController(typeof(Models.CardRule))]
    partial class GetTeamBoardCardRuleController
    {
        public override IEnumerable<Models.CardRule> Invoke()
        {
            var board = Data.GetItem<Models.Board>();

            var tp = Data.GetProject();
            var t = Data.GetTeam();

            var rule = Parameters.Get<string>(nameof(GetTeamBoardCardRule.Rule));
            var ruleType = Parameters.Get<CardRuleType>(nameof(GetTeamBoardCardRule.RuleType));

            var ctx = new TeamContext(tp.Name, t.Name);
            var client = Data.GetClient<WorkHttpClient>();

            var rules = client.GetBoardCardRuleSettingsAsync(ctx, board.Name)
                .GetResult("Error getting board card rules")
                .rules;

            // Card rules

            foreach (var r in rules
                .Where(r1 =>
                    (((ruleType & CardRuleType.CardColor) == CardRuleType.CardColor) && r1.Key.Equals("fill")))
                .SelectMany(r1 => r1.Value))
            {
                yield return new Models.CardRule(r, board.InnerObject);
            }

            // Tag rules

            foreach (var r in rules
                .Where(r1 =>
                    (((ruleType & CardRuleType.TagColor) == CardRuleType.TagColor) && r1.Key.Equals("tagStyle")))
                .SelectMany(r1 => r1.Value))
            {
                yield return new Models.CardRule(r, board.InnerObject);
            }
        }
    }
}