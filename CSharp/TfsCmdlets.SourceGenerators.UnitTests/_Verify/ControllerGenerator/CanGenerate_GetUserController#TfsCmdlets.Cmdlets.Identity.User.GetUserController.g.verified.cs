//HintName: TfsCmdlets.Cmdlets.Identity.User.GetUserController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Licensing;
using TfsCmdlets.Cmdlets.Identity;
using Microsoft.VisualStudio.Services.Licensing.Client;
namespace TfsCmdlets.Cmdlets.Identity.User
{
    internal partial class GetUserController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IAccountLicensingHttpClient Client { get; }
        // User
        protected bool Has_User => Parameters.HasParameter(nameof(User));
        protected IEnumerable User
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(User), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Current
        protected bool Has_Current { get; set; }
        protected bool Current { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.Licensing.AccountEntitlement);
        protected override void CacheParameters()
        {
            // Current
            Has_Current = Parameters.HasParameter("Current");
            Current = Parameters.Get<bool>("Current");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetUserController(TfsCmdlets.HttpClients.IAccountLicensingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}