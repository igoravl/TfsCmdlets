using System.Management.Automation;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;
using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Folder
{
    /// <summary>
    /// Deletes one or more build/pipeline definition folders.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiFolder))]
    partial class RemovePipelineFolder
    {
        /// <summary>
        /// Specifies the path of the pipeline/build folder to delete, including its name, 
        /// separated by backslashes (\). Wildcards are supported.
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
        /// Forces the exclusion of folders containing build/pipelines definitions. When omitted, 
        /// only empty folders can be deleted.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(WebApiFolder), Client=typeof(IBuildHttpClient))]
    partial class RemovePipelineFolderController
    {
        protected override IEnumerable Run()
        {
            var folders = Data.GetItems<WebApiFolder>();
            var recurse = Parameters.Get<bool>(nameof(RemovePipelineFolder.Recurse));
            var force = Parameters.Get<bool>(nameof(RemovePipelineFolder.Force));

            foreach (var f in folders)
            {
                if (!PowerShell.ShouldProcess($"Team Project '{f.Project.Name}'", $"Remove folder '{f.Path}'"))
                {
                    continue;
                }

                if (!recurse)
                {
                    Logger.Log($"Recurse argument not set. Check if folder '{f.Path}' has sub-folders");

                    var path = $@"{f.Path.TrimEnd('\\')}\**";
                    var subFolders = Data.GetItems<WebApiFolder>(new
                    {
                        Folder = path
                    }).ToList();

                    if (subFolders.Count > 0)
                    {
                        throw new Exception($"Folder '{f.Path}' has {subFolders.Count} folder(s) under it. To delete it, use the -Recurse argument.");
                    }
                }

                var tp = Data.GetProject();

                if (!force)
                {
                    Logger.Log($"Force argument not set. Check if folder '{f.Path}' has build definitions");

                    var result = Client.GetDefinitionsAsync2(project: tp.Name, queryOrder: DefinitionQueryOrder.None, path: f.Path, yamlFilename: null)
                        .GetResult($"Error fetching build definitions in folder '{f.Path}'").ToList();

                    if (result.Count > 0)
                    {
                        throw new Exception($"Folder '{f.Path}' has {result.Count} build definition(s) under it. To delete it, use the -Force argument.");
                    }
                }

                Client.DeleteFolderAsync(tp.Name, f.Path).Wait();
            }

            return null;
        }
    }
}