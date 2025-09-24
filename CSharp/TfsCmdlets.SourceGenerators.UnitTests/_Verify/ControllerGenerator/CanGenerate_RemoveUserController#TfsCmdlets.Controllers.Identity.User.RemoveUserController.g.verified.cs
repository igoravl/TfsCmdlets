//HintName: TfsCmdlets.Controllers.Identity.User.RemoveUserController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Licensing;
using Microsoft.VisualStudio.Services.Licensing.Client;
using IAccountLicensingHttpClient = TfsCmdlets.HttpClients.IAccountLicensingHttpClient;
namespace TfsCmdlets.Controllers.Identity.User
{
    internal partial class RemoveUserController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IAccountLicensingHttpClient Client { get; }
        // User
        protected bool Has_User { get; set; }
        protected object User { get; set; }
        // Collection
        protected bool Has_Collection => Parameters.HasParameter("Collection");
        protected Models.Connection Collection => Data.GetCollection();
        // Server
        protected bool Has_Server => Parameters.HasParameter("Server");
        protected Models.Connection Server => Data.GetServer();
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        // Items
        protected IEnumerable<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> Items => User switch {
            Microsoft.VisualStudio.Services.Licensing.AccountEntitlement item => new[] { item },
            IEnumerable<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> items => items,
            _ => Data.GetItems<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.Licensing.AccountEntitlement);
        protected override void CacheParameters()
        {
            // User
            Has_User = Parameters.HasParameter("User");
            User = Parameters.Get<object>("User");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveUserController(TfsCmdlets.HttpClients.IAccountLicensingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}