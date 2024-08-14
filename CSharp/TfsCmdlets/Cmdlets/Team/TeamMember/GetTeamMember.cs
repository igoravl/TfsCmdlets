using System.Management.Automation;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    /// <summary>
    /// Gets the members of a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, DataType = typeof(Models.TeamMember), OutputType = typeof(WebApiIdentity))]
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

    [CmdletController(typeof(Models.TeamMember))]
    partial class GetTeamMemberController
    {
        protected override IEnumerable Run()
        {
            var team = Data.GetTeam(includeMembers: true);
            var member = Parameters.Get<string>(nameof(GetTeamMember.Member));

            ErrorUtil.ThrowIfNotFound(team, nameof(team), Parameters.Get<object>(nameof(GetTeamMember.Team)));

            foreach (var m in Enumerable.Where<Microsoft.VisualStudio.Services.WebApi.TeamMember>(team.TeamMembers, m => 
                StringExtensions.IsLike(m.Identity.DisplayName, member) || StringExtensions.IsLike(m.Identity.UniqueName, member)))
            {
                yield return new Models.TeamMember(
                    Data.GetItem<Models.Identity>(new { Identity = m.Identity.Id }).InnerObject,
                    team);
            }
        }
    }
}