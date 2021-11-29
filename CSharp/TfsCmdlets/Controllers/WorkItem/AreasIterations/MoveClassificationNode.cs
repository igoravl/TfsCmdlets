using System.Collections.Generic;
using System.Composition;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Controllers.WorkItem.AreasIterations
{
    [CmdletController]
    internal class MoveClassificationNode : ControllerBase<ClassificationNode>
    {
        public INodeUtil NodeUtil { get; }

        public override IEnumerable<ClassificationNode> Invoke(ParameterDictionary parameters)
        {
            var tp = Data.GetProject(parameters);
            var projectName = tp.Name;
            var sourceNodes = Data.GetItems<ClassificationNode>(parameters);
            var destination = parameters.Get<object>("Destination");
            var force = parameters.Get<bool>("Force");
            var structureGroup = parameters.Get<TreeStructureGroup>("StructureGroup");

            if (sourceNodes == null) yield break;

            var destinationNode = Data.GetItem<ClassificationNode>(parameters.Override(new { Node = destination }));

            if (destinationNode == null && !force)
            {
                ErrorUtil.ThrowIfNotFound(destinationNode, "Destination", destination);
            }

            Logger.Log($"Destination node: '{destinationNode.FullPath}'");

            foreach (var sourceNode in sourceNodes)
            {
                Logger.Log($"Source node: '{sourceNode.FullPath}'");

                var moveTo = $@"{destinationNode.Path}\{sourceNode.Name}";

                if (!PowerShell.ShouldProcess($"Team Project '{sourceNode.TeamProject}'", $"Move {structureGroup} '{sourceNode.FullPath}'"))
                {
                    yield break;
                }

                var patch = new WorkItemClassificationNode()
                {
                    Id = sourceNode.Id
                };

                var client = Data.GetClient<WorkItemTrackingHttpClient>(parameters);

                var result = client.CreateOrUpdateClassificationNodeAsync(patch, tp.Name, structureGroup, destinationNode.RelativePath.Substring(1))
                    .GetResult($"Error moving node {sourceNode.RelativePath} to {destinationNode.RelativePath}");

                yield return new ClassificationNode(result, projectName, client);
            }
        }

        [ImportingConstructor]
        public MoveClassificationNode(INodeUtil nodeUtil, IPowerShellService powerShell, IDataManager data, ILogger logger)
            : base(powerShell, data, logger)
        {
            NodeUtil = nodeUtil;
        }
    }
}
