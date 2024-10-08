using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{


    public abstract class RenameClassificationNodeController: ControllerBase
    {
        private INodeUtil NodeUtil { get; set;  }

        private IWorkItemTrackingHttpClient Client { get; set; }

        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
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

            yield return new ClassificationNode(Client.UpdateClassificationNodeAsync(patch, tp.Name, structureGroup, nodeToRename.RelativePath)
                .GetResult($"Error renaming node '{nodeToRename.RelativePath}'"), tp.Name, Client);
        }

        [ImportingConstructor]
        protected RenameClassificationNodeController(INodeUtil nodeUtil, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger, IWorkItemTrackingHttpClient client)
            : base(powerShell, data, parameters, logger)
        {
            NodeUtil = nodeUtil;
            Client = client;
        }
    }
}