using System.Management.Automation;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using TfsCmdlets.Extensions;
using TfsCmdlets.Util;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    /// <summary>
    /// Creates a new release definition folder.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsReleaseDefinitionFolder", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiFolder))]
    public class NewReleaseDefinitionFolder : NewCmdletBase<WebApiFolder>
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
        [Parameter()]
        public string Description { get; set; }
    }

    partial class ReleaseFolderDataService
    {
        protected override WebApiFolder DoNewItem()
        {
            var (_, tp) = GetCollectionAndProject();
            var folder = GetParameter<string>(nameof(NewReleaseDefinitionFolder.Folder));
            var description = GetParameter<string>(nameof(NewReleaseDefinitionFolder.Description));

            if (!ShouldProcess(tp, $"Create release folder '{folder}'")) return null;

            var client = GetClient<ReleaseHttpClient>();
            var newFolder = new WebApiFolder()
            {
                Description = description,
                Path = NodeUtil.NormalizeNodePath(folder, tp.Name)
            };

            return client.CreateFolderAsync(newFolder, tp.Name)
                .GetResult($"Error creating folder '{folder}'");
        }
    }
}