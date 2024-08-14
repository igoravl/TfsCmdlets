using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Gets the work item link end types of a team project collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection)]
    partial class GetWorkItemLinkType
    {
        /// <summary>
        /// Specifies the name of the link type. Wildcards are supported.
        /// When omitted, returns all link types.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards]
        [Alias("Name", "EndLinkType", "Type", "Link")]
        public object LinkType { get; set; } = "*";
    }

    [CmdletController(typeof(WorkItemRelationType), Client=typeof(IWorkItemTrackingHttpClient))]
    partial class GetWorkItemLinkTypeController
    {
        protected override IEnumerable Run()
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
                        return Client.GetRelationTypesAsync()
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