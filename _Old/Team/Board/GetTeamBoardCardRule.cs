using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiBoard = Microsoft.TeamFoundation.Work.WebApi.Board;
using WebApiCardRule = Microsoft.TeamFoundation.Work.WebApi.Rule;

namespace TfsCmdlets.Cmdlets.Team.Board
{
    /// <summary>
    /// Gets one or more team board card rules.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamBoardCardRule")]
    [OutputType(typeof(WebApiCardRule))]
    public class GetTeamBoardCardRule : GetCmdletBase<WebApiCardRule>
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
        [Parameter()]
        public CardRuleType RuleType { get; set; } = CardRuleType.All;

        /// <summary>
        /// Specifies the board name.
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Board { get; set; }

        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter()]
        public object Team { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }
    }

    [Exports(typeof(WebApiCardRule))]
    internal partial class BoardCardRuleDataService : ControllerBase<WebApiCardRule>
    {
        protected override IEnumerable<WebApiCardRule> DoGetItems()
        {
            var board = GetItem<WebApiBoard>();
            // TODO: Pipeline
            // var (_, tp, t) = GetCollectionProjectAndTeam(new ParameterDictionary() {
            //     ["Project"] = ((ReferenceLink)board.Links.Links["project"]).Href,
            //     ["Team"] = ((ReferenceLink)board.Links.Links["team"]).Href
            // });

            var rule = GetParameter<string>(nameof(GetTeamBoardCardRule.Rule));
            var ruleType = GetParameter<CardRuleType>(nameof(GetTeamBoardCardRule.RuleType));

            var (_, tp, t) = GetCollectionProjectAndTeam();
            var ctx = new TeamContext(tp.Name, t.Name);
            var client = GetClient<WorkHttpClient>();

            var rules = client.GetBoardCardRuleSettingsAsync(ctx, board.Name)
                .GetResult("Error getting board card rules")
                .rules;

            foreach (var r in rules
                .Where(r1 =>
                    (((ruleType & CardRuleType.CardColor) == CardRuleType.CardColor) && r1.Key.Equals("fill")))
                .SelectMany(r1 => r1.Value))
            {
                yield return new Models.CardRule(r, board);
            }

            foreach (var r in rules
                .Where(r1 =>
                    (((ruleType & CardRuleType.TagColor) == CardRuleType.TagColor) && r1.Key.Equals("tagStyle")))
                .SelectMany(r1 => r1.Value))
            {
                yield return new Models.CardRule(r, board);
            }
        }
    }
}
