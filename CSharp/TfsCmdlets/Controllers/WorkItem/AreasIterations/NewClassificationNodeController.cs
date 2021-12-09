using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.WorkItem.AreasIterations
{
    [CmdletController(typeof(ClassificationNode))]
    partial class NewClassificationNodeController
    {
        [Import]
        private INodeUtil NodeUtil { get; }

        public override IEnumerable<ClassificationNode> Invoke()
        {
            var tp = Data.GetProject();
            var node = Parameters.Get<string>("Node");
            var structureGroup = Parameters.Get<TreeStructureGroup>("StructureGroup");
            var force = Parameters.Get<bool>("Force");

            var nodeType = structureGroup.ToString().TrimEnd('s');
            var nodePath = NodeUtil.NormalizeNodePath(node, tp.Name, scope: nodeType, includeTeamProject: true);
            var client = Data.GetClient<WorkItemTrackingHttpClient>();
            var parentPath = Path.GetDirectoryName(nodePath);
            var nodeName = Path.GetFileName(nodePath);

            if (!PowerShell.ShouldProcess($"Team Project {tp.Name}", $"Create node '{nodePath}'")) yield break;

            if (!Data.TestItem<ClassificationNode>(new { Node = parentPath }))
            {
                if (!force)
                {
                    Logger.Log($"Parent node '{parentPath}' does not exist");
                    throw new Exception($"Parent node '{parentPath}' does not exist. Check the path or use -Force the create any missing parent nodes.");
                }

                Data.NewItem<ClassificationNode>(new { Node = parentPath });
            }

            var patch = new WorkItemClassificationNode()
            {
                Name = nodeName
            };

            if (Parameters.HasParameter("StartDate"))
            {
                if (!Parameters.HasParameter("FinishDate"))
                {
                    throw new ArgumentException("When specifying iteration dates, both dates must be supplied.");
                }

                var startDate = Parameters.Get<DateTime?>("StartDate");
                var finishDate = Parameters.Get<DateTime?>("FinishDate");

                Logger.Log($"Setting iteration dates to '{startDate}' and '{finishDate}'");

                patch.Attributes = new Dictionary<string, object>()
                {
                    ["startDate"] = startDate,
                    ["finishDate"] = finishDate
                };
            }

            var result = client.CreateOrUpdateClassificationNodeAsync(patch, tp.Name, structureGroup, parentPath)
                .GetResult($"Error creating node {nodePath}");

            yield return new ClassificationNode(result, tp.Name, client);
        }
    }
}