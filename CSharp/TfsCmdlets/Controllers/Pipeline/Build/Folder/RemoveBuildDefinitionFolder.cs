using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;
using TfsCmdlets.Cmdlets.Pipeline.Build.Folder;
using TfsCmdlets.Extensions;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;

namespace TfsCmdlets.Controllers.Pipeline.Build.Folder
{
    [CmdletController(typeof(WebApiFolder))]
    partial class RemoveBuildDefinitionFolderController
    {
        public override IEnumerable<WebApiFolder> Invoke()
        {
            var folders = Data.GetItems<WebApiFolder>();
            var recurse = Parameters.Get<bool>(nameof(RemoveBuildDefinitionFolder.Recurse));
            var force = Parameters.Get<bool>(nameof(RemoveBuildDefinitionFolder.Force));

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
                var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();

                if (!force)
                {
                    Logger.Log($"Force argument not set. Check if folder '{f.Path}' has build definitions");

                    var result = client.GetDefinitionsAsync2(tp.Name, null, null, null, DefinitionQueryOrder.None, null, null, null, null, f.Path)
                        .GetResult($"Error fetching build definitions in folder '{f.Path}'").ToList();

                    if (result.Count > 0)
                    {
                        throw new Exception($"Folder '{f.Path}' has {result.Count} build definition(s) under it. To delete it, use the -Force argument.");
                    }
                }

                client.DeleteFolderAsync(tp.Name, f.Path).Wait();
            }

            return null;
        }
    }
}