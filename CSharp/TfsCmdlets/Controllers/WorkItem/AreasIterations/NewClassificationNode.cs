using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Controllers.WorkItem.AreasIterations
{
    [CmdletController]
    internal class NewClassificationNode : ControllerBase<ClassificationNode>
    {
        public INodeUtil NodeUtil { get; }

        public override IEnumerable<ClassificationNode> Invoke(ParameterDictionary parameters)
        {
            var tp = Data.GetProject(parameters);
            var node = parameters.Get<string>("Node");
            var structureGroup = parameters.Get<TreeStructureGroup>("StructureGroup");
            var force = parameters.Get<bool>("Force");

            var nodeType = structureGroup.ToString().TrimEnd('s');
            var nodePath = NodeUtil.NormalizeNodePath(node, tp.Name, nodeType, false, false, true);
            var client = Data.GetClient<WorkItemTrackingHttpClient>(parameters);
            var parentPath = Path.GetDirectoryName(nodePath);
            var nodeName = Path.GetFileName(nodePath);

            if (!PowerShell.ShouldProcess($"Team Project {tp.Name}", $"Create node '{nodePath}'")) yield break;

            if (!Data.TestItem<ClassificationNode>(parameters.Override(new { Node = parentPath })))
            {
                if (!force)
                {
                    Logger.Log($"Parent node '{parentPath}' does not exist");
                    throw new Exception($"Parent node '{parentPath}' does not exist. Check the path or use -Force the create any missing parent nodes.");
                }

                Data.NewItem<ClassificationNode>(parameters.Override(new { Node = parentPath }));
            }

            var patch = new WorkItemClassificationNode()
            {
                Name = nodeName
            };

            if (parameters.HasParameter("StartDate"))
            {
                if (!parameters.HasParameter("FinishDate"))
                {
                    throw new ArgumentException("When specifying iteration dates, both dates must be supplied.");
                }

                var startDate = parameters.Get<DateTime?>("StartDate");
                var finishDate = parameters.Get<DateTime?>("FinishDate");

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

        [ImportingConstructor]
        public NewClassificationNode(INodeUtil nodeUtil, IPowerShellService powerShell, IDataManager data, ILogger logger)
            : base(powerShell, data, logger)
        {
            NodeUtil = nodeUtil;
        }
    }
}
