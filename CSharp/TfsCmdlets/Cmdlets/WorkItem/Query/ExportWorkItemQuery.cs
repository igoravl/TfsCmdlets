using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    /// <summary>
    /// Exports a saved work item query to XML.
    /// </summary>
    /// <remarks>
    /// Work item queries can be exported to XML files (.WIQ extension) in order to be shared 
    /// and reused. Visual Studio Team Explorer has the ability to open and save WIQ files. Use 
    /// this cmdlet to generate WIQ files compatible with the format supported by Team Explorer.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Project, DefaultParameterSetName = "Export to output stream", SupportsShouldProcess = true,
     OutputType = typeof(string))]
    partial class ExportWorkItemQuery
    {
        /// <summary>
        /// Specifies one or more saved queries to export. Wildcards supported. 
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        [ValidateNotNull()]
        [SupportsWildcards()]
        [Alias("Path")]
        public object Query { get; set; }

        /// <summary>
        /// Specifies the scope of the returned item. Personal refers to the 
        /// "My Queries" folder", whereas Shared refers to the "Shared Queries" 
        /// folder. When omitted defaults to "Both", effectively searching for items 
        /// in both scopes.
        /// </summary>
        [Parameter]
        [ValidateSet("Personal", "Shared", "Both")]
        public string Scope { get; set; } = "Both";

        /// <summary>
        /// Specifies the path to the folder where exported queries are saved.
        /// </summary>
        [Parameter(ParameterSetName = "Export to file", Mandatory = true)]
        public string Destination { get; set; }

        /// <summary>
        /// Specifies the encoding for the exported XML files. When omitted, 
        /// defaults to UTF-8.
        /// </summary>
        [Parameter(ParameterSetName = "Export to file")]
        public string Encoding { get; set; } = "UTF-8";

        /// <summary>
        /// Flattens the query folder structure. When omitted, the original query 
        /// folder structure is recreated in the destination folder.
        /// </summary>
        [Parameter(ParameterSetName = "Export to file")]
        public SwitchParameter FlattenFolders { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_OVERWRITE
        /// </summary>
        /// <value></value>
        [Parameter(ParameterSetName = "Export to file")]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Exports the saved query to the standard output stream as a string-encoded 
        /// XML document.
        /// </summary>
        [Parameter(ParameterSetName = "Export to output stream", Mandatory = true)]
        public SwitchParameter AsXml { get; set; }
    }

    [CmdletController()]
    partial class ExportWorkItemQueryController
    {
        protected override IEnumerable Run()
        {
            var queries = Data.GetItems<QueryHierarchyItem>(new { ItemType = "Query" });
            var encoding = Parameters.Get<string>(nameof(ExportWorkItemQuery.Encoding));
            var asXml = Parameters.Get<bool>(nameof(ExportWorkItemQuery.AsXml));
            var flattenFolders = Parameters.Get<bool>(nameof(ExportWorkItemQuery.FlattenFolders));
            var force = Parameters.Get<bool>(nameof(ExportWorkItemQuery.Force));
            var destination = Parameters.Get<string>(nameof(ExportWorkItemQuery.Destination));

            var tpc = Data.GetCollection();
            var tp = Data.GetProject();

            var result = new List<string>();

            foreach (var query in queries)
            {
                if (!PowerShell.ShouldProcess($"Team Project '{tp.Name}'", $"Export work item query '{query.Path}'")) continue;

                var doc = XDocument.Parse($@"<?xml version=""1.0"" encoding=""{encoding}""?>
<WorkItemQuery Version=""1"">
    <!-- Original Query Path: [{query.Path}] -->
    <TeamFoundationServer>{tpc.Uri}</TeamFoundationServer>
    <TeamProject>{tp.Name}</TeamProject>
    <Wiql><![CDATA[{query.Wiql}]]></Wiql>
</WorkItemQuery>

");

                if (asXml)
                {
                    result.Add(doc.ToString());
                    continue;
                }

                var relativePath = $"{(flattenFolders ? query.Path.Replace('/', '_') : query.Path)}.wiq";
                var outputPath = PowerShell.ResolvePath(destination, relativePath);
                var destDir = Path.GetDirectoryName(outputPath);

                if (!Directory.Exists(destDir))
                {
                    Logger.Log($"Destination path '{destination}' not found.");

                    if (!PowerShell.ShouldProcess(destination, "Create output directory")) continue;

                    Directory.CreateDirectory(destDir);
                }

                if (File.Exists(outputPath) && !(force || PowerShell.ShouldContinue($"Are you sure you want to overwrite existing file '{outputPath}'", "Confirm")))
                {
                    Logger.LogWarn($"Cannot overwrite existing file '{outputPath}'");
                    continue;
                }

                try
                {
                    doc.Save(outputPath);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }

            return result;
        }
    }
}