using System.Management.Automation;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;

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

        /// <summary>
        /// Specifies the card rule settings as a single object. This way, all rules can be set at once.
        /// </summary>
        [Parameter(ParameterSetName = "Bulk set")]
        public BoardCardRuleSettings Rules { get; set; }

        /// <summary>
        /// Specifies the name of the card style rule.
        /// </summary>
        [Parameter(ParameterSetName = "Set individual rules")]
        public string CardStyleRuleName { get; set; }
        
        /// <summary>
        /// Specifies the filter for the card style rule.
        /// </summary>
        [Parameter(ParameterSetName = "Set individual rules")]
        public string CardStyleRuleFilter { get; set; }

        /// <summary>
        /// Specifies the settings for the card style rule.
        /// </summary>
        [Parameter(ParameterSetName = "Set individual rules")]
        public Hashtable CardStyleRuleSettings { get; set; }

        /// <summary>
        /// Specifies the name of the tag style rule.
        /// </summary>
        [Parameter(ParameterSetName = "Set individual rules")]
        public string TagStyleRuleName { get; set; }

        /// <summary>
        /// Specifies the filter for the tag style rule.
        /// </summary>
        [Parameter(ParameterSetName = "Set individual rules")]
        public string TagStyleRuleFilter { get; set; }

        /// <summary>
        /// Specifies the settings for the tag style rule.
        /// </summary>
        [Parameter(ParameterSetName = "Set individual rules")]
        public Hashtable TagStyleRuleSettings { get; set; }
    }

    [CmdletController(typeof(Models.CardRule))]
    partial class SetTeamBoardCardRuleController
    {
        protected override IEnumerable Run()
        {
            // var board = Data.GetItem<Models.Board>(nameof(GetTeamBoard.Board));

            // var tp = Data.GetProject();
            // var t = Data.GetTeam();

            // if (!PowerShell.ShouldProcess(board.Name, "Set board card rule settings")) yield break;

            // var ctx = new TeamContext(tp.Name, t.Name);
            // var client = Data.GetClient<WorkHttpClient>();

            // var currentSettings = TaskExtensions.GetResult<BoardCardRuleSettings>(client.GetBoardCardRuleSettingsAsync(ctx, board.Name), "Error getting board card rules");

            // if (Parameters.HasParameter(nameof(SetTeamBoardCardRule.Rules)))
            // {
            //     // currentSettings.rules.CopyFrom(bulkRules);
            // }
            // else
            // {
            //     // currentSettings.rules 
            // }

            throw new NotImplementedException();
        }
    }
}