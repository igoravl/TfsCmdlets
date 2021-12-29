using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.Tagging;

namespace TfsCmdlets.Controllers.WorkItem.Tagging
{
    /// <summary>
    /// Gets one or more work item tags.
    /// </summary>
    [CmdletController(typeof(WebApiTagDefinition))]
    partial class RenameWorkItemTagController
    {
        public override IEnumerable<WebApiTagDefinition> Invoke()
        {
            var tag = Data.GetItem<WebApiTagDefinition>();
            var newName = Parameters.Get<string>(nameof(RenameWorkItemTag.NewName));

            var tp = Data.GetProject();
            var client = Data.GetClient<Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient>();

            if (!PowerShell.ShouldProcess(tp, $"Rename work item tag '{tag.Name}' to '{newName}'")) yield break;

            yield return client.UpdateTagAsync(tp.Id, tag.Id, newName, tag.Active)
                 .GetResult($"Error renaming work item tag '{tag.Name}'");
        }
    }
}