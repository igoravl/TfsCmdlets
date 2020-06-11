using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;
using TfsIdentity = TfsCmdlets.Services.Identity;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    public class TeamAdmin: TfsIdentity
    {
        internal TeamAdmin(TfsIdentity admin, WebApiTeam team) : base(admin)
        {
            this.AddNoteProperty("TeamId", team.Id);
            this.AddNoteProperty("ProjectId", team.ProjectId);
        }
    }
}