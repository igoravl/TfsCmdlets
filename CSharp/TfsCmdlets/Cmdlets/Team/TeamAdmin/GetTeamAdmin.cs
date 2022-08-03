using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Gets the administrators of a team.
    /// </summary>    
    [TfsCmdlet(CmdletScope.Team, DataType = typeof(Models.TeamAdmin), OutputType = typeof(WebApiIdentity))]
    partial class GetTeamAdmin
    {
        /// <summary>
        /// Specifies the administrator to get from the given team. Wildcards are supported.
        /// When omitted, all administrators are returned.
        /// </summary>
        [Parameter(Position = 1)]
        [SupportsWildcards()]
        public string Admin { get; set; } = "*";
    }
}