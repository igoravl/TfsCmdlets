using System.Management.Automation;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

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

    [CmdletController(typeof(WebApiFolder), Client=typeof(IReleaseHttpClient))]
    partial class NewReleaseDefinitionFolderController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var folder = Parameters.Get<string>(nameof(NewReleaseDefinitionFolder.Folder));
            var description = Parameters.Get<string>(nameof(NewReleaseDefinitionFolder.Description));

            if (!PowerShell.ShouldProcess(tp, $"Create release folder '{folder}'")) yield break;

            var newFolder = new WebApiFolder()
            {
                Description = description,
                Path = NodeUtil.NormalizeNodePath(folder, tp.Name)
            };

            yield return Client.CreateFolderAsync(newFolder, tp.Name)
                .GetResult($"Error creating folder '{folder}'");
        }
    }
}