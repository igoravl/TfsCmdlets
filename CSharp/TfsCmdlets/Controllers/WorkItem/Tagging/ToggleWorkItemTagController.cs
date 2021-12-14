using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.Tagging;

namespace TfsCmdlets.Controllers.WorkItem.Tagging
{
    /// <summary>
    /// Gets one or more work item tags.
    /// </summary>
    [CmdletController(typeof(WebApiTagDefinition))]
    partial class ToggleWorkItemTagController
    {
        public override IEnumerable<WebApiTagDefinition> Invoke()
        {
            var tags = Data.GetItems<WebApiTagDefinition>();
            var tp = Data.GetProject();
            var enabled = Parameters.Get<bool>("Enabled");
            var client = Data.GetClient<Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient>();

            foreach (var tag in tags)
            {
                if (!PowerShell.ShouldProcess(tp, $"{(enabled? "Enable": "Disable")} work item tag '{tag.Name}'")) continue;

                yield return client.UpdateTagAsync(tp.Id, tag.Id, tag.Name, enabled)
                    .GetResult($"Error renaming work item tag '{tag.Name}'");
            }
        }
    }
}