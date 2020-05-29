/*

.SYNOPSIS
    Gets the contents of one or more Global Lists.

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
    System.Uri
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    [Cmdlet(VerbsCommon.Get, "GlobalList")]
    [OutputType(typeof(PSCustomObject))]
    public class GetGlobalList: BaseCmdlet
    {
/*
        [Parameter()]
        [Alias("Name")]
        [SupportsWildcards()]
        public string GlobalList = "*";
    
        [Parameter(ValueFromPipeline=true)]
        public object Collection { get; set; }

    protected override void BeginProcessing()
    {
        #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.Client"
    }
    
    protected override void ProcessRecord()
    {
        xml = [xml](Export-TfsGlobalList @PSBoundParameters)

        foreach(listNode in xml.SelectNodes("//GLOBALLIST"))
        {
            list = [PSCustomObject] [ordered] @{
                Name = listNode.GetAttribute("name")
                Items = @()
            }

            foreach(itemNode in listNode.SelectNodes("LISTITEM"))
            {
                list.Items += itemNode.GetAttribute("value")
            }

            list
        }
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
