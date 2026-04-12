//HintName: TfsCmdlets.Cmdlets.RestApi.GetRestClientController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using System.Reflection;
namespace TfsCmdlets.Cmdlets.RestApi
{
    internal partial class GetRestClientController: ControllerBase
    {
        // TypeName
        protected bool Has_TypeName { get; set; }
        protected string TypeName { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase);
        protected override void CacheParameters()
        {
            // TypeName
            Has_TypeName = Parameters.HasParameter("TypeName");
            TypeName = Parameters.Get<string>("TypeName");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetRestClientController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}