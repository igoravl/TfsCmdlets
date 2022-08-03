using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Extensions
{
    public static class TeamProjectExtensions
    {
        public static string ProcessTemplate(this WebApiTeamProject project)
        {
            return project.Capabilities["processTemplate"]?["templateName"]?.ToString();
        }

        public static string GetLink(this WebApiTeamProject project, string linkName)
        {
            if (!project.Links.Links.ContainsKey(linkName))
            {
                IDataManager data = ServiceLocator.Instance.GetExport<IDataManager>();
                project = data.GetItem<WebApiTeamProject>(new { Project = project.Id, IncludeDetails = true });
            }

            return ((ReferenceLink)project.Links.Links[linkName]).Href;
        }
    }
}