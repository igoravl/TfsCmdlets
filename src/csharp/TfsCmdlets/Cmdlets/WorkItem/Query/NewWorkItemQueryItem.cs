using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    /// <summary>
    /// Create a new work items query in the given Team Project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(QueryHierarchyItem))]
    partial class NewWorkItemQuery
    {
        /// <summary>
        /// Specifies one or more saved queries to return. Wildcards supported. 
        /// When omitted, returns all saved queries in the given scope of the given team project.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [Alias("Path")]
        public string Query { get; set; }

        /// <summary>
        /// Specifies the scope of the returned item. Personal refers to the 
        /// "My Queries" folder", whereas Shared refers to the "Shared Queries" 
        /// folder. When omitted defaults to "Both", effectively searching for items 
        /// in both scopes.
        /// </summary>
        [Parameter()]
        public QueryItemScope Scope { get; set; } = QueryItemScope.Personal;

        /// <summary>
        /// Specifies the query definition text in WIQL (Work Item Query Language).
        /// </summary>
        [Parameter]
        [Alias("Definition")]
        public string Wiql { get; set; }

        [Parameter]
        internal string ItemType => "Query";
    }
}