using System.Management.Automation;
using System.Xml;

namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    [Cmdlet(VerbsData.Export, "TfsWorkItemQuery", DefaultParameterSetName = "Export to output stream", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(XmlDocument))]
    public class ExportWorkItemQuery : BaseCmdlet
    {

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(ValueFromPipeline=true, Position=0)]
        //         [SupportsWildcards()]
        //         [object] 
        //         Query = "**/*",

        //         [Parameter()]
        //         [ValidateSet("Personal", "Shared", "Both")]
        //         public string Scope { get; set; } = "Both",

        //         [Parameter(ParameterSetName="Export to file", Mandatory=true)]
        //         public string Destination { get; set; }

        //         [Parameter(ParameterSetName="Export to file")]
        //         public string Encoding { get; set; } = "UTF-8",

        //         [Parameter(ParameterSetName="Export to file")]
        //         public SwitchParameter FlattenFolders { get; set; }

        //         [Parameter(ParameterSetName="Export to file")]
        //         public SwitchParameter Force { get; set; }

        //         [Parameter(ParameterSetName="Export to output stream")]
        //         public SwitchParameter AsXml { get; set; }

        //         [Parameter()]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void ProcessRecord()
        //     {
        //         if(ParameterSetName == "Export to output stream")
        //         {
        //             Encoding = "UTF-16"
        //         }
        //         else
        //         {
        //             if (! (Test-Path Destination -PathType Container))
        //             {
        //                 this.Log($"Destination path "{Destination}" not found.");

        //                 if (Force)
        //                 {
        //                     this.Log("-Force switch specified. Creating output directory.");

        //                     if(ShouldProcess(Destination, "Create output directory"))
        //                     {
        //                         New-Item Destination -ItemType Directory | this.Log
        //                     }
        //                 }
        //                 else
        //                 {
        //                     throw new Exception($"Invalid output path {Destination}")
        //                 }
        //             }
        //         }

        //         queries = Get-TfsWorkItemQueryItem -ItemType Query -Query Query -Scope Scope -Project Project -Collection Collection

        //         if (! queries)
        //         {
        //             throw new Exception($"No work item queries match the specified `"{Query}`" pattern supplied.")
        //         }

        //         foreach(q in queries)
        //         {
        //             if(q.TeamProject) {Project = q.TeamProject}; tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        //             xml = [xml]@"
        // <?xml version=$"1.0" encoding="{Encoding}"?>
        // <!-- Original Query Path: $(q.Path) -->
        // <WorkItemQuery Version="1">
        //   <TeamFoundationServer>$(tp.Store.TeamProjectCollection.Uri)</TeamFoundationServer>
        //   <TeamProject>$(tp.Name)</TeamProject>
        //   <Wiql><![CDATA[$(q.Wiql)]]></Wiql>
        // </WorkItemQuery>
        // "@
        //             if (! Destination)
        //             {
        //                 if (AsXml)
        //                 {
        //                     Write-Output xml
        //                 }
        //                 else 
        //                 {
        //                     Write-Output xml.OuterXml
        //                 }
        //                 continue
        //             }

        //             if (FlattenFolders)
        //             {
        //                 queryPath = q.Name
        //             }
        //             else
        //             {
        //                 queryPath = q.Path.Substring(q.Path.IndexOf("/")+1)
        //             }

        //             fileName = _GetAbsolutePath (Join-Path Destination $"{queryPath}.wiql")

        //             this.Log($"Exporting query {{q}.Name} to path "fileName"");

        //             if((Test-Path fileName) && (! Force))
        //             {
        //                 throw new Exception($"File {fileName} already exists. To overwrite an existing file, use the -Force switch")
        //             }

        //             if(ShouldProcess(fileName, $"Save query "{{q}.Name}""))
        //             {
        //                 xml.Save(fileName)
        //             }
        //         }
        //     }
        // }
    }
}
