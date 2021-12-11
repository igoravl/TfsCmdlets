using System.Collections;
using System.Management.Automation;
using Microsoft.TeamFoundation.Work.WebApi;
using WebApiBoard = Microsoft.TeamFoundation.Work.WebApi.Board;

namespace TfsCmdlets.Cmdlets.Team.Board
{
    /// <summary>
    /// Set the card rule settings of the specified backlog board.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, NoAutoPipeline = true, SupportsShouldProcess = true, OutputType = typeof(BoardCardRuleSettings))]
    partial class SetTeamBoardCardRule
    {
        /// <summary>
        /// Specifies the board name. Wildcards are supported. When omitted, returns card rules 
        /// for all boards in the given team.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards()]
        public object WebApiBoard { get; set; }

        [Parameter(ParameterSetName = "Bulk set")]
        public BoardCardRuleSettings Rules { get; set; }

        [Parameter(ParameterSetName = "Set individual rules")]
        public string CardStyleRuleName { get; set; }

        [Parameter(ParameterSetName = "Set individual rules")]
        public string CardStyleRuleFilter { get; set; }

        [Parameter(ParameterSetName = "Set individual rules")]
        public Hashtable CardStyleRuleSettings { get; set; }

        [Parameter(ParameterSetName = "Set individual rules")]
        public string TagStyleRuleName { get; set; }

        [Parameter(ParameterSetName = "Set individual rules")]
        public string TagStyleRuleFilter { get; set; }

        [Parameter(ParameterSetName = "Set individual rules")]
        public Hashtable TagStyleRuleSettings { get; set; }
    }
}