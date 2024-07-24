using System.Management.Automation;
using WebApiCardRule = Microsoft.TeamFoundation.Work.WebApi.Rule;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;

namespace TfsCmdlets.Cmdlets.Team.Board
{
    /// <summary>
    /// Gets one or more team board card rules.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, NoAutoPipeline = true, OutputType = typeof(WebApiCardRule))]
    partial class GetTeamBoardCardRule
    {
        /// <summary>
        /// Specifies the rule name. Wildcards are supported. 
        /// When omitted, returns all card rules in the given board.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        public object Rule { get; set; } = "*";

        /// <summary>
        /// Specifies the kind of rule to return. When omitted, returns 
        /// both rule types (card color and tag color).
        /// </summary>
        [Parameter]
        public CardRuleType RuleType { get; set; } = CardRuleType.All;

        /// <summary>
        /// Specifies the board name.
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Board { get; set; }
    }

    [CmdletController(typeof(Models.CardRule))]
    partial class GetTeamBoardCardRuleController
    {
        protected override IEnumerable Run()
        {
            var board = Data.GetItem<Models.Board>();

            var tp = Data.GetProject();
            var t = Data.GetTeam();

            var rule = Parameters.Get<string>(nameof(GetTeamBoardCardRule.Rule));
            var ruleType = Parameters.Get<CardRuleType>(nameof(GetTeamBoardCardRule.RuleType));

            var ctx = new TeamContext(tp.Name, t.Name);
            var client = Data.GetClient<WorkHttpClient>();

            var rules = TaskExtensions.GetResult<BoardCardRuleSettings>(client.GetBoardCardRuleSettingsAsync(ctx, board.Name), "Error getting board card rules")
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