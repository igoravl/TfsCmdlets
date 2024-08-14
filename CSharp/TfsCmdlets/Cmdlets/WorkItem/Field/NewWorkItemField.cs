using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.Field
{
    /// <summary>
    /// Gets information from one or more process templates.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WorkItemField))]
    partial class NewWorkItemField
    {
        /// <summary>
        /// Specifies the name of the field.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("Name")]
        public string Field { get; set; }

        /// <summary>
        /// Specifies the reference name of the field. It should contain only letters, numbers, dots and underscores.
        /// </summary>
        [Parameter(Position = 1)]
        [Alias("Name")]
        public string ReferenceName { get; set; }

        /// <summary>
        /// Specifies the description of the field.
        /// </summary>
        [Parameter]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the type of the field.
        /// </summary>
        [Parameter]
        public FieldType Type { get; set; } = FieldType.String;

        /// <summary>
        /// Specifies whether the field is read-only.
        /// </summary>
        [Parameter]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Specifies whether the field is sortable in server queries.
        /// </summary>
        [Parameter]
        public bool CanSortBy { get; set; }

        /// <summary>
        /// Specifies whether the field can be queried in the server.
        /// </summary>
        [Parameter]
        public bool IsQueryable { get; set; }

        /// <summary>
        /// Specifies whether the field is an identity field.
        /// </summary>
        [Parameter]
        public SwitchParameter IsIdentity { get; set; }

        /// <summary>
        /// Specifies the contents of the picklist. Supplying values to this parameter will automatically 
        /// turn the field into a picklist.
        /// </summary>
        [Parameter]
        public object[] PicklistItems { get; set; }

        /// <summary>
        /// Specifies whether the user can enter a custom value in the picklist,making it a list of suggested values instead of allowed values.
        /// </summary>
        [Parameter]
        public SwitchParameter PicklistSuggested { get; set; }
    }

    // Controller

    [CmdletController]
    partial class NewWorkItemFieldController
    {
        protected override IEnumerable Run()
        {
            throw new NotImplementedException();
        }
    }
}