using System.Management.Automation;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;

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
}