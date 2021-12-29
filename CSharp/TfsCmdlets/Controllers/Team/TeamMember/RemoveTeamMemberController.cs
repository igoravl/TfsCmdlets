using TfsCmdlets.Cmdlets.Team.TeamAdmin;
using TfsCmdlets.HttpClient;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;

namespace TfsCmdlets.Controllers.Team.TeamAdmin
{
    [CmdletController(typeof(Models.Identity))]
    partial class RemoveTeamMemberController
    {
        public override IEnumerable<Models.Identity> Invoke()
        {
            throw new System.NotImplementedException();
        }
    }
}