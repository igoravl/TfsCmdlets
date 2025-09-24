//HintName: TfsCmdlets.Cmdlets.Process.Field.GetProcessFieldDefinitionController.g.cs
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
namespace TfsCmdlets.Cmdlets.Process.Field
{
    internal partial class GetProcessFieldDefinitionController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient Client { get; }
        // Field
        protected bool Has_Field => Parameters.HasParameter(nameof(Field));
        protected IEnumerable Field
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Field), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // ReferenceName
        protected bool Has_ReferenceName { get; set; }
        protected string[] ReferenceName { get; set; }
        // Project
        protected bool Has_Project { get; set; }
        protected object Project { get; set; }
        // IncludeExtensionFields
        protected bool Has_IncludeExtensionFields { get; set; }
        protected bool IncludeExtensionFields { get; set; }
        // IncludeDeleted
        protected bool Has_IncludeDeleted { get; set; }
        protected bool IncludeDeleted { get; set; }
        // Collection
        protected bool Has_Collection => Parameters.HasParameter("Collection");
        protected Models.Connection Collection => Data.GetCollection();
        // Server
        protected bool Has_Server => Parameters.HasParameter("Server");
        protected Models.Connection Server => Data.GetServer();
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemField);
        protected override void CacheParameters()
        {
            // ReferenceName
            Has_ReferenceName = Parameters.HasParameter("ReferenceName");
            ReferenceName = Parameters.Get<string[]>("ReferenceName");
            // Project
            Has_Project = Parameters.HasParameter("Project");
            Project = Parameters.Get<object>("Project");
            // IncludeExtensionFields
            Has_IncludeExtensionFields = Parameters.HasParameter("IncludeExtensionFields");
            IncludeExtensionFields = Parameters.Get<bool>("IncludeExtensionFields");
            // IncludeDeleted
            Has_IncludeDeleted = Parameters.HasParameter("IncludeDeleted");
            IncludeDeleted = Parameters.Get<bool>("IncludeDeleted");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetProcessFieldDefinitionController(TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}