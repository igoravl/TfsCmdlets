using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.Tagging;

namespace TfsCmdlets.Controllers.WorkItem.Tagging
{
    /// <summary>
    /// Gets one or more work item tags.
    /// </summary>
    [CmdletController(typeof(WebApiTagDefinition))]
    partial class RemoveWorkItemTagController
    {
        public override IEnumerable<WebApiTagDefinition> Invoke()
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