using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Moves a work item to a different team project in the same collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true, OutputType = typeof(WebApiWorkItem))]
    partial class MoveWorkItem
    {
        /// <summary>
        /// Specifies a work item. Valid values are the work item ID or an instance of
        /// Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("Id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Specifies the team project where the work item will be moved to.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [Alias("Destination")]
        public object Project { get; set; }

        /// <summary>
        /// Specifies the area path in the destination project where the work item will be moved to. 
        /// When omitted, the work item is moved to the root area path in the destination project.
        /// </summary>
        [Parameter]
        public object Area { get; set; }

        /// <summary>
        /// Specifies the iteration path in the destination project where the work item will be moved to. 
        /// When omitted, the work item is moved to the root iteration path in the destination project.
        /// </summary>
        [Parameter]
        public object Iteration { get; set; }

        /// <summary>
        /// Specifies a new state for the work item in the destination project. 
        /// When omitted, it retains the current state.
        /// </summary>
        [Parameter]
        public string State { get; set; }

        /// <summary>
        /// Specifies a comment to be added to the history
        /// </summary>
        [Parameter]
        public string Comment { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter]
        public SwitchParameter Passthru { get; set; }
    }

    [CmdletController(typeof(WebApiWorkItem), Client=typeof(IWorkItemTrackingHttpClient))]
    partial class MoveWorkItemController
    {
        protected override IEnumerable Run()
        {
            var wis = Data.GetItems<WebApiWorkItem>();
            var targetTp = Data.GetProject();
            var area = Parameters.Get<string>("Area");
            var iteration = Parameters.Get<string>("Iteration");
            var state = Parameters.Get<string>("State");
            var comment = Parameters.Get<string>("Comment");

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

            var ids = new List<int>();

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

                var result = Client.UpdateWorkItemAsync(patch, (int)wi.Id)
                    .GetResult("Error moving work item");

                ids.Add((int)result.Id);
            }

            return Data.GetItems<WebApiWorkItem>(new { WorkItem = ids });
        }
    }
}