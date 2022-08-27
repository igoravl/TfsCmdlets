using System.Management.Automation;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.TeamProject.Member
{
    /// <summary>
    /// Gets the members of a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(TeamProjectMember))]
    [OutputType(typeof(WebApiIdentity), ParameterSetName = new[] { "As Identity" })]
    partial class GetTeamProjectMember
    {
        /// <summary>
        /// Specifies the name of a team project member. Wildcards are supported. 
        /// When omitted, all team project members are returned.
        /// </summary>
        [Parameter()]
        public object Member { get; set; } = "*";

        /// <summary>
        /// Returns the members as fully resolved <see cref="WebApiIdentity"/> objects.
        /// When omitted, it returns only the name, ID and email of the users.
        /// </summary>
        [Parameter()]
        public SwitchParameter AsIdentity { get; set; }
    }
}