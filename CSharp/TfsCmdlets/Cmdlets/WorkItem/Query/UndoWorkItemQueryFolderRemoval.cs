using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    /// <summary>
    /// Restores a deleted work item query folder.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(QueryHierarchyItem))]
    partial class UndoWorkItemQueryFolderRemoval
    {
        /// <summary>
        /// Specifies one or more query folders to restore. Wildcards supported. 
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        [ValidateNotNull()]
        [SupportsWildcards()]
        [Alias("Path")]
        public object Folder { get; set; }

        /// <summary>
        /// Specifies the scope of the item to restore. Personal refers to the 
        /// "My Queries" folder", whereas Shared refers to the "Shared Queries" 
        /// folder. When omitted defaults to "Both", effectively searching for items 
        /// in both scopes.
        /// </summary>
        [Parameter]
        public QueryItemScope Scope { get; set; } = QueryItemScope.Both;

        /// <summary>
        /// Restores the specified query folder and all its descendants. 
        /// When omitted, the specified folder is restored but not its contents (queries and/or sub-folders).
        /// </summary>
        [Parameter]
        public SwitchParameter Recursive {get;set;}

        [Parameter]
        internal bool Deleted => true;

        [Parameter]
        internal string ItemType => "Folder";
    }

    // See UndoWorkItemQueryItemRemovalController
}