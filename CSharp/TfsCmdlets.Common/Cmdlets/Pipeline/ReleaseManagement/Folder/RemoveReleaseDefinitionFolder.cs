using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using TfsCmdlets.Extensions;
using TfsCmdlets.Util;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.Release.Folder
{
    /// <summary>
    /// Deletes one or more release definition folders.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsReleaseDefinitionFolder", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiFolder))]
    public class RemoveReleaseDefinitionFolder : RemoveCmdletBase<WebApiFolder>
    {
        /// <summary>
        /// Specifies the path of the release folder to delete. Wildcards are supperted.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        [Alias("Path")]
        [SupportsWildcards()]
        public object Folder { get; set; }

        /// <summary>
        /// Removes folders recursively. When omitted, folders with subfolders cannot be deleted.
        /// </summary>
        [Parameter()]
        public SwitchParameter Recurse { get; set; }

        /// <summary>
        /// Forces the exclusion of folders containing release definitions definitions. When omitted, 
        /// only empty folders can be deleted.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }
    }

    partial class ReleaseFolderDataService
    {
        protected override void DoRemoveItem()
        {
            var folders = GetItems<WebApiFolder>();
            var recurse = GetParameter<bool>(nameof(RemoveReleaseDefinitionFolder.Recurse));
            var force = GetParameter<bool>(nameof(RemoveReleaseDefinitionFolder.Force));
            var (_, tp) = GetCollectionAndProject();

            foreach (var f in folders)
            {
                if (!ShouldProcess(tp, $"Remove release folder '{f.Path}'"))
                {
                    continue;
                }

                if (!recurse)
                {
                    this.Log($"Recurse argument not set. Check if folder '{f.Path}' has sub-folders");

                    var subFolders = GetItems<WebApiFolder>(new ParameterDictionary()
                    {
                        ["Folder"] = $@"{f.Path}\**"
                    }).ToList();

                    if (subFolders.Count > 0)
                    {
                        throw new Exception($"Folder '{f.Path}' has {subFolders.Count} folder(s) under it. To delete it, use the -Recurse argument.");
                    }
                }

                if (!force && !ShouldContinue($"Are you sure you want to delete folder '{f.Path}' and all of its contents?")) continue;

                GetClient<ReleaseHttpClient>()
                    .DeleteFolderAsync(tp.Name, f.Path)
                    .Wait();
            }
        }
    }
}