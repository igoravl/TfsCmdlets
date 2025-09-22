//HintName: TfsCmdlets.Cmdlets.Admin.GetVersionController.g.cs
using System.Text.RegularExpressions;
using TfsCmdlets.Models;
namespace TfsCmdlets.Cmdlets.Admin
{
    internal partial class GetVersionController: ControllerBase
    {
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
        public override Type DataType => typeof(TfsCmdlets.Models.ServerVersion);
        protected override void CacheParameters()
        {
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetVersionController(ITfsVersionTable tfsVersionTable, IRestApiService restApi, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            TfsVersionTable = tfsVersionTable;
            RestApi = restApi;
        }
    }
}