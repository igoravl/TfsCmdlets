using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    /// <summary>
    /// Restores a deleted work item query.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(QueryHierarchyItem))]
    partial class UndoWorkItemQueryRemoval
    {
        /// <summary>
        /// Specifies one or more saved queries to restore. Wildcards supported. 
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        [ValidateNotNull()]
        [SupportsWildcards()]
        [Alias("Path")]
        public object Query { get; set; }

        /// <summary>
        /// Specifies the scope of the item to restore. Personal refers to the 
        /// "My Queries" folder", whereas Shared refers to the "Shared Queries" 
        /// folder. When omitted defaults to "Both", effectively searching for items 
        /// in both scopes.
        /// </summary>
        [Parameter]
        public QueryItemScope Scope { get; set; } = QueryItemScope.Both;

        [Parameter]
        internal SwitchParameter Recursive => false;

        [Parameter]
        internal bool Deleted => true;

        [Parameter]
        internal string ItemType => "Query";
    }
}