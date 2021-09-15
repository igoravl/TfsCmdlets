using System.Management.Automation;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;
using TfsTeamAdmin = TfsCmdlets.Cmdlets.Team.TeamAdmin.TeamAdmin;
using TfsCmdlets.Services;
using TfsCmdlets.Util;
using System.Linq;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Gets the administrators of a team.
    /// </summary>    
    [Cmdlet(VerbsCommon.Get, "TfsTeamAdmin")]
    [OutputType(typeof(WebApiIdentity))]
    public class GetTeamAdmin : CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Team { get; set; }

        /// <summary>
        /// Specifies the administrator to get from the given team. Wildcards are supported.
        /// When omitted, all administrators are returned.
        /// </summary>
        [Parameter(Position = 1)]
        [SupportsWildcards()]
        public string Admin { get; set; } = "*";

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        // TODO

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        //    protected override void DoProcessRecord()
        //    {
        //        WriteItems<TfsTeamAdmin>();
        //    }
        //}

        //[Exports(typeof(TeamAdmin))]
        //internal class TeamAdminDataService : CollectionLevelController<TfsTeamAdmin>
        //{
        //    protected override System.Collections.Generic.IEnumerable<TfsTeamAdmin> DoGetItems()
        //    {
        //        var admin = parameters.Get<string>(nameof(GetTeamAdmin.Admin));
        //        var team = GetItem<Models.Team>(new
        //        {
        //            QueryMembership = true
        //        });

        //        ErrorUtil.ThrowIfNotFound(team, nameof(team), GetParameter<object>(nameof(GetTeamAdmin.Team)));

        //        foreach (var member in team.TeamMembers.Where(m => m.IsTeamAdmin))
        //        {
        //            var a = new TeamAdmin(GetItem<Models.Identity>(new
        //            {
        //                Identity = member.Identity.Id
        //            }), team);

        //            if (a.DisplayName.IsLike(admin) || a.UniqueName.IsLike(admin))
        //            {
        //                yield return a;
        //            }
        //        }
        //    }
    }
}