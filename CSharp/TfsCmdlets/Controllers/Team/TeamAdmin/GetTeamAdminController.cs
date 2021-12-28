using TfsCmdlets.Cmdlets.Team.TeamAdmin;
using TfsCmdlets.HttpClient;
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

            foreach (var member in team.TeamMembers.Where(m => m.IsTeamAdmin))
            {
                var a = new Models.TeamAdmin(Data.GetItem<Models.Identity>(new
                {
                    Identity = member.Identity.Id
                }), team);

                if (a.DisplayName.IsLike(admin) || a.UniqueName.IsLike(admin))
                {
                    yield return a;
                }
            }
        }
    }
}