using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.Build.Folder
{
    /// <summary>
    /// Gets one or more build/pipeline definition folders in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsBuildDefinitionFolder")]
    [OutputType(typeof(WebApiFolder))]
    public class GetBuildDefinitionFolder: CmdletBase
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
        [Parameter()]
        public FolderQueryOrder QueryOrder {get;set;} = FolderQueryOrder.None;

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        /// <value></value>
        [Parameter(ValueFromPipeline=true)]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        /// <value></value>
        [Parameter()]
        public object Collection { get; set; }
        
        // TODO


        //    /// <summary>
        //    /// Performs execution of the command
        //    /// </summary>
        //    protected override void DoProcessRecord()
        //    {
        //        WriteItems<WebApiFolder>();
        //    }
        //}

        //[Exports(typeof(WebApiFolder))]
        //internal class BuildFolderDataService : CollectionLevelController<WebApiFolder>
        //{
        //    protected override IEnumerable<WebApiFolder> DoGetItems()
        //    {
        //        var folder = parameters.Get<object>("Folder");
        //        var queryOrder = parameters.Get<FolderQueryOrder>("QueryOrder");

        //        while(true) switch(folder)
        //        {
        //            case WebApiFolder f:
        //            {
        //                yield return f;
        //                yield break;
        //            }
        //            case string s when s.IsWildcard():
        //            {
        //                var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>(parameters);
        //                var tp = Data.GetProject();
        //                var folders = client.GetFoldersAsync(tp.Name, null, queryOrder)
        //                    .GetResult($"Error getting folders matching {s}");

        //                foreach(var i in folders.Where(f => f.Path.IsLike(s) || GetFolderName(f).IsLike(s) )) yield return i;

        //                yield break;
        //            }
        //            case string s:
        //            {
        //                var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>(parameters);
        //                var tp = Data.GetProject();
        //                var f = client.GetFoldersAsync(tp.Name, $@"\{s.Trim('\\')}", queryOrder)
        //                    .GetResult($"Error getting folders matching {s}").FirstOrDefault();

        //                if(f != null) yield return f;

        //                yield break;
        //            }
        //            default:
        //            {
        //                throw new ArgumentException($"Invalid or non-existent pipeline folder '{folder}'");
        //            }
        //        }
        //    }

        //    private static string GetFolderName(WebApiFolder folder)
        //    {
        //        return folder.Path.Equals(@"\")?
        //            @"\":
        //            folder.Path.Substring(folder.Path.LastIndexOf(@"\")+1);
        //    }
    }
}