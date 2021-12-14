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
        /// Allows the cmdlet to overwrite an existing file in the destination folder.
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
}