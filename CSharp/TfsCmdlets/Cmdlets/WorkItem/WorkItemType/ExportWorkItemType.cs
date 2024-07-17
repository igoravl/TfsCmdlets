using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    /// <summary>
    /// Exports an XML work item type definition from a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, DefaultParameterSetName = "Export to file", SupportsShouldProcess = true, DesktopOnly = true,
     OutputType = typeof(string))]
    partial class ExportWorkItemType
    {
        /// <summary>
        /// Specifies one or more work item types to export. Wildcards are supported. 
        /// When omitted, all work item types in the given project are exported
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Name")]
        [SupportsWildcards()]
        public string Type { get; set; } = "*";

        /// <summary>
        /// Exports the definitions of referenced global lists. 
        /// When omitted, global list definitions are not included in the exported XML document.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeGlobalLists { get; set; }

        /// <summary>
        /// Specifies the path to the folder where exported types are saved.
        /// </summary>
        [Parameter(ParameterSetName = "Export to file")]
        public string Destination { get; set; }

        /// <summary>
        /// Specifies the encoding for the exported XML files. When omitted, 
        /// defaults to UTF-8.
        /// </summary>
        [Parameter(ParameterSetName = "Export to file")]
        public string Encoding { get; set; } = "UTF-8";

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

    [CmdletController]
    partial class ExportWorkItemTypeController
    {

        [Import]
        private IWorkItemStore Store { get; set; }

        protected override IEnumerable Run()
        {
            var types = Data.GetItems<WebApiWorkItemType>();
            var encoding = Parameters.Get<string>(nameof(ExportWorkItemType.Encoding));
            var asXml = Parameters.Get<bool>(nameof(ExportWorkItemType.AsXml));
            var force = Parameters.Get<bool>(nameof(ExportWorkItemType.Force));
            var includeGlobalLists = Parameters.Get<bool>(nameof(ExportWorkItemType.IncludeGlobalLists));
            var destination = Parameters.Get<string>(nameof(ExportWorkItemType.Destination));

            var tpc = Data.GetCollection();
            var tp = Data.GetProject();

            var result = new List<string>();

            foreach (var type in types)
            {
                if (!PowerShell.ShouldProcess($"Team Project '{tp.Name}'", $"Export work item type '{type.Name}'")) continue;

                var xml = Store.ExportWorkItemType(tp.Name, type.Name, includeGlobalLists);

                if (xml == null) continue;

                if (asXml)
                {
                    result.Add(xml);
                    continue;
                }

                var relativePath = $"{type.Name}.xml";
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
                    File.WriteAllText(outputPath, xml, System.Text.Encoding.GetEncoding(encoding));
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