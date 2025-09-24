//HintName: TfsCmdlets.Cmdlets.Admin.Registry.SetRegistryValueController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
namespace TfsCmdlets.Cmdlets.Admin.Registry
{
    internal partial class SetRegistryValueController: ControllerBase
    {
        // Path
        protected bool Has_Path { get; set; }
        protected string Path { get; set; }
        // Value
        protected bool Has_Value { get; set; }
        protected string Value { get; set; }
        // Scope
        protected bool Has_Scope { get; set; }
        protected TfsCmdlets.RegistryScope Scope { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
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
            // Path
            Has_Path = Parameters.HasParameter("Path");
            Path = Parameters.Get<string>("Path");
            // Value
            Has_Value = Parameters.HasParameter("Value");
            Value = Parameters.Get<string>("Value");
            // Scope
            Has_Scope = Parameters.HasParameter("Scope");
            Scope = Parameters.Get<TfsCmdlets.RegistryScope>("Scope", RegistryScope.Server);
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public SetRegistryValueController(IRestApiService restApi, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            RestApi = restApi;
        }
    }
}
