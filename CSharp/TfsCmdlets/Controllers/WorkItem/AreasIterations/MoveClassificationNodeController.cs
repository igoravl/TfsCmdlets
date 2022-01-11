using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;
using TfsCmdlets.Util;

namespace TfsCmdlets.Controllers.WorkItem.AreasIterations
{
    [CmdletController(typeof(ClassificationNode), CustomBaseClass = typeof(MoveClassificationNodeController))]
    partial class MoveAreaController { }

    [CmdletController(typeof(ClassificationNode), CustomBaseClass = typeof(MoveClassificationNodeController))]
    partial class MoveIterationController { }

    internal abstract class MoveClassificationNodeController: ControllerBase
    {
        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var projectName = tp.Name;
            var sourceNodes = Data.GetItems<ClassificationNode>();
            var destination = Parameters.Get<object>("Destination");
            var force = Parameters.Get<bool>("Force");
            var structureGroup = Parameters.Get<TreeStructureGroup>("StructureGroup");

            if (sourceNodes == null) yield break;

            var destinationNode = Data.GetItem<ClassificationNode>(new { Node = destination });

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

                var client = Data.GetClient<WorkItemTrackingHttpClient>();

                var result = client.CreateOrUpdateClassificationNodeAsync(patch, tp.Name, structureGroup, destinationNode.RelativePath.Substring(1))
                    .GetResult($"Error moving node {sourceNode.RelativePath} to {destinationNode.RelativePath}");

                yield return new ClassificationNode(result, projectName, client);
            }
        }

        [ImportingConstructor]
        protected MoveClassificationNodeController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}