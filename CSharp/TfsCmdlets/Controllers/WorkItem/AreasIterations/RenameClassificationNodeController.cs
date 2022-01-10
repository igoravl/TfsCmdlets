using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;

namespace TfsCmdlets.Controllers.WorkItem.AreasIterations
{
    [CmdletController(typeof(ClassificationNode), CustomBaseClass = typeof(RenameClassificationNodeController))]
    partial class RenameAreaController { }

    [CmdletController(typeof(ClassificationNode), CustomBaseClass = typeof(RenameClassificationNodeController))]
    partial class RenameIterationController { }

    internal abstract class RenameClassificationNodeController: ControllerBase<Models.ClassificationNode>
    {
        [Import]
        private INodeUtil NodeUtil { get; }

        public override IEnumerable<ClassificationNode> Invoke()
        {
            var tp = Data.GetProject();
            var client = Data.GetClient<WorkItemTrackingHttpClient>();
            var nodeToRename = Data.GetItem<ClassificationNode>();
            var structureGroup = Parameters.Get<TreeStructureGroup>("StructureGroup");
            var structureGroupName = structureGroup.ToString().TrimEnd('s');
            var newName = NodeUtil.NormalizeNodePath(Parameters.Get<string>("NewName"));

            if(newName.Contains("\\"))
            {
                throw new ArgumentException($"New name cannot contain backslashes: {newName}");
            }

            if (!PowerShell.ShouldProcess($"{structureGroupName} '{nodeToRename.FullPath}'", $"Rename to '{newName}'"))
            {
                yield break;
            }

            var patch = new WorkItemClassificationNode()
            {
                Name = newName,
                Attributes = nodeToRename.Attributes
            };

            yield return new ClassificationNode(client.UpdateClassificationNodeAsync(patch, tp.Name, structureGroup, nodeToRename.RelativePath.Substring(1))
                .GetResult($"Error renaming node '{nodeToRename.RelativePath}'"), tp.Name, client);
        }

        [ImportingConstructor]
        protected RenameClassificationNodeController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}