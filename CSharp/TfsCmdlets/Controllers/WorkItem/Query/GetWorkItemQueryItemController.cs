using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Cmdlets.WorkItem.Query;

namespace TfsCmdlets.Controllers.WorkItem.Query
{
    [CmdletController(typeof(QueryHierarchyItem))]
    partial class GetWorkItemQueryController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        public override IEnumerable<QueryHierarchyItem> Invoke()
        {
            var itemType = Parameters.Get<string>("ItemType");
            var isFolder = itemType.Equals("Folder", System.StringComparison.OrdinalIgnoreCase);
            var item = Parameters.Get<object>(itemType);
            var scope = Parameters.Get<string>(nameof(GetWorkItemQuery.Scope));

            var tp = Data.GetProject();
            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            var rootFolders = GetRootFolders(tp.Name, scope, client);

            switch (item)
            {
                case QueryHierarchyItem queryItem:
                    {
                        if (queryItem.IsFolder == isFolder) yield return queryItem;
                        yield break;
                    }
                case string s when s.Equals("Personal") || s.Equals("Shared"):
                    {
                        yield return rootFolders.First();
                        yield break;
                    }
                case string s:
                    {
                        foreach (var rootFolder in rootFolders)
                        {
                            var path = NodeUtil.NormalizeNodePath(s, tp.Name, rootFolder.Name, includeScope: true, separator: '/');

                            foreach (var c in GetItemsRecursively(rootFolder, path, tp.Name, !isFolder, client))
                            {
                                yield return c;
                            }
                        }

                        yield break;
                    }
                default:
                    {
                        Logger.LogError(new ArgumentException($"Invalid or non-existent query/folder '{item}'"));
                        yield break;
                    }
            }
        }

        private IEnumerable<QueryHierarchyItem> GetRootFolders(string projectName, string scope, WorkItemTrackingHttpClient client)
        {
            var result = client.GetQueriesAsync(projectName, QueryExpand.All, 2)
                .GetResult("Error getting work item query root folders");

            var both = scope.Equals("Both");
            var isPublic = both || scope.Equals("Shared");
            var isPrivate = both || scope.Equals("Personal");

            return result.Where(q => q.IsPublic == isPublic || !q.IsPublic == isPrivate).ToList();
        }

        private IEnumerable<QueryHierarchyItem> GetItemsRecursively(QueryHierarchyItem item, string pattern, string projectName, bool queriesOnly, WorkItemTrackingHttpClient client)
        {
            if (!(item.HasChildren ?? false) && (item.Children == null || item.Children.ToList().Count == 0))
            {
                Logger.Log($"Fetching child nodes for node '{item.Path}'");

                item = client.GetQueryAsync(projectName, item.Path, QueryExpand.All, 2, false)
                    .GetResult($"Error retrieving folder from path '{item.Path}'");
            }

            if (item.Children == null) yield break;

            foreach (var c in item.Children)
            {
                var isFolder = c.IsFolder ?? false;

                if ((c.Path.IsLike(pattern) || c.Name.IsLike(pattern)) && (!isFolder == queriesOnly)) yield return c;
            }

            foreach (var c in item.Children)
            {
                var isFolder = c.IsFolder ?? false;

                if (!isFolder) continue;

                foreach (var c1 in GetItemsRecursively(c, pattern, projectName, queriesOnly, client))
                {
                    yield return c1;
                }
            }
        }
    }
}