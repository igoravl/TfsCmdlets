//HintName: TfsCmdlets.Cmdlets.Team.Board.GetTeamBoardCardRule.g.cs
namespace TfsCmdlets.Cmdlets.Team.Board
{
    [Cmdlet("Get", "TfsTeamBoardCardRule")]
    [OutputType(typeof(Microsoft.TeamFoundation.Work.WebApi.Rule))]
    public partial class GetTeamBoardCardRule: CmdletBase
    {
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