using System.Management.Automation;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.TeamProject.Member
{
    /// <summary>
    /// Gets the members of a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(TeamProjectMember))]
    partial class GetTeamProjectMember
    {
        /// <summary>
        /// Specifies the name of a team project member. Wildcards are supported. 
        /// When omitted, all team project members are returned.
        /// </summary>
        [Parameter()]
        public object Member { get; set; } = "*";
    }
}