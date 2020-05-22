/*
.SYNOPSIS
Exports the contents of one or more Global Lists to XML.

.DESCRIPTION
This cmdlets generates an XML containing one or more global lists and their respective items, in the same format used by witadmin. It is functionally equivalent to "witadmin exportgloballist"

.PARAMETER Name
Specifies the name of the global list to be exported. Wildcards are supported; when used, they result in a single XML containing all the matching global lists.

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

.EXAMPLE
Export-TfsGlobalList | Out-File "gl.xml"
Exports all global lists in the current project collection to a file called gl.xml.

.EXAMPLE
Export-TfsGlobalList -Name "Builds - *"
Exports all build-related global lists (with names starting with "Build - ") and WriteObject(the resulting XML document); return;

.NOTES
To export or list global lists, you must be a member of the Project Collection Valid Users group or have your View collection-level information permission set to Allow.
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    [Cmdlet(VerbsData.Export, "GlobalList")]
    [OutputType(typeof(string))]
    public class ExportGlobalList: BaseCmdlet
    {
/*
        [Parameter(Position=0)]
        [Alias("Name")]
        [SupportsWildcards()]
        [string] 
        GlobalList = "*",

        [Parameter(ValueFromPipeline=true)]
        public object Collection { get; set; }

    protected override void BeginProcessing()
    {
        #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.Client"
    }
    
    protected override void ProcessRecord()
    {
        tpc = Get-TfsTeamProjectCollection Collection
        store = tpc.GetService([type]"Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore")

        xml = [xml] store.ExportGlobalLists()

        procInstr = xml.CreateProcessingInstruction("xml", "version="1.0"")

        [void] xml.InsertBefore(procInstr, xml.DocumentElement)

        nodesToRemove = xml.SelectNodes("//GLOBALLIST")

        foreach(node in nodesToRemove)
        {
            if (([System.Xml.XmlElement]node).GetAttribute("name") -notlike GlobalList)
            {
                [void]xml.DocumentElement.RemoveChild(node)
            }
        }

        WriteObject(xml.OuterXml); return;
    }
}
*/
}
}
