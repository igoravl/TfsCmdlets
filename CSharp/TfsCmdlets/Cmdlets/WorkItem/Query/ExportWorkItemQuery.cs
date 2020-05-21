/*
.SYNOPSIS
Exports a saved work item query to XML.

.DESCRIPTION
Work item queries can be exported to XML files (.WIQ extension) in order to be shared and reused. Visual Studio Team Explorer has the ability to open and save WIQ files. Use this cmdlet to generate WIQ files compatible with the format supported by Team Explorer.

.PARAMETER Query
Name of the work item query to be exported. Wildcards are supported.

.PARAMETER Folder
Full path of the folder containing the query(ies) to be exported. Wildcards are supported.

.PARAMETER Destination
Path to the target directory where the exported work item query (WIQL file) will be saved. The original folder structure (as defined in TFS/VSTS) will be preserved.

.PARAMETER Encoding
XML encoding of the generated WIQL files. If omitted, defaults to UTF-8.

.PARAMETER Collection
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
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri

.EXAMPLE
Export-TfsWorkItemQuery 
.LINK
https://www.visualstudio.com/en-us/docs/work/reference/process-templates/define-work-item-query-process-template

.NOTES
For queries made against Team Services, the WIQL length must not exceed 32K characters. The system won"t allow you to create or run queries that exceed that length.
*/

using System;
using System.Management.Automation;
using System.Xml;

namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    [Cmdlet(VerbsData.Export, "WorkItemQuery", DefaultParameterSetName = "Export to output stream", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(XmlDocument))]
    public class ExportWorkItemQuery : PSCmdlet
    {
        /*
                [Parameter(ValueFromPipeline=true, Position=0)]
                [SupportsWildcards()]
                [object] 
        // TODO: Remover espaço antes do /
                Query = "** /*",

                [Parameter()]
                [ValidateSet("Personal", "Shared", "Both")]
                public string Scope { get; set; } = "Both",

                [Parameter(ParameterSetName="Export to file", Mandatory=true)]
                public string Destination { get; set; }

                [Parameter(ParameterSetName="Export to file")]
                public string Encoding { get; set; } = "UTF-8",

                [Parameter(ParameterSetName="Export to file")]
                public SwitchParameter FlattenFolders { get; set; }

                [Parameter(ParameterSetName="Export to file")]
                public SwitchParameter Force { get; set; }

                [Parameter(ParameterSetName="Export to output stream")]
                public SwitchParameter AsXml { get; set; }

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

            protected override void ProcessRecord()
            {
                if(ParameterSetName == "Export to output stream")
                {
                    Encoding = "UTF-16"
                }
                else
                {
                    if (! (Test-Path Destination -PathType Container))
                    {
                        _Log $"Destination path "{Destination}" not found."

                        if (Force)
                        {
                            _Log "-Force switch specified. Creating output directory."

                            if(ShouldProcess(Destination, "Create output directory"))
                            {
                                New-Item Destination -ItemType Directory | _Log
                            }
                        }
                        else
                        {
                            throw new Exception($"Invalid output path {Destination}")
                        }
                    }
                }

                queries = Get-TfsWorkItemQueryItem -ItemType Query -Query Query -Scope Scope -Project Project -Collection Collection

                if (! queries)
                {
                    throw new Exception($"No work item queries match the specified `"{Query}`" pattern supplied.")
                }

                foreach(q in queries)
                {
                    if(q.TeamProject) {Project = q.TeamProject}; tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

                    xml = [xml]@"
        <?xml version=$"1.0" encoding="{Encoding}"?>
        <!-- Original Query Path: $(q.Path) -->
        <WorkItemQuery Version="1">
          <TeamFoundationServer>$(tp.Store.TeamProjectCollection.Uri)</TeamFoundationServer>
          <TeamProject>$(tp.Name)</TeamProject>
          <Wiql><![CDATA[$(q.Wiql)]]></Wiql>
        </WorkItemQuery>
        "@
                    if (! Destination)
                    {
                        if (AsXml)
                        {
                            Write-Output xml
                        }
                        else 
                        {
                            Write-Output xml.OuterXml
                        }
                        continue
                    }

                    if (FlattenFolders)
                    {
                        queryPath = q.Name
                    }
                    else
                    {
                        queryPath = q.Path.Substring(q.Path.IndexOf("/")+1)
                    }

                    fileName = _GetAbsolutePath (Join-Path Destination $"{queryPath}.wiql")

                    _Log $"Exporting query {{q}.Name} to path "fileName""

                    if((Test-Path fileName) && (! Force))
                    {
                        throw new Exception($"File {fileName} already exists. To overwrite an existing file, use the -Force switch")
                    }

                    if(ShouldProcess(fileName, $"Save query "{{q}.Name}""))
                    {
                        xml.Save(fileName)
                    }
                }
            }
        }
        */
    }
}
