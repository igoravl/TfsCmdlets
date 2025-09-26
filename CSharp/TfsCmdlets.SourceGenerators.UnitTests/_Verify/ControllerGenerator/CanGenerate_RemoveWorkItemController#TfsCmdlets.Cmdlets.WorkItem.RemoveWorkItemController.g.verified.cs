//HintName: TfsCmdlets.Cmdlets.WorkItem.RemoveWorkItemController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
namespace TfsCmdlets.Cmdlets.WorkItem
{
    internal partial class RemoveWorkItemController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient Client { get; }
        // WorkItem
        protected bool Has_WorkItem { get; set; }
        protected object WorkItem { get; set; }
        // Destroy
        protected bool Has_Destroy { get; set; }
        protected bool Destroy { get; set; }
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
        protected IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem> Items => WorkItem switch {
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem);
        protected override void CacheParameters()
        {
            // WorkItem
            Has_WorkItem = Parameters.HasParameter("WorkItem");
            WorkItem = Parameters.Get<object>("WorkItem");
            // Destroy
            Has_Destroy = Parameters.HasParameter("Destroy");
            Destroy = Parameters.Get<bool>("Destroy");
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveWorkItemController(TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}