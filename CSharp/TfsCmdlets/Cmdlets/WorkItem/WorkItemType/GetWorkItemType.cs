/*

.SYNOPSIS
    Gets one or more Work Item Type definitions from a team project.

.PARAMETER Name
    Uses this parameter to filter for an specific Work Item Type.
    If suppress, cmdlet will show all Work Item Types.

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

.EXAMPLE
    Get-TfsWorkItemType -Name "Task" -Project "My Team Project"
    Get informations about Work Item Type "Task" of a team project name "My Team Project"

.EXAMPLE
    Get-TfsWorkItemType -Project "My Team Project"
    Get all Work Item Types of a team project name "My Team Project"

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    [Cmdlet(VerbsCommon.Get, "WorkItemType")]
    //[OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType))]
    public class GetWorkItemType: PSCmdlet
    {
/*
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias("Name")]
        [object] 
        Type = "*",

        [Parameter(ValueFromPipeline=true)]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

    protected override void ProcessRecord()
    {
        if (Type is Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType)
        {
            WriteObject(Type); return;
        }

        tp = Get-TfsTeamProject -Project Project -Collection Collection

        WriteObject(tp.WorkItemTypes | Where-Object Name -Like Type); return;
    }
}
*/
}
}
