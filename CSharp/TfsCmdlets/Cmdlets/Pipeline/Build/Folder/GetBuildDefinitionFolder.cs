using System.Management.Automation;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.Build.Folder
{
    /// <summary>
    /// Gets one or more build/pipeline definition folders in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(WebApiFolder))]
    partial class GetBuildDefinitionFolder
    {
        /// <summary>
        /// Specifies the folder path. Wildcards are supported. 
        /// When omitted, all build/pipeline folders in the supplied team project are returned.
        /// </summary>
        [Parameter(Position=0)]
        [Alias("Path")]
        [SupportsWildcards()]
        public object Folder { get; set; } = "**";

        /// <summary>
        /// Specifies the query order. When omitted, defaults to None.
        /// </summary>
        [Parameter]
        public Microsoft.TeamFoundation.Build.WebApi.FolderQueryOrder QueryOrder {get;set;}
    }
}