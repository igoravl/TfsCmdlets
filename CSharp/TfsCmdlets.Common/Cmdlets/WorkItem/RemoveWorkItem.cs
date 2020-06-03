/*

.SYNOPSIS
    Deletes a work item from a team project collection.

.PARAMETER Collection
    Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem
    System.Int32
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    [Cmdlet(VerbsCommon.Remove, "TfsWorkItem", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public class RemoveWorkItem : BaseCmdlet
    {
        /*
                [Parameter(Position=0, Mandatory=true, ValueFromPipeline=true)]
                [Alias("id")]
                [ValidateNotNull()]
                public object WorkItem { get; set; }

                [Parameter()]
                public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                ids = @()

                foreach(wi in WorkItem)
                {
                    if (WorkItem is Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem)
                    {
                        id = WorkItem.Id
                    }
                    elseif (WorkItem is int)
                    {
                        id = WorkItem
                    }
                    else
                    {
                        throw new Exception($"Invalid work item ""{WorkItem}"". Supply either a WorkItem object or one or more integer ID numbers")
                    }

                    if (ShouldProcess($"{{wi}.WorkItemType} id ("$(wi.Title)")", "Remove work item"))
                    {
                        ids += id
                    }
                }

                if (ids.Count -gt 0)
                {
                    tpc = Get-TfsTeamProjectCollection Collection
                    store = tpc.GetService([type] "Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore")

                    errors = store.DestroyWorkItems([int[]] ids)

                    if (errors && (errors.Count -gt 0))
                    {
                        errors | Write-Error $"Error {{_}.Id}: $(_.Exception.Message)"

                        throw new Exception("Error destroying one or more work items")
                    }
                }
            }
        }
        */
    }
}
