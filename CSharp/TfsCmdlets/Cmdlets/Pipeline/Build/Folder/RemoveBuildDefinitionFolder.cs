using System.Management.Automation;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.Build.Folder
{
    /// <summary>
    /// Deletes one or more build/pipeline definition folders.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsBuildDefinitionFolder", SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiFolder))]
    [TfsCmdlet(CmdletScope.Project)]
    partial class RemoveBuildDefinitionFolder
    {
        /// <summary>
        /// Specifies the path of the pipeline/build folder to delete, including its name, 
        /// separated by backslashes (\). Wildcards are supperted.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        [Alias("Path")]
        [SupportsWildcards()]
        public object Folder { get; set; }

        /// <summary>
        /// Removes folders recursively. When omitted, folders with subfolders cannot be deleted.
        /// </summary>
        [Parameter]
        public SwitchParameter Recurse { get; set; }

        /// <summary>
        /// Forces the exclusion of folders containing build/pipelines definitions. When omitted, 
        /// only empty folders can be deleted.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }
}