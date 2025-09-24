//HintName: TfsCmdlets.Cmdlets.ServiceHook.GetServiceHookNotificationHistoryController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using WebApiSubscription = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription;
using WebApiServiceHookNotification = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification;
namespace TfsCmdlets.Cmdlets.ServiceHook
{
    internal partial class GetServiceHookNotificationHistoryController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IServiceHooksPublisherHttpClient Client { get; }
        // Subscription
        protected bool Has_Subscription => Parameters.HasParameter(nameof(Subscription));
        protected IEnumerable Subscription
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Subscription));
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // From
        protected bool Has_From { get; set; }
        protected System.DateTime? From { get; set; }
        // To
        protected bool Has_To { get; set; }
        protected System.DateTime? To { get; set; }
        // Status
        protected bool Has_Status { get; set; }
        protected Microsoft.VisualStudio.Services.ServiceHooks.WebApi.NotificationStatus Status { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification);
        protected override void CacheParameters()
        {
            // From
            Has_From = Parameters.HasParameter("From");
            From = Parameters.Get<System.DateTime?>("From");
            // To
            Has_To = Parameters.HasParameter("To");
            To = Parameters.Get<System.DateTime?>("To");
            // Status
            Has_Status = Parameters.HasParameter("Status");
            Status = Parameters.Get<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.NotificationStatus>("Status");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetServiceHookNotificationHistoryController(TfsCmdlets.HttpClients.IServiceHooksPublisherHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}