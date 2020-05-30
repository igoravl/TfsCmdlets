/*
.SYNOPSIS
Changes the name or the contents of a Global List.

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
    [Cmdlet(VerbsCommon.Set, "GlobalList", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    public class SetGlobalList : BaseCmdlet
    {
        /*
                [Parameter(Mandatory=true, ValueFromPipelineByPropertyName="Name")]
                [Alias("Name")]
        public string GlobalList,

                [Parameter(ParameterSetName="Edit list items")]
                [string[]] 
                Add,

                [Parameter(ParameterSetName="Edit list items")]
                [string[]] 
                Remove,

                [Parameter(ParameterSetName="Rename list", Mandatory=true)]
        public string NewName,

                [Parameter(ParameterSetName="Edit list items")]
                [SwitchParameter] 
                Force,

                public object Collection { get; set; }

            protected override void BeginProcessing()
            {
                #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.Client"
            }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                xml = [xml] (Export-TfsGlobalList -Name GlobalList -Collection Collection)

                # Retrieves the list
                list = xml.SelectSingleNode("//GLOBALLIST")
                newList = false

                if (null = = list)
                {
                    if (! Force.IsPresent)
                    { 
                        throw new Exception($"Global list name {GlobalList} is invalid or non-existent. Either check the name or use -Force to create a new list.")
                    }

                    # Creates the new list XML element
                    list = xml.CreateElement("GLOBALLIST")
                    [void] list.SetAttribute("name", GlobalList)
                    [void] xml.DocumentElement.AppendChild(list)
                    newList = true
                }

                if (ParameterSetName == "Rename list")
                {
                    if(ShouldProcess(GlobalList, $"Rename global list to {NewName}"))
                    {
                        list.SetAttribute("name", NewName)
                        Import-TfsGlobalList -Xml xml -Collection Collection
                        Remove-TfsGlobalList -Name GlobalList -Collection Collection -Confirm:false
                    }
                    WriteObject(Get-TfsGlobalList -Name NewName -Collection Collection); return;
                }

                foreach(item in Add)
                {
                    if (! newList)
                    {
                        # Checks if the element exists (prevents duplicates)
                        existingItem = list.SelectSingleNode($"LISTITEM[@value="{item}"]")
                        if (null != existingItem) { continue }
                    }

                    if(ShouldProcess(GlobalList, $"Add item "{item}" to global list"))
                    {
                        isDirty = true
                        itemElement = xml.CreateElement("LISTITEM")
                        [void] itemElement.SetAttribute("value", item)
                        [void]list.AppendChild(itemElement)
                    }
                }

                if (! newList)
                {
                    foreach(item in Remove)
                    {
                        existingItem = list.SelectSingleNode($"LISTITEM[@value="{item}"]")

                        if (existingItem && ShouldProcess(GlobalList, $"Remove item "{item}" from global list"))
                        {
                            isDirty = true
                            [void]list.RemoveChild(existingItem)
                        }
                    }
                }

                # Saves the list back to TFS
                if(isDirty)
                {
                    Import-TfsGlobalList -Xml xml -Collection Collection -Force
                }

                WriteObject(Get-TfsGlobalList -Name GlobalList -Collection Collection); return;
            }
        }
        */
    }
}
