using System;
using System.IO;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    /// <summary>
    /// Create a new work items query in the given Team Project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(QueryHierarchyItem))]
    partial class NewWorkItemQueryFolder
    {
        /// <summary>
        /// Specifies one or more saved queries to return. Wildcards supported. 
        /// When omitted, returns all saved queries in the given scope of the given team project.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [Alias("Path")]
        public string Folder { get; set; }

        /// <summary>
        /// Specifies the scope of the returned item. Personal refers to the 
        /// "My Queries" folder", whereas Shared refers to the "Shared Queries" 
        /// folder. When omitted defaults to "Both", effectively searching for items 
        /// in both scopes.
        /// </summary>
        [Parameter()]
        [ValidateSet("Personal", "Shared", "Both")]
        public string Scope { get; set; } = "Both";

        [Parameter]
        internal string ItemType => "Folder";
    }
}