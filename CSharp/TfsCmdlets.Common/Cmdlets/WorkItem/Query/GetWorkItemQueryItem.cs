using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    /// <summary>
    /// Gets the definition of one or more work item saved queries.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemQuery")]
    [OutputType(typeof(QueryHierarchyItem))]
    public class GetWorkItemQuery : GetWorkItemQueryItemBase
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
        protected override string ItemType => "Query";
    }

    /// <summary>
    /// Gets the definition of one or more work item saved queries.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemQueryFolder")]
    [OutputType(typeof(QueryHierarchyItem))]
    public class GetWorkItemQueryFolder : GetWorkItemQueryItemBase
    {
        /// <summary>
        /// Specifies one or more saved queries to return. Wildcards supported. 
        /// When omitted, returns all saved queries in the given scope of the given team project.
        /// </summary>
        /// <value></value>
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [SupportsWildcards()]
        [Alias("Path")]
        public object Folder { get; set; } = "**";

        /// <inheritdoc/>
        protected override string ItemType => "Folder";
    }

    /// <summary>
    /// Base implementation for Get-WorkItemQuery and Get-WorkItemQueryFolder
    /// </summary>
    public abstract class GetWorkItemQueryItemBase : GetCmdletBase<QueryHierarchyItem>
    {
        /// <summary>
        /// Indicates the type of item (query or folder)
        /// </summary>
        [Parameter()]
        protected abstract string ItemType { get; }

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
        /// <value></value>
        [Parameter()]
        public SwitchParameter Deleted { get; set; }
    }

    [Exports(typeof(QueryHierarchyItem))]
    internal class QueryHierarchyItemDataService : BaseDataService<QueryHierarchyItem>
    {
        protected override IEnumerable<QueryHierarchyItem> DoGetItems()
        {
            var item = GetParameter<object>("Query", GetParameter<object>("Folder"));
            var itemType = GetParameter<string>("ItemType");
            var scope = GetParameter<string>(nameof(GetWorkItemQuery.Scope));

            var (_, tp) = GetCollectionAndProject();
            var client = GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();

            while (true) switch (item)
                {
                    case string s:
                        {
                            var result = client.GetQueriesAsync(tp.Name, QueryExpand.All, 2)
                                .GetResult("Error getting work item query items")
                                .Where(q => scope.Equals("Both") || q.IsPublic == scope.Equals("Shared"))
                                .ToList();

                            foreach (var q in result)
                            {
                                var path = NodeUtil.NormalizeNodePath(s, tp.Name, q.Name, includeScope: true, separator: '/');

                                foreach (var q1 in GetItemsRecursively(q, path, tp.Name, itemType.Equals("Query"), client))
                                {
                                    yield return q1;
                                }
                            }
                            yield break;

                        }
                    default: throw new ArgumentException($"Invalid or non-exixtent query/folder '{item}'");
                }
        }

        private IEnumerable<QueryHierarchyItem> GetItemsRecursively(QueryHierarchyItem item, string pattern, string projectName, bool queriesOnly, WorkItemTrackingHttpClient client)
        {
            if(item.HasChildren?? false && (item.Children == null || item.Children.ToList().Count == 0))
            {
                this.Log($"Fetching child nodes for node '{item.Path}'");

                item = client.GetQueryAsync(projectName, item.Path, QueryExpand.All, 2, false)
                    .GetResult( $"Error retrieving folder from path '{item.Path}'");
            }

            if(item.Children == null) yield break;

            foreach(var c in item.Children)
            {
                var isFolder = c.IsFolder?? false;

                if((c.Path.IsLike(pattern) || c.Name.IsLike(pattern)) && (!isFolder == queriesOnly)) yield return c;

                if(!isFolder) continue;

                foreach(var c1 in GetItemsRecursively(c, pattern, projectName, queriesOnly, client))
                    yield return c1;
            }
       }
    }
}