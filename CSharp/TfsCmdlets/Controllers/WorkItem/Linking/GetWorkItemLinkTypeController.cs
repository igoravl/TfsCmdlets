using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Cmdlets.WorkItem.Linking;

namespace TfsCmdlets.Controllers.WorkItem.Linking
{
    /// <summary>
    /// Gets the work item link end types of a team project collection.
    /// </summary>
    [CmdletController(typeof(WorkItemRelationType))]
    partial class GetWorkItemLinkTypeController
    {
        public override IEnumerable<WorkItemRelationType> Invoke()
        {
            var linkType = Parameters.Get<object>(nameof(GetWorkItemLinkType.LinkType));

            switch (linkType)
            {
                case WorkItemRelationType wirt:
                    {
                        return new[] { wirt };
                    }
                case string s:
                    {
                        var client = Data.GetClient<WorkItemTrackingHttpClient>();

                        return client.GetRelationTypesAsync()
                            .GetResult("Error getting work item link types")
                            .Where(wirt => wirt.Name.IsLike(s) || wirt.ReferenceName.IsLike(s));
                    }
                default:
                    {
                        Logger.LogError($"Invalid or non-existent link type: {linkType}");
                        return Enumerable.Empty<WorkItemRelationType>();
                    }
            }

        }
    }
}