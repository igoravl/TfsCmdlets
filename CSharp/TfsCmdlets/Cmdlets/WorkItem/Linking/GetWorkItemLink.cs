using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Gets the links in a work item.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WorkItemRelation))]
    partial class GetWorkItemLink
    {
        /// <summary>
        /// HELP_PARAM_WORKITEM
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Returns only the specified link types. When omitted, returns all link types.
        /// </summary>
        [Parameter]
        public WorkItemLinkType LinkType { get; set; }

    }

    [CmdletController(typeof(WorkItemRelation))]
    partial class GetWorkItemLinkController
    {
        [Import]
        private IKnownWorkItemLinkTypes KnownLinkTypes { get; set; }

        protected override IEnumerable Run()
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