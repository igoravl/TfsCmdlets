using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;

namespace TfsCmdlets.Controllers.WorkItem.AreasIterations
{
    [CmdletController(typeof(ClassificationNode), CustomBaseClass = typeof(RemoveClassificationNodeController))]
    partial class RemoveAreaController { }

    [CmdletController(typeof(ClassificationNode), CustomBaseClass = typeof(RemoveClassificationNodeController))]
    partial class RemoveIterationController { }

    internal class RemoveClassificationNodeController: ControllerBase<Models.ClassificationNode>
    {
        public override IEnumerable<ClassificationNode> Invoke()
        {
            var nodes = Data.GetItems<ClassificationNode>().OrderByDescending(n => n.Path).ToList();
            var moveTo = Parameters.Get<object>("MoveTo");
            var recurse = Parameters.Get<bool>("Recurse");
            var structureGroup = Parameters.Get<TreeStructureGroup>("StructureGroup");
            var structureGroupName = structureGroup.ToString().TrimEnd('s');
            var moveToNode = Data.GetItem<ClassificationNode>(new { Node = moveTo });

            if (moveToNode == null)
            {
                Logger.LogError(new Exception($"Invalid or non-existent node '{moveTo}'. To remove nodes, supply a valid node in the -MoveTo argument"));
                return null;
            }

            Logger.Log($"Remove nodes and move orphaned work items to node '{moveToNode.FullPath}'");

            var tp = Data.GetProject();
            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            foreach (var node in nodes)
            {
                if (!PowerShell.ShouldProcess($"Team Project '{tp.Name}'", $"Remove {structureGroupName} '{node.RelativePath}'")) continue;

                if (node.ChildCount > 0 && !recurse && !PowerShell.ShouldContinue($"The {structureGroupName} at '{node.RelativePath}' " +
                    "has children and the Recurse parameter was not specified. If you continue, all children will be removed with " +
                    "the item. Are you sure you want to continue?"))
                {
                    continue;
                }

                client.DeleteClassificationNodeAsync(node.TeamProject, structureGroup, node.RelativePath, moveToNode.Id)
                    .Wait($"Error removing node '{node.FullPath}'");
            }

            return null;
        }

        [ImportingConstructor]
        protected RemoveClassificationNodeController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}