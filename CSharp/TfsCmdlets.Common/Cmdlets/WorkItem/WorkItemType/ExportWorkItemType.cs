/*

.SYNOPSIS
    Exports a work item type definition from a team project to XML.

.PARAMETER Name
    Uses this parameter to filter for an specific Work Item Type.
    If suppress, cmdlet will WriteObject(all Work Item Types on XML format.); return;

.PARAMETER IncludeGlobalLists
     Exports the definitions of referenced global lists. If not specified, global list definitions are omitted.

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
using System.Xml;

namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    [Cmdlet(VerbsData.Export, "WorkItemType")]
    [OutputType(typeof(XmlDocument))]
    public class ExportWorkItemType: BaseCmdlet
    {
/*
        [Parameter()]
        [Alias("Name")]
        [SupportsWildcards()]
        public string WorkItemType = "*";

        [Parameter()]
        public SwitchParameter IncludeGlobalLists { get; set; }

        [Parameter(ValueFromPipeline=true)]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
    {
        types = Get-TfsWorkItemType -Name WorkItemType -Project Project -Collection Collection

        foreach(type in types)
        {
            type.Export(IncludeGlobalLists)
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
