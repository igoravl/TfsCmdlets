using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Cmdlets.WorkItem;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class SetWorkItemController
    {
        [Import]
        private IWorkItemPatchBuilder Builder { get; }

        protected override IEnumerable Run()
        {
            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            foreach (var wi in Items)
            {
                var result = client.UpdateWorkItemAsync(Builder.GetJson(wi), (int)wi.Id, false, BypassRules, SuppressNotifications)
                    .GetResult("Error updating work item");

                yield return result;
            }
        }
    }
}