using Microsoft.TeamFoundation.Core.WebApi;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Represents an administrator of a Team Foundation / Azure DevOps Team
    /// </summary>
    public class TeamAdmin : Identity
    {
        public TeamAdmin(WebApiIdentity obj, WebApiTeam team) : base(obj, team)
        {
        }
    }
}