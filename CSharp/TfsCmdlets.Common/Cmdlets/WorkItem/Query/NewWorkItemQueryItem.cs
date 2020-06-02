/*
.SYNOPSIS
Create a new work items query in the given Team Project.

.PARAMETER Query
Specifies the path of the new work item query.
When supplying a path, use a slash ("/") between the path segments. Leading and trailing slashes are optional.  The last segment in the path will be the query name.

.PARAMETER Project
Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. If omitted, it defaults to the connection opened by Connect-TfsTeamProject (if any). 

For more details, see the Get-TfsTeamProject cmdlet.

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
System.String
*/

using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    [Cmdlet(VerbsCommon.New, "WorkItemQueryItem", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(QueryHierarchyItem))]
    public class NewWorkItemQueryItem : BaseCmdlet
    {
        /*
                [Parameter(Position=0)]
                [ValidateNotNull()]
                [Alias("Path")]
                [Alias("Folder")]
                [Alias("Query")]
                public object Item { get; set; }

                [Parameter()]
                [ValidateSet("Personal", "Shared")]
                public string Scope { get; set; } = "Personal",

                [Parameter()]
                [ValidateSet("Folder", "Query")]
                public string ItemType { get; set; }

                [Parameter()]
                [Alias("Definition")]
                public string Wiql { get; set; }

                [Parameter(ValueFromPipeline=true)]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Force { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                if(! (PSBoundParameters.ContainsKey("ItemType"))){if (MyInvocation.InvocationName -like "*Folder"){ItemType = "Folder"}elseif (MyInvocation.InvocationName -like "*Query"){ItemType = "Query"}else{throw new Exception("Invalid or missing ItemType argument"}};PSBoundParameters["ItemType"] = ItemType)

                if (! ShouldProcess(queryName, $"Create work item {ItemType} "Item""))
                {
                    return
                }

                tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

                var client = GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();


                newItem = new Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem() -Property @{
                    Path = Item
                    IsFolder = (ItemType = = "Folder")
                }

                this.Log($"New-TfsWorkItemQuery: Creating query "{queryName}" in folder "queryPath"");

                task = client.CreateQueryAsync(newItem, tp.Name, Item); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error creating new {ItemType}" task.Exception.InnerExceptions })

                if (Passthru || SkipSave)
                {
                    WriteObject(result); return;
                }
            }
        }

        Set-Alias -Name New-TfsWorkItemQueryFolder -Value New-TfsWorkItemQueryItem
        Set-Alias -Name New-TfsWorkItemQuery -Value New-TfsWorkItemQueryItem
        */
    }
}
