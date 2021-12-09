using Microsoft.TeamFoundation.Core.WebApi;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Renames a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsTeam", SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTeam))]
    [TfsCmdlet(CmdletScope.Project)]
    partial class RenameTeam 
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public object Team { get; set; }
    }
}