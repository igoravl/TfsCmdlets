using System.Management.Automation;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.Build.Folder
{
    /// <summary>
    /// Creates a new build/pipeline definition folder
    /// </summary>
    /// <remarks>
    /// Folders are created recursively - i.e. when specifying a path like '\foo\bar\baz', if any of 
    /// the parent folders (foo, foo\bar) does not exist, it is automatically created before creating any
    /// child folders.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiFolder))]
    partial class NewBuildDefinitionFolder
    {
        /// <summary>
        /// Specifies the path of the new pipeline/build folder, including its name, 
        /// separated by backslashes (\).
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Path")]
        public object Folder { get; set; }

        /// <summary>
        /// Specifies the description of the new build/pipeline folder.
        /// </summary>
        [Parameter]
        public string Description { get; set; }
    }
}