using TfsCmdlets.Cmdlets.Team.TeamAdmin;
using TfsCmdlets.Util;

namespace TfsCmdlets.Controllers.Team.TeamAdmin
{
    [CmdletController(typeof(Models.TeamAdmin))]
    partial class GetTeamAdminController
    {
        public override IEnumerable<Models.TeamAdmin> Invoke()
        {
            var team = Data.GetTeam(new { QueryMembership = true });
            var admin = Parameters.Get<string>(nameof(GetTeamAdmin.Admin));

            ErrorUtil.ThrowIfNotFound(team, nameof(team), Parameters.Get<object>(nameof(GetTeamAdmin.Team)));

            foreach (var m in team.TeamMembers.Where(m => m.IsTeamAdmin &&
                (m.Identity.DisplayName.IsLike(admin) || m.Identity.UniqueName.IsLike(admin))))
            {
                yield return new Models.TeamAdmin(
                    Data.GetItem<Models.Identity>(new { Identity = m.Identity.Id }).InnerObject,
                    team);
            }
        }
    }
}