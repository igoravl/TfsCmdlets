/*

.SYNOPSIS
    Deletes one or more Global Lists.

.PARAMETER Collection
    Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
    System.String
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    [Cmdlet(VerbsCommon.Remove, "GlobalList", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public class RemoveGlobalList : BaseCmdlet
    {
        /*
                [Parameter(ValueFromPipelineByPropertyName="Name")]
                [Alias("Name")]
                [SupportsWildcards()]
                [string] 
                GlobalList = "*",

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

                lists = Get-TfsGlobalList -Name GlobalList -Collection Collection
                listsToRemove = @()

                foreach(list in lists)
                {
                    if (ShouldProcess(list.Name, "Remove global list"))
                    {
                        listsToRemove += list
                    }
                }

                if (listsToRemove.Length == 0)
                {
                    return
                }

                xml = [xml] "<Package />"

                foreach(list in listsToRemove)
                {
                    elem = xml.CreateElement("DestroyGlobalList");
                    elem.SetAttribute("ListName", "*" + list.Name);
                    elem.SetAttribute("ForceDelete", "true");
                    [void]xml.DocumentElement.AppendChild(elem);
                }

                returnElem = null

                store.SendUpdatePackage(xml.DocumentElement, [ref] returnElem, false)
            }
        }
        */
    }
}
