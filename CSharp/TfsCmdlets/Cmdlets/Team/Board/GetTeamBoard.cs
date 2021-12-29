using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Team.Board
{
    /// <summary>
    /// Gets one or more team boards.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, OutputType = typeof(WebApiBoard))]
    partial class GetTeamBoard
    {
        /// <summary>
        /// Specifies the board name. Wildcards are supported. When omitted, returns all boards in 
        /// the given team.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Board { get; set; } = "*";
    }
}