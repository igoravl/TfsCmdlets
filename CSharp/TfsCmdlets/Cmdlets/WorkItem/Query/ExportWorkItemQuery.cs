using System.Management.Automation;

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