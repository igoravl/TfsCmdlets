using System.Management.Automation;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    /// <summary>
    /// Gets one or more Release/pipeline definition folders in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsReleaseDefinitionFolder")]
    [OutputType(typeof(WebApiFolder))]
    [TfsCmdlet(CmdletScope.Project)]
    partial class GetReleaseDefinitionFolder
    {
        /// <summary>
        /// Specifies the folder path. Wildcards are supported. 
        /// When omitted, all Release/pipeline folders in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Path")]
        [SupportsWildcards()]
        public object Folder { get; set; } = "**";

        /// <summary>
        /// Specifies the query order. When omitted, defaults to None.
        /// </summary>
        [Parameter]
        public FolderPathQueryOrder QueryOrder { get; set; } = FolderPathQueryOrder.None;
    }
}