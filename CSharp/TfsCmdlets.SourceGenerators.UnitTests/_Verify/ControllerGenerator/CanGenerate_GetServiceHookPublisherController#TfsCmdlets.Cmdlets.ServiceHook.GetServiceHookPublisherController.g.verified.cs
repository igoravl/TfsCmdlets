//HintName: TfsCmdlets.Cmdlets.ServiceHook.GetServiceHookPublisherController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using WebApiPublisher = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher;
namespace TfsCmdlets.Cmdlets.ServiceHook
{
    internal partial class GetServiceHookPublisherController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IServiceHooksPublisherHttpClient Client { get; }
        // Publisher
        protected bool Has_Publisher => Parameters.HasParameter(nameof(Publisher));
        protected IEnumerable Publisher
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Publisher), "*");
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher);
        protected override void CacheParameters()
        {
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetServiceHookPublisherController(TfsCmdlets.HttpClients.IServiceHooksPublisherHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}