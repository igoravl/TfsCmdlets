using TfsCmdlets.Cmdlets.Team.TeamMember;
using TfsCmdlets.Util;

namespace TfsCmdlets.Controllers.Team.TeamMember
{
    [CmdletController(typeof(Models.TeamMember))]
    partial class GetTeamMemberController
    {
        protected override IEnumerable Run()
        {
            var team = Data.GetTeam(new { QueryMembership = true });
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