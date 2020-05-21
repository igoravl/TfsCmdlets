/*

.SYNOPSIS
    Creates a new Global List.

.PARAMETER Collection
    Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
    System.String / System.String[]
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    [Cmdlet(VerbsCommon.New, "GlobalList", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(PSCustomObject))]
    public class NewGlobalList : PSCmdlet
    {
        /*
                [Parameter(Mandatory=true, ValueFromPipelineByPropertyName="Name")]
                [Alias("Name")]
                [string] 
                GlobalList,

                [Parameter(Mandatory=true, ValueFromPipelineByPropertyName="Items")] 
                [string[]] 
                Items,

                [Parameter()]
                public SwitchParameter Force { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

            protected override void BeginProcessing()
            {
                #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.Client"
            }

            protected override void ProcessRecord()
            {
                [xml] xml = Export-TfsGlobalList -Collection Collection

                # Checks whether the global list already exists
                list = xml.SelectSingleNode($"//GLOBALLIST[@name="{GlobalList}"]")

                if (null != list)
                {
                    if (Force.IsPresent)
                    {
                        if (ShouldProcess(GlobalList, "Overwrite existing global list"))
                        {
                            [void] list.ParentNode.RemoveChild(list)
                        }
                    }
                    else
                    {
                        throw new Exception($"Global List {GlobalList} already exists. To overwrite an existing list, use the -Force switch.")
                    }
                }

                if(ShouldProcess(GlobalList, "Create global list"))
                {
                    # Creates the new list XML element
                    list = xml.CreateElement("GLOBALLIST")
                    list.SetAttribute("name", GlobalList)

                    # Adds the item elements to the list
                    foreach(item in Items)
                    {
                        itemElement = xml.CreateElement("LISTITEM")
                        [void] itemElement.SetAttribute("value", item)
                        [void]list.AppendChild(itemElement)
                    }

                    # Appends the new list to the XML obj
                    [void] xml.DocumentElement.AppendChild(list)

                    Import-TfsGlobalList -Xml xml -Collection Collection
                    list =  Get-TfsGlobalList -Name GlobalList -Collection Collection

                    if (Passthru)
                    {
                        WriteObject(list); return;
                    }
                }        
            }
        }
        */
    }
}
