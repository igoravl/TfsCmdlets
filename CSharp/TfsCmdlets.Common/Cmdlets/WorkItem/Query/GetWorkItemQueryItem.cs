using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    /// <summary>
    /// Gets the definition of one or more work item saved queries.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemQueryItem")]
    [OutputType(typeof(QueryHierarchyItem))]
    public class GetWorkItemQueryItem : CmdletBase
    {

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();
        
        //         [Parameter(Position=0)]
        //         [ValidateNotNull()]
        //         [SupportsWildcards()]
        //         [Alias("Path")]
        //         [Alias("Folder")]
        //         [Alias("Query")]
        //         public object Item { get; set; } = "*/**",

        //         [Parameter()]
        //         [ValidateSet("Personal", "Shared", "Both")]
        //         public string Scope { get; set; } = "Both",

        //         [Parameter()]
        //         [ValidateSet("Folder", "Query", "Both")]
        //         public string ItemType { get; set; }

        //         [Parameter()]
        //         public SwitchParameter IncludeDeleted { get; set; }

        //         [Parameter(ValueFromPipeline=true)]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void ProcessRecord()
        //     {
        //         if (Item is Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem) { this.Log("Input item is of type Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem; returning input item immediately, without further processing."; WriteObject(Item }); return;);

        //         if(! (PSBoundParameters.ContainsKey("ItemType"))){if (MyInvocation.InvocationName -like "*Folder"){ItemType = "Folder"}elseif (MyInvocation.InvocationName -like "*Query"){ItemType = "Query"}else{throw new Exception("Invalid or missing ItemType argument"}};PSBoundParameters["ItemType"] = ItemType)

        //         tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        //         var client = GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();

        //         task = client.GetQueriesAsync(tp.Name, "All", 2); result = task.Result; if(task.IsFaulted) { _throw new Exception( "Error fetching work item query items" task.Exception.InnerExceptions })

        //         this.Log($"Getting {{ItemType}.ToLower(}) items matching "Item"");

        //         foreach(i in result)
        //         {
        //             _GetQueryItemRecursively -Pattern Item -Item i -ItemType ItemType -Scope Scope -IncludeDeleted IncludeDeleted.IsPresent -Project tp.Name -Client Client
        //         }
        //     }
        // }

        // Set-Alias -Name Get-TfsWorkItemQueryFolder -Value Get-TfsWorkItemQueryItem
        // Set-Alias -Name Get-TfsWorkItemQuery -Value Get-TfsWorkItemQueryItem

        // Function _GetQueryItemRecursively(Pattern, Item, ItemType, Scope, Project, Client, Depth = 2, IncludeDeleted)
        // {
        //     this.Log($"Found {{Item}.ItemType} "$(Item.Path)" (IsPublic=$(Item.IsPublic),HasChildren=$(Item.HasChildren))");

        //     if(Item.HasChildren && (Item.Children.Count == 0))
        //     {
        //         this.Log($"Fetching child nodes for node "{{Item}.Path}"");

        //         task = client.GetQueryAsync(Project, Item.Path, "All", Depth, IncludeDeleted); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error retrieving {StructureGroup} from path "{Item.RelativePath}"" task.Exception.InnerExceptions })

        //         Item = result
        //     }

        //     if((ItemType != "Both") && (Item.ItemType != ItemType))
        //     {
        //         this.Log($"Skipping item. "{{Item}.Path}" is "$(Item.ItemType)" but item type being queried for is "ItemType".");
        //     }
        //     elseif ((Scope = = "Both") || `
        //             ((Scope = = "Shared") && Item.IsPublic) || `
        //             ((Scope = = "Personal") && (! Item.IsPublic)))
        //     {
        //         if(Item.Path -like Pattern)
        //         {
        //             this.Log($""{{Item}.Path}" matches pattern "Pattern". Returning node.");

        //             Item | Add-Member -MemberType NoteProperty -Name Project -Value Project
        //             Write-Output Item
        //         }
        //         else
        //         {
        //             this.Log($"Skipping item. "{{Item}.Path}" does not match pattern "Pattern".");
        //         }
        //     }
        //     else
        //     {
        //         this.Log($"Skipping item. "{{Item}.Path}" does not match scope "Scope".");
        //     }

        //     foreach(c in Item.Children)
        //     {
        //         PSBoundParameters["Item"] = c
        //         _GetQueryItemRecursively @PSBoundParameters
        //     }
        // }
    }
}
