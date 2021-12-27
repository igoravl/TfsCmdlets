using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;
using TfsCmdlets.Util;

namespace TfsCmdlets.Controllers.WorkItem.AreasIterations
{
    [CmdletController(typeof(ClassificationNode))]
    partial class SetClassificationNodeController
    {
        public override IEnumerable<ClassificationNode> Invoke()
        {
            var nodeToSet = Data.GetItem<ClassificationNode>();
            var startDate = Parameters.Get<DateTime?>("StartDate");
            var finishDate = Parameters.Get<DateTime?>("FinishDate");
            var structureGroup = Parameters.Get<TreeStructureGroup>("StructureGroup");

            ErrorUtil.ThrowIfNotFound(nodeToSet, "Node", Parameters.Get<string>("Node"));

            var tp = Data.GetProject();
            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            if (!PowerShell.ShouldProcess(tp, $"Set dates on iteration '{nodeToSet.RelativePath}'")) yield break;

            var patch = new WorkItemClassificationNode()
            {
                Attributes = new Dictionary<string, object>()
                {
                    ["startDate"] = startDate,
                    ["finishDate"] = finishDate
                }
            };

            var result = client.UpdateClassificationNodeAsync(patch, tp.Name, structureGroup, nodeToSet.RelativePath.Substring(1))
                .GetResult($"Error setting dates on iteration '{nodeToSet.FullPath}'");

            yield return new ClassificationNode(result, tp.Name, client);
        }
    }
}