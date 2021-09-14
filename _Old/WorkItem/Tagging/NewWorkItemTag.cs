using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Creates a new work item tag.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsWorkItemTag", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTagDefinition))]
    public class NewWorkItemTag : NewCmdletBase<WebApiTagDefinition>
    {
        /// <summary>
        /// Specifies the name of the new tag.
        /// </summary>
        /// <value></value>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        public string Tag { get; set; }
    }

    partial class WorkItemTagDataService
    {
        protected override WebApiTagDefinition DoNewItem()
        {
            var tag = GetParameter<string>(nameof(NewWorkItemTag.Tag));
            var (_, tp) = GetCollectionAndProject();

            if (!ShouldProcess(tp, $"Create work item tag '{tag}'")) return null;

            var client = GetClient<TaggingHttpClient>();

            return client.CreateTagAsync(tp.Id, tag)
                .GetResult($"Error creating work item tag '{tag}'");
        }
    }
}