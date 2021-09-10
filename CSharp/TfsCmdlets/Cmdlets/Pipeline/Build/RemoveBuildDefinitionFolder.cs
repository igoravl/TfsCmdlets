using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;
using TfsCmdlets.Extensions;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.Build
{
    /// <summary>
    /// Deletes one or more build/pipeline definition folders.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsBuildDefinitionFolder", SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiFolder))]
    public class RemoveBuildDefinitionFolder : CmdletBase
    {
        /// <summary>
        /// Specifies the path of the pipeline/build folder to delete, including its name, 
        /// separated by backslashes (\). Wildcards are supperted.
        /// </summary>
        [Parameter(Position=0, ValueFromPipeline=true, ValueFromPipelineByPropertyName=true, Mandatory=true)]
        [Alias("Path")]
        [SupportsWildcards()]
        public object Folder { get; set; }

        /// <summary>
        /// Removes folders recursively. When omitted, folders with subfolders cannot be deleted.
        /// </summary>
        [Parameter()]
        public SwitchParameter Recurse { get; set; }

        /// <summary>
        /// Forces the exclusion of folders containing build/pipelines definitions. When omitted, 
        /// only empty folders can be deleted.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            var folders = GetItems<WebApiFolder>();

            foreach(var f in folders)
            {
                if(!ShouldProcess($"Team Project '{f.Project.Name}'", $"Remove folder '{f.Path}'"))
                {
                    continue;
                }

                if(!Recurse)
                {
                    this.Log($"Recurse argument not set. Check if folder '{f.Path}' has sub-folders");

                    var path = $@"{f.Path.TrimEnd('\\')}\**";
                    var subFolders = GetItems<WebApiFolder>(new {
                        Folder = path
                    }).ToList();

                    if(subFolders.Count > 0)
                    {
                        throw new Exception($"Folder '{f.Path}' has {subFolders.Count} folder(s) under it. To delete it, use the -Recurse argument.");
                    }
                }

                var (_, tp) = GetCollectionAndProject();
                var client = GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();

                if(!Force)
                {
                    this.Log($"Force argument not set. Check if folder '{f.Path}' has build definitions");

                    var result = client.GetDefinitionsAsync2(tp.Name, null, null, null, DefinitionQueryOrder.None, null, null, null, null, f.Path)
                        .GetResult($"Error fetching build definitions in folder '{f.Path}'").ToList();

                    if(result.Count > 0)
                    {
                        throw new Exception($"Folder '{f.Path}' has {result.Count} build definition(s) under it. To delete it, use the -Force argument.");
                    }
                }

                client.DeleteFolderAsync(tp.Name, f.Path).Wait();
            }
        }
    }
}