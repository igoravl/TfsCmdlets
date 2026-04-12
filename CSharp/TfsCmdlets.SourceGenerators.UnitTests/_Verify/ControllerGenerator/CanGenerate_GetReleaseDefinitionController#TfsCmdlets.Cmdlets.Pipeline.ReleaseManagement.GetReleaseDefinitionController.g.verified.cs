//HintName: TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement.GetReleaseDefinitionController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    internal partial class GetReleaseDefinitionController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IReleaseHttpClient2 Client { get; }
        // Definition
        protected bool Has_Definition => Parameters.HasParameter(nameof(Definition));
        protected IEnumerable Definition
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Definition), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Project
        protected bool Has_Project => Parameters.HasParameter("Project");
        protected WebApiTeamProject Project => Data.GetProject();
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.ReleaseDefinition);
        protected override void CacheParameters()
        {
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetReleaseDefinitionController(TfsCmdlets.HttpClients.IReleaseHttpClient2 client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}