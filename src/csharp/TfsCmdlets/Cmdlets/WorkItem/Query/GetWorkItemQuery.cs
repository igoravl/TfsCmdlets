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
        [ValidateNotNullOrEmpty()]
        [SupportsWildcards()]
        [Alias("Path")]
        public object Query { get; set; } = "**";

        /// <summary>
        /// Specifies the scope of the returned item. Personal refers to the 
        /// "My Queries" folder", whereas Shared refers to the "Shared Queries" 
        /// folder. When omitted defaults to "Both", effectively searching for items 
        /// in both scopes.
        /// </summary>
        [Parameter()]
        public QueryItemScope Scope { get; set; } = QueryItemScope.Both;

        /// <summary>
        /// Returns only deleted items.
        /// </summary>
        [Parameter()]
        public SwitchParameter Deleted { get; set; }

        [Parameter]
        internal string ItemType => "Query";
    }
}