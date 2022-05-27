using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Cmdlets.WorkItem.Query;

namespace TfsCmdlets.Controllers.WorkItem.Query
{
    [CmdletController(typeof(QueryHierarchyItem))]
    partial class NewWorkItemQueryController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        protected override IEnumerable Run()
        {
            var itemType = Parameters.Get<string>("ItemType");
            var isFolder = itemType.Equals("Folder", System.StringComparison.OrdinalIgnoreCase);
            var item = Parameters.Get<string>(itemType);
            var tp = Data.GetProject();
            var client = GetClient<WorkItemTrackingHttpClient>();

            var root = GetItem<QueryHierarchyItem>(new { Folder = @"\" });
            var fullPath = NodeUtil.NormalizeNodePath(item, tp.Name, root.Name, includeScope: false, separator: '/');
            var itemExists = Data.TryGetItem<QueryHierarchyItem>(out var existingItem);

            if (itemExists && isFolder)
            {
                Logger.LogWarn($"A folder with the specified name '{fullPath}' already exists. Ignoring.");
                yield return existingItem;
                yield break;
            }

            if (!PowerShell.ShouldProcess(tp, $"{(itemExists ? "Create" : "Overwrite")} " +
                $"work item query{(isFolder ? " folder" : "")} '{fullPath}'"))
            {
                yield break;
            }

            var queryName = fullPath.Substring(fullPath.LastIndexOf('/') + 1);

            var parentPath = fullPath.Equals(queryName) ?
                string.Empty :
                fullPath.Substring(0, fullPath.Length - queryName.Length - 1);

            var newItem = new QueryHierarchyItem()
            {
                Name = queryName,
                Path = parentPath,
                IsFolder = isFolder,
                Wiql = Wiql
            };

            var parentFolder = string.IsNullOrEmpty(parentPath) ? root :
                Data.TestItem<QueryHierarchyItem>(new { Folder = parentPath, ItemType = "Folder" }) ?
                    Data.GetItem<QueryHierarchyItem>(new { Folder = parentPath, ItemType = "Folder" }) :
                    Data.NewItem<QueryHierarchyItem>(new { Folder = parentPath, ItemType = "Folder" });

            Logger.Log($"Creating query '{queryName}' in folder '{parentPath}'");

            var result = client.CreateQueryAsync(newItem, tp.Name, parentFolder.Id.ToString())
                .GetResult($"Error creating new work item {itemType} '{fullPath}'");

            yield return result;
        }
    }
}