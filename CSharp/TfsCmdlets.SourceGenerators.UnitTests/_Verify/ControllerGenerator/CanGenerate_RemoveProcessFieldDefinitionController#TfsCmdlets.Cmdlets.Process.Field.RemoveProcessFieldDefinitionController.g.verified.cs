//HintName: TfsCmdlets.Cmdlets.Process.Field.RemoveProcessFieldDefinitionController.g.cs
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
namespace TfsCmdlets.Cmdlets.Process.Field
{
    internal partial class RemoveProcessFieldDefinitionController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient Client { get; }
        // Field
        protected bool Has_Field { get; set; }
        protected object Field { get; set; }
        // ReferenceName
        protected bool Has_ReferenceName { get; set; }
        protected string[] ReferenceName { get; set; }
        // Force
        protected bool Has_Force { get; set; }
        protected bool Force { get; set; }
        // Collection
        protected bool Has_Collection => Parameters.HasParameter("Collection");
        protected Models.Connection Collection => Data.GetCollection();
        // Server
        protected bool Has_Server => Parameters.HasParameter("Server");
        protected Models.Connection Server => Data.GetServer();
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        // Items
        protected IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemField> Items => Field switch {
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemField item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemField> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemField>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemField);
        protected override void CacheParameters()
        {
            // Field
            Has_Field = Parameters.HasParameter("Field");
            Field = Parameters.Get<object>("Field", "*");
            // ReferenceName
            Has_ReferenceName = Parameters.HasParameter("ReferenceName");
            ReferenceName = Parameters.Get<string[]>("ReferenceName");
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveProcessFieldDefinitionController(TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}