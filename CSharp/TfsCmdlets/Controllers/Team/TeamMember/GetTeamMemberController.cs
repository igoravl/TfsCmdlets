using TfsCmdlets.Cmdlets.Team.TeamAdmin;
using TfsCmdlets.Cmdlets.Team.TeamMember;
using TfsCmdlets.HttpClient;
using TfsCmdlets.Util;

namespace TfsCmdlets.Controllers.Team.TeamAdmin
{
    [CmdletController(typeof(Models.TeamMember))]
    partial class GetTeamMemberController
    {
        public override IEnumerable<Models.TeamMember> Invoke()
        {
            var team = Data.GetTeam(new { QueryMembership = true });
            var member = Parameters.Get<string>(nameof(GetTeamMember.Member));

            ErrorUtil.ThrowIfNotFound(team, nameof(team), Parameters.Get<object>(nameof(GetTeamMember.Team)));

            foreach (var m in team.TeamMembers.Where(m => 
                m.Identity.DisplayName.IsLike(member) || m.Identity.UniqueName.IsLike(member)))
            {
                yield return new Models.TeamMember(
                    Data.GetItem<Models.Identity>(new { Identity = m.Identity.Id }).InnerObject,
                    team);
            }
        }
    }
}