using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using WebApiIdentityRef = Microsoft.VisualStudio.Services.WebApi.IdentityRef;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Extensions
{
    internal static class TeamProjectExtensions
    {
        public static string ProcessTemplate(this WebApiTeamProject project)
        {
            return project.Capabilities["processTemplate"]?["templateName"]?.ToString();
        }
    }
}