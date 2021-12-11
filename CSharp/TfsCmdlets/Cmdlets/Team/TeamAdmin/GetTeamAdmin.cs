using System.Management.Automation;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Gets the administrators of a team.
    /// </summary>    
    [TfsCmdlet(CmdletScope.Team, OutputType = typeof(WebApiIdentity))]
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