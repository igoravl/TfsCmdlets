using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Models;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class MoveWorkItemController
    {
        public override IEnumerable<WebApiWorkItem> Invoke()
        {
            var wis = Data.GetItems<WebApiWorkItem>();
            var targetTp = Data.GetProject();
            var area = Parameters.Get<string>("Area");
            var iteration = Parameters.Get<string>("Iteration");
            var state = Parameters.Get<string>("State");
            var comment = Parameters.Get<string>("Comment");
            var passthru = Parameters.Get<bool>("Passthru");

            string targetAreaPath;
            string targetIterationPath;

            if (area != null)
            {
                var targetArea = Data.GetItem<ClassificationNode>(new { Node = area, StructureGroup = TreeStructureGroup.Areas });

                if (targetArea == null)
                {
                    if (!PowerShell.ShouldProcess($"Team project '{targetTp.Name}'", $"Create area path {area}")) return null;

                    targetArea = Data.NewItem<ClassificationNode>(new
                    {
                        Node = area,
                        StructureGroup = TreeStructureGroup.Areas
                    });
                }

                Logger.Log($"Moving to area {targetArea.Path}");
                targetAreaPath = $"{targetTp.Name}{targetArea.RelativePath}";
            }
            else
            {
                Logger.Log("Area not informed. Moving to root iteration.");
                targetAreaPath = targetTp.Name;
            }

            if (iteration != null)
            {
                var targetIteration = Data.GetItem<ClassificationNode>(new { Node = iteration, StructureGroup = TreeStructureGroup.Iterations });

                if (targetIteration == null)
                {
                    if (!PowerShell.ShouldProcess($"Team project '{targetTp.Name}'", $"Create iteration path {iteration}")) return null;

                    targetIteration = Data.NewItem<ClassificationNode>(new
                    {
                        Node = iteration,
                        StructureGroup = TreeStructureGroup.Iterations
                    });

                }
                targetIterationPath = $"{targetTp.Name}{targetIteration.RelativePath}";
            }
            else
            {
                Logger.Log("Iteration not informed. Moving to root iteration.");
                targetIterationPath = targetTp.Name;
            }

            List<int> ids = new List<int>();

            foreach (var wi in wis)
            {
                if (!PowerShell.ShouldProcess($"Work item {wi.Id}", $"Move work item to team project '{targetTp.Name}'")) continue;

                var patch = new JsonPatchDocument() {
                   new JsonPatchOperation(){
                               Operation = Operation.Add,
                               Path = "/fields/System.TeamProject",
                               Value = targetTp.Name
                   },
                   new JsonPatchOperation() {
                               Operation = Operation.Add,
                               Path = "/fields/System.AreaPath",
                               Value = targetAreaPath
                   },
                   new JsonPatchOperation() {
                               Operation = Operation.Add,
                               Path = "/fields/System.IterationPath",
                               Value = targetIterationPath
                   }
               };

                if (!string.IsNullOrEmpty(state))
                {
                    patch.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.State",
                        Value = state
                    });
                }

                if (!string.IsNullOrEmpty(comment))
                {
                    patch.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = "/fields/System.History",
                        Value = comment
                    });
                }

                var client = Data.GetClient<WorkItemTrackingHttpClient>();
                var result = client.UpdateWorkItemAsync(patch, (int)wi.Id)
                    .GetResult("Error moving work item");

                ids.Add((int)result.Id);
            }

            return Data.GetItems<WebApiWorkItem>(new { WorkItem = ids });
        }
    }
}