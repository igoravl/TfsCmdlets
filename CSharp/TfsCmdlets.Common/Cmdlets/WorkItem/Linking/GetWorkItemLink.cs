/*

.SYNOPSIS
    Gets the links of a work item.

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
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemLink")]
    //[OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.Client.Link))]
    public class GetWorkItemLink: BaseCmdlet
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

        if (wi)
        {
            WriteObject(wi.Links); return;
        }
    }
}
*/
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();
    }
}
