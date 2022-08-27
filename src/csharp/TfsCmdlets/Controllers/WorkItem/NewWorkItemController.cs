using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class NewWorkItemController
    {
        [Import]
        private IWorkItemPatchBuilder Builder { get; }

        protected override IEnumerable Run()
        {
            var type = Data.GetItem<WebApiWorkItemType>();

            if (!PowerShell.ShouldProcess(Project, $"Create new '{type}' work item")) yield break;

            var wi = new WebApiWorkItem();
            wi.Fields["System.TeamProject"] = Project.Name;
            wi.Fields["System.WorkItemType"] = type.Name;

            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            var result = client.CreateWorkItemAsync(Builder.GetJson(wi), Project.Name, type.Name, false, BypassRules)
                .GetResult("Error creating work item");

            yield return result;
        }
    }
}