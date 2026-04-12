//HintName: TfsCmdlets.Cmdlets.ProcessTemplate.GetProcessTemplateController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    internal partial class GetProcessTemplateController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IProcessHttpClient Client { get; }
        // ProcessTemplate
        protected bool Has_ProcessTemplate => Parameters.HasParameter(nameof(ProcessTemplate));
        protected IEnumerable ProcessTemplate
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(ProcessTemplate), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Default
        protected bool Has_Default { get; set; }
        protected bool Default { get; set; }
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
        public override Type DataType => typeof(Microsoft.TeamFoundation.Core.WebApi.Process);
        protected override void CacheParameters()
        {
            // Default
            Has_Default = Parameters.HasParameter("Default");
            Default = Parameters.Get<bool>("Default");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetProcessTemplateController(TfsCmdlets.HttpClients.IProcessHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}