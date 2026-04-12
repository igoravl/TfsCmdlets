//HintName: TfsCmdlets.Cmdlets.Team.Board.SetTeamBoardCardRule.g.cs
namespace TfsCmdlets.Cmdlets.Team.Board
{
    [Cmdlet("Set", "TfsTeamBoardCardRule", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.Work.WebApi.BoardCardRuleSettings))]
    public partial class SetTeamBoardCardRule: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
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
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Alias("Organization")]
        [Parameter()]
        public object Collection { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}