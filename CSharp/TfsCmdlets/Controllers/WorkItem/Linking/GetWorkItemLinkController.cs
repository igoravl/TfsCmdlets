using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Cmdlets.WorkItem.Linking;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;

namespace TfsCmdlets.Controllers.WorkItem.Linking
{
    [CmdletController(typeof(WorkItemRelation))]
    partial class GetWorkItemLinkController
    {
        [Import]
        private IKnownWorkItemLinkTypes KnownLinkTypes { get; set; }

        public override IEnumerable<WorkItemRelation> Invoke()
        {
            var wi = Data.GetItem<WebApiWorkItem>(new { IncludeLinks = true });

            if(wi.Relations == null) return Enumerable.Empty<WorkItemRelation>();

            var linkType = Parameters.Get<WorkItemLinkType>(nameof(GetWorkItemLink.LinkType));
            var includeAll = (linkType == WorkItemLinkType.All);
            var filteredTypes = KnownLinkTypes.LinkTypes.Where(kvp => (kvp.Key & linkType) == kvp.Key).Select(kvp => kvp.Value).ToList();

            return wi.Relations.Where(l => includeAll || filteredTypes.Contains(l.Rel)).ToList();
        }
    }
}
