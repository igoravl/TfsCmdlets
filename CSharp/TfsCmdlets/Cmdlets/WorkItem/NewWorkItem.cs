using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Creates a new work item.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, SupportsShouldProcess = true, OutputType = typeof(WebApiWorkItem))]
    partial class NewWorkItem
    {
        /// <summary>
        /// Specifies the type of the new work item.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public object Type { get; set; }

        /// <summary>
        /// Specifies the title of the work item.
        /// </summary>
        [Parameter]
        [WorkItemField("System.Title", FieldType.String)]
        public string Title { get; set; }

        /// <summary>
        /// Specifies the description of the work item.
        /// </summary>
        [Parameter]
        [WorkItemField("System.Description", FieldType.String)]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the area path of the work item.
        /// </summary>
        [Parameter]
        [WorkItemField("System.AreaPath", FieldType.TreePath)]
        public string AreaPath { get; set; }

        /// <summary>
        /// Specifies the iteration path of the work item.
        /// </summary>
        [Parameter]
        [WorkItemField("System.IterationPath", FieldType.TreePath)]
        public string IterationPath { get; set; }

        /// <summary>
        /// Specifies the user this work item is assigned to.
        /// </summary>
        [Parameter]
        [WorkItemField("System.AssignedTo", FieldType.Identity)]
        public object AssignedTo { get; set; }

        /// <summary>
        /// Specifies the state of the work item.
        /// </summary>
        [Parameter]
        [WorkItemField("System.State", FieldType.String)]
        public string State { get; set; }

        /// <summary>
        /// Specifies the reason (field 'System.Reason') of the work item. 
        /// </summary>
        [Parameter]
        [WorkItemField("System.Reason", FieldType.String)]
        public string Reason { get; set; }

        /// <summary>
        /// Specifies the Value Area of the work item. 
        /// </summary>
        [Parameter]
        [WorkItemField("Microsoft.VSTS.Common.ValueArea", FieldType.String)]
        public string ValueArea { get; set; }

        /// <summary>
        /// Specifies the board column of the work item. 
        /// </summary>
        [Parameter]
        [WorkItemField("System.BoardColumn", FieldType.String)]
        public string BoardColumn { get; set; }

        /// <summary>
        /// Specifies whether the work item is in the sub-column Doing or Done in a board.
        /// </summary>
        [Parameter]
        [WorkItemField("System.BoardColumnDone", FieldType.Boolean)]
        public bool BoardColumnDone { get; set; }

        /// <summary>
        /// Specifies the board lane of the work item
        /// </summary>
        [Parameter]
        [WorkItemField("System.BoardLane", FieldType.String)]
        public string BoardLane { get; set; }

        /// <summary>
        /// Specifies the priority of the work item.
        /// </summary>
        [Parameter]
        [WorkItemField("Microsoft.VSTS.Common.Priority", FieldType.Integer)]
        public int Priority { get; set; }

        /// <summary>
        /// Specifies the tags of the work item.
        /// </summary>
        [Parameter]
        [WorkItemField("System.Tags", FieldType.PlainText)]
        public string[] Tags { get; set; }

        /// <summary>
        /// Specifies the names and the corresponding values for the fields to be set 
        /// in the work item and whose values were not supplied in the other arguments 
        /// to this cmdlet.
        /// </summary>
        [Parameter]
        public Hashtable Fields { get; set; }

        /// <summary>
        /// Bypasses any rule validation when saving the work item. Use it with caution, as this 
        /// may leave the work item in an invalid state.
        /// </summary>
        [Parameter]
        public SwitchParameter BypassRules { get; set; }

        /// <summary>
        /// Do not fire any notifications for this change. Useful for bulk operations and automated processes.
        /// </summary>
        [Parameter]
        public SwitchParameter SuppressNotifications { get; set; }
    }

    [CmdletController(typeof(WebApiWorkItem))]
    partial class NewWorkItemController
    {
        [Import]
        private IWorkItemPatchBuilder Builder { get; }

        protected override IEnumerable Run()
        {
            var type = Data.GetItem<WebApiWorkItemType>();

            if (!PowerShell.ShouldProcess(Project, $"Create new '{type}' work item")) yield break;

            var wi = new WebApiWorkItem();
            wi.Fields["System.TeamProject"] = Project.Name;
            wi.Fields["System.WorkItemType"] = type.Name;

            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            var result = client.CreateWorkItemAsync(Builder.GetJson(wi), Project.Name, type.Name, false, BypassRules, SuppressNotifications)
                .GetResult("Error creating work item");

            yield return result;
        }
    }
}