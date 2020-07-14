using TfsCmdlets.Extensions;
using WebApiCardRule = Microsoft.TeamFoundation.Work.WebApi.Rule;
using WebApiBoard = Microsoft.TeamFoundation.Work.WebApi.Board;

namespace TfsCmdlets.Models
{
    public class CardRule : WebApiCardRule
    {
        public CardRule(WebApiCardRule original, WebApiBoard board)
        {
            Board = board;
            this.CopyFrom(original);
        }

        public WebApiBoard Board { get; set; }

        public string BoardName => Board.Name;

        public CardRuleType RuleType => string.IsNullOrEmpty(this.filter)?
            CardRuleType.TagColor:
            CardRuleType.CardColor;
    }
}