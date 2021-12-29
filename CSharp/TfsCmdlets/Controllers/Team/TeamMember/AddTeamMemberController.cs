using TfsCmdlets.Cmdlets.Team.TeamAdmin;
using TfsCmdlets.Cmdlets.Team.TeamMember;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Controllers.Team.TeamAdmin
{
    [CmdletController(typeof(Models.TeamMember))]
    partial class AddTeamMemberController
    {
        public override IEnumerable<Models.TeamMember> Invoke()
        {
            throw new System.NotImplementedException();
        }
    }
}
