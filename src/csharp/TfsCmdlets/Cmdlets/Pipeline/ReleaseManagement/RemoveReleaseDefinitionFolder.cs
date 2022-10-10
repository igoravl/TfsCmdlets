using System.Management.Automation;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    /// <summary>
    /// Deletes one or more release definition folders.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiFolder))]
    partial class RemoveReleaseDefinitionFolder
    {
        /// <summary>
        /// Specifies the path of the release folder to delete. Wildcards are supported.
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
        /// Forces the exclusion of folders containing release definitions definitions. When omitted, 
        /// only empty folders can be deleted.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }
}