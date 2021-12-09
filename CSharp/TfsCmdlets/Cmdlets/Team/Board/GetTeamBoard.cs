using System.Management.Automation;
using WebApiBoard = Microsoft.TeamFoundation.Work.WebApi.Board;

namespace TfsCmdlets.Cmdlets.Team.Board
{
    /// <summary>
    /// Gets one or more team boards.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamBoard")]
    [OutputType(typeof(WebApiBoard))]
    [TfsCmdlet(CmdletScope.Team)]
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