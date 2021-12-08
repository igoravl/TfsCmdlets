using System.Management.Automation;
using WebApiCardRule = Microsoft.TeamFoundation.Work.WebApi.Rule;

namespace TfsCmdlets.Cmdlets.Team.Board
{
    /// <summary>
    /// Gets one or more team board card rules.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamBoardCardRule")]
    [OutputType(typeof(WebApiCardRule))]
    [TfsCmdlet(CmdletScope.Team, PipelineProperty = "Board")]
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
}