using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using TfsCmdlets.Util;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    /// <summary>
    /// Gets one or more Release/pipeline definition folders in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsReleaseDefinitionFolder")]
    [OutputType(typeof(WebApiFolder))]
    public class GetReleaseDefinitionFolder : CmdletBase
    {
        /// <summary>
        /// Specifies the folder path. Wildcards are supported. 
        /// When omitted, all Release/pipeline folders in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Path")]
        [SupportsWildcards()]
        public object Folder { get; set; } = "**";

        /// <summary>
        /// Specifies the query order. When omitted, defaults to None.
        /// </summary>
        [Parameter()]
        public FolderPathQueryOrder QueryOrder { get; set; } = FolderPathQueryOrder.None;

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }
    }

    // TODO

    //[Exports(typeof(WebApiFolder))]
    //internal partial class ReleaseFolderDataService : CollectionLevelController<WebApiFolder>
    //{
    //    //TODO: Inject TeamProject
    //    //TODO: Use NormalizePath

    //    protected override IEnumerable<WebApiFolder> DoGetItems()
    //    {
    //        var folder = parameters.Get<object>("Folder");
    //        var queryOrder = parameters.Get<FolderPathQueryOrder>("QueryOrder");

    //        while (true) switch (folder)
    //            {
    //                case WebApiFolder f:
    //                    {
    //                        yield return f;
    //                        yield break;
    //                    }
    //                case string s when s.IsWildcard():
    //                    {
    //                        var client = GetClient<ReleaseHttpClient>();
    //                        var tp = Project;
    //                        s = NodeUtil.NormalizeNodePath(s, tp.Name);
    //                        var folders = client.GetFoldersAsync(tp.Name, null, queryOrder)
    //                            .GetResult($"Error getting folders matching {s}");

    //                        foreach (var i in folders
    //                            .Where(f => f.Path.IsLike(s) || GetFolderName(f).IsLike(s)))
    //                        {
    //                            yield return i;
    //                        }

    //                        yield break;
    //                    }
    //                case string s:
    //                    {
    //                        var client = GetClient<ReleaseHttpClient>();
    //                        var tp = Project;
    //                        var f = client.GetFoldersAsync(tp.Name, NodeUtil.NormalizeNodePath(s, tp.Name), queryOrder)
    //                            .GetResult($"Error getting folders matching {s}").FirstOrDefault();

    //                        if (f != null) yield return f;

    //                        yield break;
    //                    }
    //                default:
    //                    {
    //                        throw new ArgumentException($"Invalid or non-existent pipeline folder '{folder}'");
    //                    }
    //            }
    //    }

    //    private static string GetFolderName(WebApiFolder folder)
    //    {
    //        return folder.Path.Equals(@"\") ?
    //            @"\" :
    //            folder.Path.Substring(folder.Path.LastIndexOf(@"\") + 1);
    //    }
    //}
}