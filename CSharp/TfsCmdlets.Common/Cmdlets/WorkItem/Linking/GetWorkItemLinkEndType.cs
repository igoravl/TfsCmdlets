/*

.SYNOPSIS
    Gets the work item link end types of a team project collection.

.PARAMETER Collection
    Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
    Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
    System.String
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemLinkEndType")]
    //[OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemLinkTypeEnd))]
    public class GetWorkItemLinkEndType: BaseCmdlet
    {
/*
        [Parameter(Position=0, ValueFromPipeline=true)]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
    {
        tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})

        WriteObject(tpc.WorkItemStore.WorkItemLinkTypes.LinkTypeEnds); return;
    }
}
*/
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();
    }
}
