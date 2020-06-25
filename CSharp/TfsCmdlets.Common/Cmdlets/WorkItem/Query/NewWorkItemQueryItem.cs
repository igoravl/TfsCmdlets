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
    [Cmdlet(VerbsCommon.New, "TfsWorkItemQuery", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(QueryHierarchyItem))]
    public class NewWorkItemQuery : NewWorkItemQueryItemCmdletBase
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
        /// Specifies the query definition text in WIQL (Work Item Query Language).
        /// </summary>
        [Parameter()]
        [Alias("Definition")]
        public string Wiql { get; set; }

        /// <inheritdoc/>
        protected override string ItemType => "Query";
    }

    /// <summary>
    /// Create a new work items query in the given Team Project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsWorkItemQueryFolder", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(QueryHierarchyItem))]
    public class NewWorkItemQueryFolder : NewWorkItemQueryItemCmdletBase
    {
        /// <summary>
        /// Specifies one or more saved queries to return. Wildcards supported. 
        /// When omitted, returns all saved queries in the given scope of the given team project.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [Alias("Path")]
        public string Folder { get; set; }

        /// <inheritdoc/>
        protected override string ItemType => "Folder";
    }

    /// <summary>
    /// Base implementation for New-WorkItemQuery and New-WorkItemQueryFolder
    /// </summary>
    public abstract class NewWorkItemQueryItemCmdletBase : NewCmdletBase<QueryHierarchyItem>
    {
        /// <summary>
        /// Specifies the scope of the returned item. Personal refers to the 
        /// "My Queries" folder", whereas Shared refers to the "Shared Queries" 
        /// folder. When omitted defaults to "Both", effectively searching for items 
        /// in both scopes.
        /// </summary>
        [Parameter()]
        [ValidateSet("Personal", "Shared")]
        public string Scope { get; set; } = "Personal";

        /// <summary>
        /// Allow the cmdlet to overwrite an existing item.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Indicates the type of item (query or folder)
        /// </summary>
        [Parameter()]
        protected abstract string ItemType { get; }
    }

    partial class WorkItemQueryDataService
    {
        protected override QueryHierarchyItem DoNewItem()
        {
            var itemType = GetParameter<string>("ItemType").ToLower();
            var isFolder = itemType.Equals("folder");

            var item = isFolder? GetParameter<string>("Folder"): GetParameter<string>("Query");
            var wiql = GetParameter<string>("Wiql");
            var scope = GetParameter<string>("Scope").Equals("Personal") ? "My Queries" : "Shared Queries";
            var force = GetParameter<bool>("Force");
            var (_, tp) = GetCollectionAndProject();

            var fullPath = NodeUtil.NormalizeNodePath(item, tp.Name, scope, includeScope: true, separator: '/');
            var queryName = Path.GetFileName(fullPath);
            var parentPath = Path.GetDirectoryName(fullPath);

            var existingItem = GetItem<QueryHierarchyItem>();

            if(existingItem != null && isFolder)
            {
                Log("Folder already exists.");

                if(!force) throw new Exception($"A folder with the specified name '{fullPath}' already exists.");

                return existingItem;
            }

            if (!ShouldProcess(tp, $"{(existingItem == null ? "Create" : "Overwrite")} " +
                $"work item {itemType} '{fullPath}'")) return null;

            var client = GetClient<WorkItemTrackingHttpClient>();

            var newItem = new QueryHierarchyItem()
            {
                Name = queryName,
                Path = parentPath,
                IsFolder = isFolder,
                Wiql = wiql
            };

            var parentFolder = GetItem<QueryHierarchyItem>(new{Folder=parentPath, ItemType="Folder"})??
                NewItem<QueryHierarchyItem>(new {Folder=parentPath, ItemType="Folder"});

            this.Log($"Creating query '{queryName}' in folder '{parentPath}'");

            var result = client.CreateQueryAsync(newItem, tp.Name, parentFolder.Id.ToString())
                .GetResult($"Error creating new work item {itemType} '{fullPath}'");

            return result;
        }
    }
}