using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    internal abstract class MoveClassificationNodeController : ControllerBase
    {
        private IWorkItemTrackingHttpClient Client { get; set; }
        
        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var projectName = tp.Name;
            var sourceNodes = Data.GetItems<ClassificationNode>().ToList();
            var destination = Parameters.Get<object>("Destination");
            var force = Parameters.Get<bool>("Force");
            var structureGroup = Parameters.Get<TreeStructureGroup>("StructureGroup");

            if (sourceNodes == null) yield break;

            if (!Data.TryGetItem<ClassificationNode>(out var destinationNode, new { Node = destination }))
            {
                if (!force)
                {
                    ErrorUtil.ThrowIfNotFound(destinationNode, "Destination", destination);
                    yield break;
                }

                Logger.Log($"Creating missing destination node '{destinationNode}'");

                destinationNode = Data.NewItem<ClassificationNode>( new { Node = destination });
            }

            Logger.Log($"Destination node: '{destinationNode.FullPath}'");

            foreach (var sourceNode in sourceNodes)
            {
                Logger.Log($"Source node: '{sourceNode.FullPath}'");

                // var moveTo = $@"{destinationNode.Path}\{sourceNode.Name}";

                if (!PowerShell.ShouldProcess($"Team Project '{sourceNode.TeamProject}'", $"Move {structureGroup} '{sourceNode.FullPath}'"))
                {
                    yield break;
                }

                var patch = new WorkItemClassificationNode()
                {
                    Id = sourceNode.Id
                };

                var result = Client.CreateOrUpdateClassificationNodeAsync(patch, tp.Name, structureGroup, destinationNode.RelativePath)
                    .GetResult($"Error moving node '{sourceNode.RelativePath}' to '{destinationNode.RelativePath}'");

                yield return new ClassificationNode(result, projectName, Client);
            }
        }

        [ImportingConstructor]
        protected MoveClassificationNodeController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger, IWorkItemTrackingHttpClient client)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}