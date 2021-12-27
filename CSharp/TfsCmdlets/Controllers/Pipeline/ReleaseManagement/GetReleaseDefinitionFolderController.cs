using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;

namespace TfsCmdlets.Controllers.Pipeline.ReleaseManagement
{
    /// <summary>
    /// Gets one or more Release/pipeline definition folders in a team project.
    /// </summary>
    [CmdletController(typeof(WebApiFolder))]
    partial class GetReleaseDefinitionFolderController
    {
        [Import] 
        private INodeUtil NodeUtil { get; set; }

        public override IEnumerable<WebApiFolder> Invoke()
        {
            var folder = Parameters.Get<object>("Folder");
            var queryOrder = Parameters.Get<FolderPathQueryOrder>("QueryOrder");

            while (true) switch (folder)
                {
                    case WebApiFolder f:
                        {
                            yield return f;
                            yield break;
                        }
                    case string s when s.IsWildcard():
                        {
                            var client = Data.GetClient<ReleaseHttpClient>();
                            var tp = Data.GetProject();
                            s = NodeUtil.NormalizeNodePath(s, tp.Name);
                            var folders = client.GetFoldersAsync(tp.Name, null, queryOrder)
                                .GetResult($"Error getting folders matching {s}");

                            foreach (var i in folders
                                .Where(f => f.Path.IsLike(s) || GetFolderName(f).IsLike(s)))
                            {
                                yield return i;
                            }

                            yield break;
                        }
                    case string s:
                        {
                            var client = Data.GetClient<ReleaseHttpClient>();
                            var tp = Data.GetProject();
                            var f = client.GetFoldersAsync(tp.Name, NodeUtil.NormalizeNodePath(s, tp.Name), queryOrder)
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