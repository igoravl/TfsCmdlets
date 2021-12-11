using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    /// <summary>
    /// Gets the definition of one or more work item saved queries.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(QueryHierarchyItem))]
    partial class GetWorkItemQuery
    {
        /// <summary>
        /// Specifies one or more saved queries to return. Wildcards supported. 
        /// When omitted, returns all saved queries in the given scope of the given team project.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [SupportsWildcards()]
        [Alias("Path")]
        public object Query { get; set; } = "**";

        /// <inheritdoc/>
        internal string ItemType => "Query";
    }
}