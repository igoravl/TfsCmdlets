using TfsCmdlets.Cmdlets.Team.TeamAdmin;
using TfsCmdlets.Util;

namespace TfsCmdlets.Controllers.Team.TeamAdmin
{
    [CmdletController(typeof(Models.TeamAdmin))]
    partial class GetTeamAdminController
    {
        protected override IEnumerable Run()
        {
            var team = Data.GetTeam(new { IncludeMembers = true });
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