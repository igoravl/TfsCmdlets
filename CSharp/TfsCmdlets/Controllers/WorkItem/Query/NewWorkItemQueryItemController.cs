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
            var scope = Parameters.Get<string>(nameof(GetWorkItemQuery.Scope));
            var wiql = Parameters.Get<string>("Wiql");
            var force = Parameters.Get<bool>("Force");

            var tp = Data.GetProject();

            var fullPath = NodeUtil.NormalizeNodePath(item, tp.Name, scope, includeScope: true, separator: '/');
            var queryName = Path.GetFileName(fullPath);
            var parentPath = Path.GetDirectoryName(fullPath);

            var existingItem = Data.TestItem<QueryHierarchyItem>();

            if (existingItem && isFolder)
            {
                Logger.Log("Folder already exists.");

                if (!force) Logger.LogError(new Exception($"A folder with the specified name '{fullPath}' already exists."));

                return null;
            }

            if (!PowerShell.ShouldProcess(tp, $"{(existingItem ? "Create" : "Overwrite")} " +
                $"work item query{(isFolder ? " folder" : "")} '{fullPath}'")) return null;

            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            var newItem = new QueryHierarchyItem()
            {
                Name = queryName,
                Path = parentPath,
                IsFolder = isFolder,
                Wiql = wiql
            };

            var parentFolder = Data.TestItem<QueryHierarchyItem>(new { Folder = parentPath, ItemType = "Folder" }) ?
                Data.GetItem<QueryHierarchyItem>(new { Folder = parentPath, ItemType = "Folder" }) :
                Data.NewItem<QueryHierarchyItem>(new { Folder = parentPath, ItemType = "Folder" });

            Logger.Log($"Creating query '{queryName}' in folder '{parentPath}'");

            var result = client.CreateQueryAsync(newItem, tp.Name, parentFolder.Id.ToString())
                .GetResult($"Error creating new work item {itemType} '{fullPath}'");

            return new[]{result};
        }
    }
}