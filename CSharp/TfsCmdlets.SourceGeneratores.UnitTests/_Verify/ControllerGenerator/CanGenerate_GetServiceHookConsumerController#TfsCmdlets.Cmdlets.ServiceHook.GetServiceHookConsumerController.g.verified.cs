//HintName: TfsCmdlets.Cmdlets.ServiceHook.GetServiceHookConsumerController.g.cs
using System.Management.Automation;
using WebApiConsumer = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
namespace TfsCmdlets.Cmdlets.ServiceHook
{
    internal partial class GetServiceHookConsumerController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IServiceHooksPublisherHttpClient Client { get; }
        // Consumer
        protected bool Has_Consumer { get; set; }
        protected string Consumer { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer);
        protected override void CacheParameters()
        {
            // Consumer
            Has_Consumer = Parameters.HasParameter("Consumer");
            Consumer = Parameters.Get<string>("Consumer", "*");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetServiceHookConsumerController(TfsCmdlets.HttpClients.IServiceHooksPublisherHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}