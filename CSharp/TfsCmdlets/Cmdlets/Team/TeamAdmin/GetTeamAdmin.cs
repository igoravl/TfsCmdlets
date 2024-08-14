using System.Management.Automation;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Gets the administrators of a team.
    /// </summary>    
    [TfsCmdlet(CmdletScope.Team, DataType = typeof(Models.TeamAdmin), OutputType = typeof(WebApiIdentity))]
    partial class GetTeamAdmin
    {
        /// <summary>
        /// Specifies the administrator to get from the given team. Wildcards are supported.
        /// When omitted, all administrators are returned.
        /// </summary>
        [Parameter(Position = 1)]
        [SupportsWildcards()]
        public string Admin { get; set; } = "*";
    }

    [CmdletController(typeof(Models.TeamAdmin))]
    partial class GetTeamAdminController
    {
        protected override IEnumerable Run()
        {
            var team = Data.GetTeam(includeMembers: true);
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