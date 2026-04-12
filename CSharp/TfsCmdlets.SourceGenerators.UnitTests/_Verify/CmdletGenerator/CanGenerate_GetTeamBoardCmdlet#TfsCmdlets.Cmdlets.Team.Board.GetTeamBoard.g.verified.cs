//HintName: TfsCmdlets.Cmdlets.Team.Board.GetTeamBoard.g.cs
namespace TfsCmdlets.Cmdlets.Team.Board
{
    [Cmdlet("Get", "TfsTeamBoard")]
    [OutputType(typeof(Microsoft.TeamFoundation.Work.WebApi.Board))]
    public partial class GetTeamBoard: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(ValueFromPipeline=true)]
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