using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    /// <summary>
    /// Create a new work items query in the given Team Project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsWorkItemQueryItem", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(QueryHierarchyItem))]
    public class NewWorkItemQueryItem : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Position=0)]
        //         [ValidateNotNull()]
        //         [Alias("Path")]
        //         [Alias("Folder")]
        //         [Alias("Query")]
        //         public object Item { get; set; }

        //         [Parameter()]
        //         [ValidateSet("Personal", "Shared")]
        //         public string Scope { get; set; } = "Personal",

        //         [Parameter()]
        //         [ValidateSet("Folder", "Query")]
        //         public string ItemType { get; set; }

        //         [Parameter()]
        //         [Alias("Definition")]
        //         public string Wiql { get; set; }

        //         [Parameter(ValueFromPipeline=true)]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //         [Parameter()]
        //         public SwitchParameter Force { get; set; }

        //         [Parameter()]
        //         public SwitchParameter Passthru { get; set; }

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void DoProcessRecord()
        //     {
        //         if(! (PSBoundParameters.ContainsKey("ItemType"))){if (MyInvocation.InvocationName -like "*Folder"){ItemType = "Folder"}elseif (MyInvocation.InvocationName -like "*Query"){ItemType = "Query"}else{throw new Exception("Invalid or missing ItemType argument"}};PSBoundParameters["ItemType"] = ItemType)

        //         if (! ShouldProcess(queryName, $"Create work item {ItemType} "Item""))
        //         {
        //             return
        //         }

        //         tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        //         var client = GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();


        //         newItem = new Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem() -Property @{
        //             Path = Item
        //             IsFolder = (ItemType = = "Folder")
        //         }

        //         this.Log($"New-TfsWorkItemQuery: Creating query "{queryName}" in folder "queryPath"");

        //         task = client.CreateQueryAsync(newItem, tp.Name, Item); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error creating new {ItemType}" task.Exception.InnerExceptions })

        //         if (Passthru || SkipSave)
        //         {
        //             WriteObject(result); return;
        //         }
        //     }
        // }

        // Set-Alias -Name New-TfsWorkItemQueryFolder -Value New-TfsWorkItemQueryItem
        // Set-Alias -Name New-TfsWorkItemQuery -Value New-TfsWorkItemQueryItem
    }
}
