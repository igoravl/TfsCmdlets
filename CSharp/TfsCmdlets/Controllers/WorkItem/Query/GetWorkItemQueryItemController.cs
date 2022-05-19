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

        protected override IEnumerable Run()
        {
            var isFolder = ItemType.Equals("Folder", System.StringComparison.OrdinalIgnoreCase);
            var item = Parameters.Get<object>(ItemType);
            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            switch (item)
            {
                case QueryHierarchyItem queryItem:
                    {
                        if (queryItem.IsFolder == isFolder) yield return queryItem;
                        yield break;
                    }
                case string s when isFolder && (string.IsNullOrEmpty(s) || s.Equals("/") || s.Equals("\\")):
                    {
                        var rootFolders = GetRootFolders(Project.Name, Scope, client, 0, QueryExpand.None);

                        if ((Scope & QueryItemScope.Personal) == QueryItemScope.Personal)
                        {
                            yield return rootFolders.First(f => !(bool)f.IsPublic);
                        }

                        if ((Scope & QueryItemScope.Shared) == QueryItemScope.Shared)
                        {
                            yield return rootFolders.First(f => (bool)f.IsPublic);
                        }

                        yield break;
                    }
                case string s:
                    {
                        var rootFolders = GetRootFolders(Project.Name, Scope, client);

                        foreach (var rootFolder in rootFolders)
                        {
                            var path = NodeUtil.NormalizeNodePath(s, Project.Name, rootFolder.Name, includeScope: false, separator: '/');

                            foreach (var c in GetItemsRecursively(rootFolder, path, Project.Name, !isFolder, client))
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

        private IEnumerable<QueryHierarchyItem> GetRootFolders(string projectName, QueryItemScope scope, WorkItemTrackingHttpClient client, int depth = 2, QueryExpand expand = QueryExpand.All)
        {
            var result = client.GetQueriesAsync(projectName, expand, depth)
                .GetResult("Error getting work item query root folders");

            var isPublic = (scope & QueryItemScope.Shared) == QueryItemScope.Shared;
            var isPrivate = (scope & QueryItemScope.Personal) == QueryItemScope.Personal;

            return result.Where(q => isPublic && ((bool)q.IsPublic) || isPrivate && (!(bool)q.IsPublic)).ToList();
        }

        private IEnumerable<QueryHierarchyItem> GetItemsRecursively(QueryHierarchyItem item, string pattern, string projectName, bool queriesOnly, WorkItemTrackingHttpClient client)
        {
            if ((item.HasChildren ?? false) && (item.Children == null || item.Children.ToList().Count == 0))
            {
                Logger.Log($"Fetching child nodes for node '{item.Path}'");

                item = client.GetQueryAsync(projectName, item.Path, QueryExpand.All, 2, false)
                    .GetResult($"Error retrieving folder from path '{item.Path}'");
            }

            if (item.Children == null) yield break;

            foreach (var c in item.Children)
            {
                var isFolder = c.IsFolder ?? false;
                var isMatch = c.Name.IsLike(pattern) || c.Path.IsLike(pattern) || c.Path.Substring(c.Path.IndexOf("/") + 1).IsLike(pattern);

                if (isMatch && (!isFolder == queriesOnly)) yield return c;
            }

            // var shouldRecurse = pattern.Contains("**") || (pattern.IndexOf("/") > 0);

            // if (!shouldRecurse) yield break;

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