using System.Management.Automation;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using WebApiBoard = Microsoft.TeamFoundation.Work.WebApi.Board;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Deletes a work item from a team project collection.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsWorkItem", SupportsShouldProcess = true)]
    [TfsCmdlet(CmdletScope.Collection)]
    partial class RemoveWorkItem
    {
        /// <summary>
        /// Specifies the work item to remove.
        /// </summary>
        /// <seealso cref="Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem">
        /// A WorkItem object
        /// </seealso>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Permanently deletes the work item, without sending it to the recycle bin.
        /// </summary>
        [Parameter]
        public SwitchParameter Destroy { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }
}