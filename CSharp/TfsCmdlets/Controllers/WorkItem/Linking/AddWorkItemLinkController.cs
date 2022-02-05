using TfsCmdlets.Cmdlets.WorkItem.Linking;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace TfsCmdlets.Controllers.WorkItem.Linking
{
    /// <summary>
    /// Adds a link between two work items.
    /// </summary>
    [CmdletController(typeof(WebApiWorkItemRelation))]
    partial class AddWorkItemLinkController
    {
        [Import]
        private IKnownWorkItemLinkTypes KnownLinkTypes { get; set; }

        protected override IEnumerable Run()
        {
            {
                var sourceWi = Data.GetItem<WebApiWorkItem>();
                var targetWi = Data.GetItem<WebApiWorkItem>(new { WorkItem = Parameters.Get<object>(nameof(AddWorkItemLink.TargetWorkItem)) });
                var linkType = Parameters.Get<WorkItemLinkType>(nameof(AddWorkItemLink.LinkType));
                var runId = Guid.NewGuid();

                var patch = new JsonPatchDocument() {
                   new JsonPatchOperation() {
                        Operation = Operation.Test,
                        Path = "/rev",
                        Value = sourceWi.Rev
                   },
                   new JsonPatchOperation() {
                        Operation = Operation.Add,
                        Path = "/relations/-",
                        Value = new WebApiWorkItemRelation() {
                            Rel = KnownLinkTypes.GetReferenceName(linkType),
                            Url = targetWi.Url,
                            Attributes = new Dictionary<string,object>() {
                                ["comment"] = Parameters.Get<string>(nameof(AddWorkItemLink.Comment), string.Empty)
                            }
                        }
                   }
                };

                var client = Data.GetClient<WorkItemTrackingHttpClient>();
                var result = client.UpdateWorkItemAsync(patch, (int)sourceWi.Id)
                    .GetResult("Error updating target work item");

                return result.Relations.Where(r => 
                    r.Url == targetWi.Url && 
                    r.Rel == KnownLinkTypes.GetReferenceName(linkType)
                ).ToList();
            }
        }
    }
}
