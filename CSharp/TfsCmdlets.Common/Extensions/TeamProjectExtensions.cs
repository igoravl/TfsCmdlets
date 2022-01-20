namespace TfsCmdlets.Extensions
{
    public static class TeamProjectExtensions
    {
        public static string ProcessTemplate(this WebApiTeamProject project)
        {
            return project.Capabilities["processTemplate"]?["templateName"]?.ToString();
        }
    }
}