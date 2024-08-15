using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.Tagging;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Renames a work item tag.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiTagDefinition))]
    partial class RenameWorkItemTag
    {
        /// <summary>
        /// Specifies the name of the work item tag to rename.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        public object Tag { get; set; }
    }

    [CmdletController(typeof(WebApiTagDefinition), Client=typeof(ITaggingHttpClient))]
    partial class RenameWorkItemTagController
    {
        protected override IEnumerable Run()
        {
            var tag = Data.GetItem<WebApiTagDefinition>();
            var newName = Parameters.Get<string>(nameof(RenameWorkItemTag.NewName));

            var tp = Data.GetProject();

            if (!PowerShell.ShouldProcess(tp, $"Rename work item tag '{tag.Name}' to '{newName}'")) yield break;

            yield return Client.UpdateTagAsync(tp.Id, tag.Id, newName, tag.Active)
                 .GetResult($"Error renaming work item tag '{tag.Name}'");
        }
    }
}