using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;

namespace TfsCmdlets.Controllers.WorkItem.AreasIterations
{
    [CmdletController(typeof(ClassificationNode))]
    partial class RenameClassificationNodeController
    {
        public override IEnumerable<ClassificationNode> Invoke()
        {
            var tp = Data.GetProject();
            var client = Data.GetClient<WorkItemTrackingHttpClient>();
            var nodeToRename = Data.GetItem<ClassificationNode>();
            var structureGroup = Parameters.Get<TreeStructureGroup>("StructureGroup");
            var structureGroupName = structureGroup.ToString().TrimEnd('s');
            var newName = Parameters.Get<string>("NewName");

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
    }
}