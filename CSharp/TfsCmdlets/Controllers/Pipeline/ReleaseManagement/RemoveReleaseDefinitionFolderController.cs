using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;

namespace TfsCmdlets.Controllers.Pipeline.ReleaseManagement
{
    /// <summary>
    /// Deletes one or more release definition folders.
    /// </summary>
    [CmdletController(typeof(WebApiFolder))]
    partial class RemoveReleaseDefinitionFolderController
    {
        public override IEnumerable<WebApiFolder> Invoke()
        {
            var folders = Data.GetItems<WebApiFolder>();
            var recurse = Parameters.Get<bool>(nameof(RemoveReleaseDefinitionFolder.Recurse));
            var force = Parameters.Get<bool>(nameof(RemoveReleaseDefinitionFolder.Force));
            var tp = Data.GetProject();

            foreach (var f in folders)
            {
                if (!PowerShell.ShouldProcess(tp, $"Remove release folder '{f.Path}'"))
                {
                    continue;
                }

                if (!recurse)
                {
                    Logger.Log($"Recurse argument not set. Check if folder '{f.Path}' has sub-folders");

                    var subFolders = Data.GetItems<WebApiFolder>(new
                    {
                        Folder = $@"{f.Path}\**"
                    }).ToList();

                    if (subFolders.Count > 0)
                    {
                        throw new Exception($"Folder '{f.Path}' has {subFolders.Count} folder(s) under it. To delete it, use the -Recurse argument.");
                    }
                }

                if (!force && !PowerShell.ShouldContinue($"Are you sure you want to delete folder '{f.Path}' and all of its contents?")) continue;

                Data.GetClient<ReleaseHttpClient>()
                    .DeleteFolderAsync(tp.Name, f.Path)
                    .Wait();
            }

            return null;
        }
    }
}