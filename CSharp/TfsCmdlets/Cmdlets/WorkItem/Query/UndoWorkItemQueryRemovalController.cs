using System.Xml.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Cmdlets.WorkItem.Query;

namespace TfsCmdlets.Controllers.WorkItem.Query
{
    [CmdletController(typeof(QueryHierarchyItem))]
    partial class UndoWorkItemQueryRemovalController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<WorkItemTrackingHttpClient>();

            foreach(var item in Items)
            {
                if(!PowerShell.ShouldProcess(Project, $"Restore {ItemType} '{item.Path}'")) continue;

                yield return client.UpdateQueryAsync(new QueryHierarchyItem(){IsDeleted=false}, Project.Id, item.Id.ToString(), undeleteDescendants: Recursive)
                    .GetResult($"Error restoring {ItemType} '{item.Path}'");
            }
        }
    }
}