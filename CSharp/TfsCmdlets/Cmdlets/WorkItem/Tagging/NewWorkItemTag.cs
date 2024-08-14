using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.Tagging;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Creates a new work item tag.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiTagDefinition))]
    partial class NewWorkItemTag
    {
        /// <summary>
        /// Specifies the name of the new tag.
        /// </summary>
        /// <value></value>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        public string Tag { get; set; }
    }

    [CmdletController(typeof(WebApiTagDefinition), Client=typeof(ITaggingHttpClient))]
    partial class NewWorkItemTagController
    {
        protected override IEnumerable Run()
        {
            var tag = Parameters.Get<string>(nameof(NewWorkItemTag.Tag));
            var tp = Data.GetProject();

            if (!PowerShell.ShouldProcess(tp, $"Create work item tag '{tag}'")) yield break;

            yield return Client.CreateTagAsync(tp.Id, tag)
                .GetResult($"Error creating work item tag '{tag}'");
        }
    }
}