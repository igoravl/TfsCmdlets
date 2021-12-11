using System.Management.Automation;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    /// <summary>
    /// Creates a new release definition folder.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiFolder))]
    partial class NewReleaseDefinitionFolder 
    {
        /// <summary>
        /// Specifies the folder path. Wildcards are supported. 
        /// When omitted, all Release/pipeline folders in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Path")]
        public string Folder { get; set; } = "**";

        /// <summary>
        /// Specifies the description of the new build/pipeline folder.
        /// </summary>
        [Parameter]
        public string Description { get; set; }
    }
}