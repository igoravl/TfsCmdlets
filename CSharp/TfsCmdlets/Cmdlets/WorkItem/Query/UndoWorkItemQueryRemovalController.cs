using System.Xml.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Cmdlets.WorkItem.Query;

namespace TfsCmdlets.Controllers.WorkItem.Query
{
    [CmdletController(typeof(QueryHierarchyItem), Client=typeof(IWorkItemTrackingHttpClient))]
    partial class UndoWorkItemQueryRemovalController
    {
        protected override IEnumerable Run()
        {
            foreach(var item in Items)
            {
                if(!PowerShell.ShouldProcess(Project, $"Restore {ItemType} '{item.Path}'")) continue;

                yield return Client.UpdateQueryAsync(new QueryHierarchyItem(){IsDeleted=false}, Project.Id, item.Id.ToString(), undeleteDescendants: Recursive)
                    .GetResult($"Error restoring {ItemType} '{item.Path}'");
            }
        }
    }
}