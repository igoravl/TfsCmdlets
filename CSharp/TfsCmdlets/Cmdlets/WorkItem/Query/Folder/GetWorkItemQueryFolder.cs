using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.Query.Folder
{
    /// <summary>
    /// Gets the definition of one or more work item saved queries.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(QueryHierarchyItem))]
    partial class GetWorkItemQueryFolder
    {
        /// <summary>
        /// Specifies one or more saved queries to return. Wildcards supported. 
        /// When omitted, returns all saved queries in the given scope of the given team project.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [SupportsWildcards()]
        [Alias("Path")]
        public object Folder { get; set; } = "**";

        /// <summary>
        /// Specifies the scope of the returned item. Personal refers to the 
        /// "My Queries" folder", whereas Shared refers to the "Shared Queries" 
        /// folder. When omitted defaults to "Both", effectively searching for items 
        /// in both scopes.
        /// </summary>
        [Parameter()]
        [ValidateSet("Personal", "Shared", "Both")]
        public string Scope { get; set; } = "Both";

        /// <summary>
        /// Returns deleted items.
        /// </summary>
        [Parameter()]
        public SwitchParameter Deleted { get; set; }

        [Parameter()]       
        internal string ItemType => "Folder";
    }
}