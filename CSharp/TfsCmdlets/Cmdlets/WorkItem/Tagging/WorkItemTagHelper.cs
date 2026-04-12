using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    internal static class WorkItemTagHelper
    {
        public static IEnumerable ToggleTag(
            IEnumerable<WebApiTagDefinition> tags,
            WebApiTeamProject tp,
            bool enabled,
            ITaggingHttpClient client,
            IPowerShellService powerShell)
        {
            foreach (var tag in tags)
            {
                if (!powerShell.ShouldProcess(tp, $"{(enabled ? "Enable" : "Disable")} work item tag '{tag.Name}'"))
                {
                    continue;
                }

                yield return client.UpdateTagAsync(tp.Id, tag.Id, tag.Name, enabled)
                    .GetResult($"Error renaming work item tag '{tag.Name}'");
            }
        }
    }
}
