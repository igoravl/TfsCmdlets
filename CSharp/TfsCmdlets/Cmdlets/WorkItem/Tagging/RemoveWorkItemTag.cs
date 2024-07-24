using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.Tagging;

namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    /// <summary>
    /// Deletes one or more work item tags.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true)]
    partial class RemoveWorkItemTag
    {
        /// <summary>
        /// Specifies one or more tags to delete. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Tag { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(WebApiTagDefinition))]
    partial class RemoveWorkItemTagController
    {
        protected override IEnumerable Run()
        {
            var tags = Data.GetItems<WebApiTagDefinition>();
            var force = Parameters.Get<bool>(nameof(RemoveWorkItemTag.Force));

            var tp = Data.GetProject();
            var client = Data.GetClient<Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient>();

            foreach (var t in tags)
            {
                if (!PowerShell.ShouldProcess(tp, $"Delete {((bool)t.Active ? "active" : "inactive")} work item tag '{t.Name}'")) continue;

                if (((bool)t.Active) && !force && !PowerShell.ShouldContinue($"The tag '{t.Name}' is currently in use. " +
                    "Are you sure you want to remove this tag?"))
                {
                    continue;
                }

                client.DeleteTagAsync(tp.Id, t.Id)
                    .Wait($"Error deleting tag '{t.Name}'");
            }

            return null;
        }
    }
}