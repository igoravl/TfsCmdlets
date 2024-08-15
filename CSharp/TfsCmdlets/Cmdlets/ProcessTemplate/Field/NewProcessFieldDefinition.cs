using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using ProcessFieldType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.FieldType;

namespace TfsCmdlets.Cmdlets.Process.Field
{
    /// <summary>
    /// Gets information from one or more process templates.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true, OutputType = typeof(WorkItemField))]
    partial class NewProcessFieldDefinition
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
        [Parameter(Position = 1, Mandatory = true)]
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
        public ProcessFieldType Type { get; set; } = ProcessFieldType.String;

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
        /// Specifies whether the user can enter a custom value in the picklist, making it a list of suggested values instead of allowed values.
        /// </summary>
        [Parameter]
        public SwitchParameter PicklistSuggested { get; set; }
    }

    // Controller

    [CmdletController(Client = typeof(IWorkItemTrackingHttpClient))]
    partial class NewProcessFieldDefinitionController
    {

        [Import]
        private IWorkItemTrackingProcessHttpClient ProcessClient { get; set; }

        protected override IEnumerable Run()
        {
            var isPicklist = (PicklistItems != null) ||
                Type == ProcessFieldType.PicklistString ||
                Type == ProcessFieldType.PicklistInteger ||
                Type == ProcessFieldType.PicklistDouble;

            var fieldType = Type switch
            {
                ProcessFieldType.String or ProcessFieldType.PicklistString => ProcessFieldType.String,
                ProcessFieldType.Integer or ProcessFieldType.PicklistInteger => ProcessFieldType.Integer,
                ProcessFieldType.Double or ProcessFieldType.PicklistDouble => ProcessFieldType.Double,
                _ when isPicklist => throw new Exception("Picklist fields must be of type string, integer or double."),
                _ => Type
            };

            PickList picklist = null;

            if (isPicklist)
            {
                if (!PowerShell.ShouldProcess(Collection, $"Create picklist for field {ReferenceName} with items [{string.Join(", ", PicklistItems.Select(i => i.ToString()))}]"))
                {
                    yield break;
                }

                if ((PicklistItems?.Length ?? 0) == 0)
                {
                    throw new ArgumentException("Picklist fields must contain at least one item. Use the PicklistItems parameter to specify the items.");
                }

                picklist = CreatePicklist(fieldType, PicklistItems, PicklistSuggested);
            }

            if (!PowerShell.ShouldProcess(Collection, $"Create process field {ReferenceName} ('{Field}'), type {fieldType}"))
            {
                yield break;
            }

            yield return Client.CreateFieldAsync(new WorkItemField
            {
                Name = Field,
                ReferenceName = ReferenceName,
                Description = Description,
                Type = fieldType,
                ReadOnly = ReadOnly,
                CanSortBy = CanSortBy,
                IsQueryable = IsQueryable,
                IsIdentity = IsIdentity,
                PicklistId = picklist?.Id
            }).GetResult("Error creating field");
        }

        private PickList CreatePicklist(ProcessFieldType type, object[] picklistItems, bool picklistSuggested)
        {
            var fieldType = type.ToString().Substring(0, 1).ToUpper() + type.ToString().Substring(1).ToLower();

            var picklist = ProcessClient.CreateListAsync(new PickList
            {
                Id = Guid.Empty,
                Name = "picklist_" + Guid.NewGuid().ToString(),
                Type = fieldType,
                Items = PicklistItems.Select(i => i.ToString()).ToList(),
                IsSuggested = PicklistSuggested
            }).GetResult("Error creating picklist");

            return picklist;
        }
    }
}