using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Deletes a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsTeam", SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTeam))]
    [TfsCmdlet(CmdletScope.Project)]
    partial class RemoveTeam 
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Team { get; set; }
    }
}