//HintName: TfsCmdlets.Cmdlets.WorkItem.History.GetWorkItemHistoryController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
namespace TfsCmdlets.Cmdlets.WorkItem.History
{
    internal partial class GetWorkItemHistoryController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient Client { get; }
        // WorkItem
        protected bool Has_WorkItem => Parameters.HasParameter(nameof(WorkItem));
        protected IEnumerable WorkItem
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(WorkItem));
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
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
        public override Type DataType => typeof(TfsCmdlets.Models.WorkItemHistoryEntry);
        protected override void CacheParameters()
        {
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetWorkItemHistoryController(TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}