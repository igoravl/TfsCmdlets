using System.Collections.Generic;
using System.Composition;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;

namespace TfsCmdlets.Controllers.Pipeline.ReleaseManagement
{
    [CmdletController(typeof(WebApiFolder))]
    partial class NewReleaseDefinitionFolderController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        public override IEnumerable<WebApiFolder> Invoke()
        {
            var tp = Data.GetProject();
            var folder = Parameters.Get<string>(nameof(NewReleaseDefinitionFolder.Folder));
            var description = Parameters.Get<string>(nameof(NewReleaseDefinitionFolder.Description));

            if (!PowerShell.ShouldProcess(tp, $"Create release folder '{folder}'")) yield break;

            var client = Data.GetClient<ReleaseHttpClient>();
            var newFolder = new WebApiFolder()
            {
                Description = description,
                Path = NodeUtil.NormalizeNodePath(folder, tp.Name)
            };

            yield return client.CreateFolderAsync(newFolder, tp.Name)
                .GetResult($"Error creating folder '{folder}'");
        }
    }
}