/*
.SYNOPSIS
Imports one or more Global Lists from an XML document

.DESCRIPTION
This cmdletsimports an XML containing one or more global lists and their respective items, in the same format used by witadmin. It is functionally equivalent to "witadmin importgloballist"

.PARAMETER InputObject
XML document object containing one or more global list definitions

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
System.Xml.XmlDocument

.EXAMPLE
Get-Content gl.xml | Import-GlobalList
Imports the contents of an XML document called gl.xml to the current project collection

.NOTES
To import global lists, you must be a member of the Project Collection Administrators security group
*/

using System;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    [Cmdlet(VerbsData.Import, "GlobalList", ConfirmImpact = ConfirmImpact.Medium)]
    public class ImportGlobalList : PSCmdlet
    {
        /*
                [Parameter(Mandatory=true, ValueFromPipeline=true)]
                [Alias("Xml")]
                [object] 
                InputObject,

                [Parameter()]
                public SwitchParameter Force { get; set; }

                [Parameter()]
                public object Collection { get; set; }

            protected override void BeginProcessing()
            {
                #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.Client"
            }

            protected override void ProcessRecord()
            {
                tpc = Get-TfsTeamProjectCollection Collection
                store = tpc.GetService([type]"Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore")

                if (InputObject is xml)
                {
                    doc = InputObject.OuterXml
                }
                else
                {
                    doc = InputObject
                }

                if (! Force)
                {
                    existingLists = Get-TfsGlobalList -Collection tpc

                    //TODO: Remover espaço após *

                    listsInXml = ([xml](InputObject)).SelectNodes("//* /@name")."#text"

                    foreach(list in existingLists)
                    {
                        if (list.Name -in listsInXml)
                        {
                            throw new Exception($"Global List "{{list}.Name}" already exists. To overwrite an existing list, use the -Force switch.")
                        }
                    }
                }

                [void] store.ImportGlobalLists(doc)
            }
        }
        */
    }
}
