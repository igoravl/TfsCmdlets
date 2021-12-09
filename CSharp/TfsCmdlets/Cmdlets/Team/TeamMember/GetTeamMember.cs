using System.Management.Automation;
using TfsQueryMembership = Microsoft.VisualStudio.Services.Identity.QueryMembership;

namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    /// <summary>
    /// Gets the members of a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamMember")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    [TfsCmdlet(CmdletScope.Team)]
    partial class GetTeamMember
    {
        /// <summary>
        /// Specifies the member (user or group) to get from the given team. Wildcards are supported.
        /// When omitted, all team members are returned.
        /// </summary>
        [Parameter(Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Member { get; set; } = "*";

        /// <summary>
        /// Recursively expands all member groups, returning the users and/or groups contained in them
        /// </summary>
        [Parameter]
        public SwitchParameter Recurse { get; set; }
    }
}