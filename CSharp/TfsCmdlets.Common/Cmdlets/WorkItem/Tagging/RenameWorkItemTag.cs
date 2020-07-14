using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Renames a work item tag.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsWorkItemTag", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTagDefinition))]
    public class RenameWorkItemTag : RenameCmdletBase<WebApiTagDefinition>
    {
        /// <summary>
        /// Specifies the name of the work item tag to rename.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        public object Tag { get; set; }
    }

    partial class WorkItemTagDataService
    {
        protected override WebApiTagDefinition DoRenameItem()
        {
            var tag = GetItem<WebApiTagDefinition>(new { IncludeInactive = true });
            var force = GetParameter<bool>(nameof(RemoveWorkItemTag.Force));
            var newName = GetParameter<string>("NewName");

            var (_, tp) = GetCollectionAndProject();
            var client = GetClient<TaggingHttpClient>();

            if (!ShouldProcess(tp, $"Rename work item tag '{tag.Name}' to '{newName}'")) return null;

            return client.UpdateTagAsync(tp.Id, tag.Id, newName, tag.Active)
                .GetResult($"Error renaming work item tag '{tag.Name}'");
        }
    }
}