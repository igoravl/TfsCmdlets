using TfsCmdlets.Extensions;
using WebApiCardRule = Microsoft.TeamFoundation.Work.WebApi.Rule;
using WebApiBoard = Microsoft.TeamFoundation.Work.WebApi.Board;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Azure Boards card rule
    /// </summary>
    public class CardRule : ModelBase<WebApiCardRule>
    {
        /// <summary>
        /// Creates a new instance from the given object
        /// </summary>
        /// <param name="original">The original object to copy settings from</param>
        /// <param name="board">The board to apply this rule to</param>
        public CardRule(WebApiCardRule original, WebApiBoard board)
            : base(original)
        {
            Board = board;
            this.CopyFrom(original);
        }

        /// <summary>
        /// The board linked to this rule
        /// </summary>
        public WebApiBoard Board { get; set; }

        /// <summary>
        /// The name of the board linked to this rule
        /// </summary>
        public string BoardName => Board.Name;

        /// <summary>
        /// The type of the rule
        /// </summary>
        public CardRuleType RuleType => string.IsNullOrEmpty(InnerObject.filter)?
            CardRuleType.TagColor:
            CardRuleType.CardColor;
    }
}