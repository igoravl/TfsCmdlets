using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.Tagging;

namespace TfsCmdlets.Controllers.WorkItem.Tagging
{
    /// <summary>
    /// Gets one or more work item tags.
    /// </summary>
    [CmdletController(typeof(WebApiTagDefinition))]
    partial class NewWorkItemTagController
    {
        public override IEnumerable<WebApiTagDefinition> Invoke()
        {
            var tag = Parameters.Get<string>(nameof(NewWorkItemTag.Tag));
            var tp = Data.GetProject();

            if (!PowerShell.ShouldProcess(tp, $"Create work item tag '{tag}'")) yield break;

            var client = Data.GetClient<Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient>();

            yield return client.CreateTagAsync(tp.Id, tag)
                .GetResult($"Error creating work item tag '{tag}'");
        }
    }
}