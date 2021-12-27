using Microsoft.TeamFoundation.Build.WebApi;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;

namespace TfsCmdlets.Controllers.Pipeline.Build.Folder
{
    [CmdletController(typeof(WebApiFolder))]
    partial class GetBuildDefinitionFolderController
    {
        public override IEnumerable<WebApiFolder> Invoke()
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
                            var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();
                            var tp = Data.GetProject();
                            var folders = client.GetFoldersAsync(tp.Name, null, queryOrder)
                                .GetResult($"Error getting folders matching {s}");

                            foreach (var i in folders.Where(f => f.Path.IsLike(s) || GetFolderName(f).IsLike(s))) yield return i;

                            yield break;
                        }
                    case string s:
                        {
                            var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();
                            var tp = Data.GetProject();
                            var f = client.GetFoldersAsync(tp.Name, $@"\{s.Trim('\\')}", queryOrder)
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