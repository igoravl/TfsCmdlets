//HintName: TfsCmdlets.Cmdlets.ServiceHook.GetServiceHookSubscriptionController.g.cs
using System.Management.Automation;
using WebApiConsumer = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer;
using WebApiPublisher = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher;
using WebApiSubscription = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
namespace TfsCmdlets.Cmdlets.ServiceHook
{
    internal partial class GetServiceHookSubscriptionController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IServiceHooksPublisherHttpClient Client { get; }
        // Subscription
        protected bool Has_Subscription => Parameters.HasParameter(nameof(Subscription));
        protected IEnumerable Subscription
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Subscription), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Publisher
        protected bool Has_Publisher { get; set; }
        protected object Publisher { get; set; }
        // Consumer
        protected bool Has_Consumer { get; set; }
        protected object Consumer { get; set; }
        // EventType
        protected bool Has_EventType { get; set; }
        protected string EventType { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription);
        protected override void CacheParameters()
        {
            // Publisher
            Has_Publisher = Parameters.HasParameter("Publisher");
            Publisher = Parameters.Get<object>("Publisher");
            // Consumer
            Has_Consumer = Parameters.HasParameter("Consumer");
            Consumer = Parameters.Get<object>("Consumer");
            // EventType
            Has_EventType = Parameters.HasParameter("EventType");
            EventType = Parameters.Get<string>("EventType");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetServiceHookSubscriptionController(TfsCmdlets.HttpClients.IServiceHooksPublisherHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}