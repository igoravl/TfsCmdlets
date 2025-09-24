//HintName: TfsCmdlets.Cmdlets.Admin.Registry.GetRegistryValueController.g.cs
using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.VisualStudio.Services.WebApi;
namespace TfsCmdlets.Cmdlets.Admin.Registry
{
    internal partial class GetRegistryValueController: ControllerBase
    {
        // Path
        protected bool Has_Path => Parameters.HasParameter(nameof(Path));
        protected IEnumerable Path
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Path));
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Scope
        protected bool Has_Scope { get; set; }
        protected TfsCmdlets.RegistryScope Scope { get; set; }
        // Collection
        protected bool Has_Collection => Parameters.HasParameter("Collection");
        protected Models.Connection Collection => Data.GetCollection();
        // Server
        protected bool Has_Server => Parameters.HasParameter("Server");
        protected Models.Connection Server => Data.GetServer();
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        protected override void CacheParameters()
        {
            // Scope
            Has_Scope = Parameters.HasParameter("Scope");
            Scope = Parameters.Get<TfsCmdlets.RegistryScope>("Scope", RegistryScope.Server);
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetRegistryValueController(IRestApiService restApi, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            RestApi = restApi;
        }
    }
}