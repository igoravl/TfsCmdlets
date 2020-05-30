/*

.SYNOPSIS
    Gets the history of changes of a work item.

.PARAMETER Collection
    Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.EXAMPLE
    Get-TfsWorkItem -Filter "[System.WorkItemType] = $"Task"" | Foreach-Object { Write-Output "WI {{_}.Id}: $(_.Title)"; Get-TfsWorkItemHistory -WorkItem _ } 

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem
    System.Int32
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.History
{
    [Cmdlet(VerbsCommon.Get, "WorkItemHistory")]
    [OutputType(typeof(PSCustomObject))]
    public class GetWorkItemHistory: BaseCmdlet
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
        wi = Get-TfsWorkItem -WorkItem WorkItem -Collection Collection
        latestRev = wi.Revisions.Count - 1

        0..latestRev | Foreach-Object {
            rev = wi.Revisions[_]

            [PSCustomObject] @{
                Revision = _ + 1;
                ChangedDate = rev.Fields["System.ChangedDate"].Value
                ChangedBy = rev.Fields["System.ChangedBy"].Value
                Changes = _GetChangedFields wi _
            }
        }
    }
}

Function _GetChangedFields([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem] wi, [int] rev)
{
    result = @{}

    wi.Revisions[rev].Fields | Where-Object IsChangedInRevision == true | Foreach-Object {
        result[_.ReferenceName] =  [PSCustomObject] @{
            NewValue = _.Value;
            OriginalValue = _.OriginalValue
        }
    }

    WriteObject(result); return;
}
*/
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();
    }
}
