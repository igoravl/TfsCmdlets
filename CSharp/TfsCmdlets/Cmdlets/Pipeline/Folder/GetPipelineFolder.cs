using System.Management.Automation;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;
using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Folder
{
    /// <summary>
    /// Gets one or more build/pipeline definition folders in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(WebApiFolder))]
    partial class GetPipelineFolder
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

    [CmdletController(typeof(WebApiFolder), Client=typeof(IBuildHttpClient))]
    partial class GetPipelineFolderController
    {
        protected override IEnumerable Run()
        {
            var folder = Parameters.Get<object>("Folder");
            var queryOrder = Parameters.Get<FolderQueryOrder>("QueryOrder");

            while (true) switch (folder)
                {
                    case WebApiFolder f:
                        {
                            yield return f;
                            yield break;
                        }
                    case string s when s.IsWildcard():
                        {
                            var tp = Data.GetProject();
                            var folders = Client.GetFoldersAsync(tp.Name, null, queryOrder)
                                .GetResult($"Error getting folders matching {s}");

                            foreach (var i in folders.Where(f => f.Path.IsLike(s) || GetFolderName(f).IsLike(s))) yield return i;

                            yield break;
                        }
                    case string s:
                        {
                            var tp = Data.GetProject();
                            var f = Client.GetFoldersAsync(tp.Name, $@"\{s.Trim('\\')}", queryOrder)
                                .GetResult($"Error getting folders matching {s}").FirstOrDefault();

                            if (f != null) yield return f;

                            yield break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent pipeline folder '{folder}'");
                        }
                }
        }

        private static string GetFolderName(WebApiFolder folder)
        {
            return folder.Path.Equals(@"\") ?
                @"\" :
                folder.Path.Substring(folder.Path.LastIndexOf(@"\") + 1);
        }
    }
}