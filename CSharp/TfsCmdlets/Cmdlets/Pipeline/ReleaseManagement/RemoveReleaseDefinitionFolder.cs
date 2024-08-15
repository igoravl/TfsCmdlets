using System.Management.Automation;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

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

    [CmdletController(typeof(WebApiFolder), Client=typeof(IReleaseHttpClient))]
    partial class RemoveReleaseDefinitionFolderController
    {
        protected override IEnumerable Run()
        {
            var folders = Data.GetItems<WebApiFolder>();
            var recurse = Parameters.Get<bool>(nameof(RemoveReleaseDefinitionFolder.Recurse));
            var force = Parameters.Get<bool>(nameof(RemoveReleaseDefinitionFolder.Force));
            var tp = Data.GetProject();

            foreach (var f in folders)
            {
                if (!PowerShell.ShouldProcess($"[Project: {tp.Name}]/[Folder: {f.Path}]", "Remove release definition folder")) continue;

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

                Client.DeleteFolderAsync(tp.Name, f.Path)
                    .Wait();
            }

            return null;
        }
    }
}