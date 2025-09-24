//HintName: TfsCmdlets.Cmdlets.Process.Field.NewProcessFieldDefinitionController.g.cs
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using ProcessFieldType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.FieldType;
namespace TfsCmdlets.Cmdlets.Process.Field
{
    internal partial class NewProcessFieldDefinitionController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient Client { get; }
        // Field
        protected bool Has_Field { get; set; }
        protected string Field { get; set; }
        // ReferenceName
        protected bool Has_ReferenceName { get; set; }
        protected string ReferenceName { get; set; }
        // Description
        protected bool Has_Description { get; set; }
        protected string Description { get; set; }
        // Type
        protected bool Has_Type { get; set; }
        protected Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.FieldType Type { get; set; }
        // ReadOnly
        protected bool Has_ReadOnly { get; set; }
        protected bool ReadOnly { get; set; }
        // CanSortBy
        protected bool Has_CanSortBy { get; set; }
        protected bool CanSortBy { get; set; }
        // IsQueryable
        protected bool Has_IsQueryable { get; set; }
        protected bool IsQueryable { get; set; }
        // IsIdentity
        protected bool Has_IsIdentity { get; set; }
        protected bool IsIdentity { get; set; }
        // PicklistItems
        protected bool Has_PicklistItems { get; set; }
        protected object[] PicklistItems { get; set; }
        // PicklistSuggested
        protected bool Has_PicklistSuggested { get; set; }
        protected bool PicklistSuggested { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
        // Collection
        protected bool Has_Collection => Parameters.HasParameter("Collection");
        protected Models.Connection Collection => Data.GetCollection();
        // Server
        protected bool Has_Server => Parameters.HasParameter("Server");
        protected Models.Connection Server => Data.GetServer();
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        protected override void CacheParameters()
        {
            // Field
            Has_Field = Parameters.HasParameter("Field");
            Field = Parameters.Get<string>("Field");
            // ReferenceName
            Has_ReferenceName = Parameters.HasParameter("ReferenceName");
            ReferenceName = Parameters.Get<string>("ReferenceName");
            // Description
            Has_Description = Parameters.HasParameter("Description");
            Description = Parameters.Get<string>("Description");
            // Type
            Has_Type = Parameters.HasParameter("Type");
            Type = Parameters.Get<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.FieldType>("Type", ProcessFieldType.String);
            // ReadOnly
            Has_ReadOnly = Parameters.HasParameter("ReadOnly");
            ReadOnly = Parameters.Get<bool>("ReadOnly");
            // CanSortBy
            Has_CanSortBy = Parameters.HasParameter("CanSortBy");
            CanSortBy = Parameters.Get<bool>("CanSortBy");
            // IsQueryable
            Has_IsQueryable = Parameters.HasParameter("IsQueryable");
            IsQueryable = Parameters.Get<bool>("IsQueryable");
            // IsIdentity
            Has_IsIdentity = Parameters.HasParameter("IsIdentity");
            IsIdentity = Parameters.Get<bool>("IsIdentity");
            // PicklistItems
            Has_PicklistItems = Parameters.HasParameter("PicklistItems");
            PicklistItems = Parameters.Get<object[]>("PicklistItems");
            // PicklistSuggested
            Has_PicklistSuggested = Parameters.HasParameter("PicklistSuggested");
            PicklistSuggested = Parameters.Get<bool>("PicklistSuggested");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public NewProcessFieldDefinitionController(IWorkItemTrackingProcessHttpClient processClient, TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            ProcessClient = processClient;
            Client = client;
        }
    }
}