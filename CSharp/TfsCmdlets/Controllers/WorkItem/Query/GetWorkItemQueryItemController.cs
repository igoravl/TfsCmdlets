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

            var (myQueriesFolder, sharedQueriesFolder) = GetRootFolders(Project.Name, Scope, client, 0, QueryExpand.Minimal);

            switch (item)
            {
                case QueryHierarchyItem queryItem:
                    {
                        if (queryItem.IsFolder == isFolder) yield return queryItem;
                        yield break;
                    }
                case string s when isFolder && (string.IsNullOrEmpty(s) || s.Equals("/") || s.Equals("\\")):
                    {
                        if ((Scope & QueryItemScope.Personal) == QueryItemScope.Personal)
                        {
                            yield return myQueriesFolder;
                        }

                        if ((Scope & QueryItemScope.Shared) == QueryItemScope.Shared)
                        {
                            yield return sharedQueriesFolder;
                        }

                        yield break;
                    }
                case string s:
                    {
                        var path = NodeUtil.NormalizeNodePath(s, separator: '/');

                        if(path.StartsWith($"{myQueriesFolder.Name}/", System.StringComparison.OrdinalIgnoreCase))
                        {
                            Scope = QueryItemScope.Personal;
                            path = path.Substring(myQueriesFolder.Name.Length);
                        }
                        else if(path.StartsWith($"{sharedQueriesFolder.Name}/", System.StringComparison.OrdinalIgnoreCase))
                        {
                            Scope = QueryItemScope.Shared;
                            path = path.Substring(sharedQueriesFolder.Name.Length + 1);
                        }

                        var rootFolders = new List<QueryHierarchyItem>();

                        (myQueriesFolder, sharedQueriesFolder) = GetRootFolders(Project.Name, Scope, client, 2, QueryExpand.All);

                        if ((Scope & QueryItemScope.Personal) == QueryItemScope.Personal)
                        {
                            rootFolders.Add(myQueriesFolder);
                        }

                        if ((Scope & QueryItemScope.Shared) == QueryItemScope.Shared)
                        {
                            rootFolders.Add(sharedQueriesFolder);
                        }

                        foreach (var rootFolder in rootFolders)
                        {
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

        private (QueryHierarchyItem personal, QueryHierarchyItem shared) GetRootFolders(string projectName, QueryItemScope scope, WorkItemTrackingHttpClient client, int depth = 2, QueryExpand expand = QueryExpand.All)
        {
            var result = client.GetQueriesAsync(projectName, expand, depth)
                .GetResult("Error getting work item query root folders")
                .ToList();

            return (result.First(q => !(bool)q.IsPublic), result.First(q => (bool)q.IsPublic));
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
                var relativePath = c.Path.Substring(c.Path.IndexOf("/") + 1);
                var isMatch = relativePath.IsLike(pattern);

                if (isMatch && (!isFolder == queriesOnly)) yield return c;
            }

            var shouldRecurse = pattern.Contains("/") || pattern.Contains("**");

            if(!shouldRecurse) yield break;

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