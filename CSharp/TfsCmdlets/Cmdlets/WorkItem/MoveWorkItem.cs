using System.Management.Automation;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using TfsCmdlets.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Moves a work item to a different team project in the same collection.
    /// </summary>
    [Cmdlet(VerbsCommon.Move, "TfsWorkItem", SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiWorkItem))]
    public class MoveWorkItem : CmdletBase
    {
        /// <summary>
        /// Specifies a work item. Valid values are the work item ID or an instance of
        /// Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("id")]
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
        [Parameter()]
        public object Area { get; set; }

        /// <summary>
        /// Specifies the iteration path in the destination project where the work item will be moved to. 
        /// When omitted, the work item is moved to the root iteration path in the destination project.
        /// </summary>
        [Parameter()]
        public object Iteration { get; set; }

        /// <summary>
        /// Specifies a new state for the work item in the destination project. 
        /// When omitted, it retains the current state.
        /// </summary>
        [Parameter()]
        public string State { get; set; }

        /// <summary>
        /// Specifies a comment to be added to the history
        /// </summary>
        [Parameter()]
        public string Comment { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        // TODO

        ///// <summary>
        ///// Performs execution of the command
        ///// </summary>
        //protected override void DoProcessRecord()
        //{
        //    var wis = GetItems<WebApiWorkItem>();
        //    var (tpc, targetTp) = GetCollectionAndProject();

        //    string targetAreaPath;
        //    string targetIterationPath;

        //    if (Area != null)
        //    {
        //        var targetArea = GetItem<ClassificationNode>(new
        //        {
        //            Node = Area,
        //            StructureGroup = TreeStructureGroup.Areas
        //        });

        //        if (targetArea == null)
        //        {
        //            if (!PowerShell.ShouldProcess($"Team project '{targetTp.Name}'", $"Create area path {Area}"))
        //            {
        //                return;
        //            }

        //            targetArea = NewItem<ClassificationNode>(new
        //            {
        //                Node = Area,
        //                StructureGroup = TreeStructureGroup.Areas
        //            });
        //        }

        //        this.Log($"Moving to area {targetArea.Path}");
        //        targetAreaPath = $"{targetTp.Name}{targetArea.RelativePath}";
        //    }
        //    else
        //    {
        //        this.Log("Area not informed. Moving to root iteration.");
        //        targetAreaPath = targetTp.Name;
        //    }

        //    if (Iteration != null)
        //    {
        //        var targetIteration = GetItem<ClassificationNode>(new
        //        {
        //            Node = Iteration,
        //            StructureGroup = TreeStructureGroup.Iterations
        //        });

        //        if (targetIteration == null)
        //        {
        //            if (!PowerShell.ShouldProcess($"Team project '{targetTp.Name}'", $"Create iteration path {Iteration}"))
        //            {
        //                return;
        //            }

        //            targetIteration = NewItem<ClassificationNode>(new
        //            {
        //                Node = Iteration,
        //                StructureGroup = TreeStructureGroup.Iterations
        //            });

        //        }
        //        targetIterationPath = $"{targetTp.Name}{targetIteration.RelativePath}";
        //    }
        //    else
        //    {
        //        this.Log("Iteration not informed. Moving to root iteration.");
        //        targetIterationPath = targetTp.Name;
        //    }

        //    foreach (var wi in wis)
        //    {
        //        if (!PowerShell.ShouldProcess($"Work item {wi.Id}", $"Move work item to team project '{targetTp.Name}'"))
        //        {
        //            continue;
        //        }

        //        var patch = new JsonPatchDocument() {
        //            new JsonPatchOperation(){
        //                        Operation = Operation.Add,
        //                        Path = "/fields/System.TeamProject",
        //                        Value = targetTp.Name
        //            },
        //            new JsonPatchOperation() {
        //                        Operation = Operation.Add,
        //                        Path = "/fields/System.AreaPath",
        //                        Value = targetAreaPath
        //            },
        //            new JsonPatchOperation() {
        //                        Operation = Operation.Add,
        //                        Path = "/fields/System.IterationPath",
        //                        Value = targetIterationPath
        //            }
        //        };

        //        if (!string.IsNullOrEmpty(State))
        //        {
        //            patch.Add(new JsonPatchOperation()
        //            {
        //                Operation = Operation.Add,
        //                Path = "/fields/System.State",
        //                Value = State
        //            });
        //        }

        //        if (!string.IsNullOrEmpty(Comment))
        //        {
        //            patch.Add(new JsonPatchOperation()
        //            {
        //                Operation = Operation.Add,
        //                Path = "/fields/System.History",
        //                Value = Comment
        //            });
        //        }

        //        var client = GetClient<WorkItemTrackingHttpClient>();
        //        var result = client.UpdateWorkItemAsync(patch, (int)wi.Id)
        //            .GetResult("Error moving work item");

        //        if (Passthru)
        //        {
        //            WriteObject(GetItem<WebApiWorkItem>(new { WorkItem = (int)result.Id }));
        //        }
        //    }
        //}
    }
}