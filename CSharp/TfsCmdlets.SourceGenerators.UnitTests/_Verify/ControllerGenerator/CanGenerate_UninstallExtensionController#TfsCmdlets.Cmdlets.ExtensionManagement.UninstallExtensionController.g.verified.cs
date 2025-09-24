//HintName: TfsCmdlets.Cmdlets.ExtensionManagement.UninstallExtensionController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;
namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    internal partial class UninstallExtensionController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IExtensionManagementHttpClient Client { get; }
        // Extension
        protected bool Has_Extension { get; set; }
        protected object Extension { get; set; }
        // Publisher
        protected bool Has_Publisher { get; set; }
        protected string Publisher { get; set; }
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
        protected IEnumerable<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension> Items => Extension switch {
            Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension item => new[] { item },
            IEnumerable<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension> items => items,
            _ => Data.GetItems<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension);
        protected override void CacheParameters()
        {
            // Extension
            Has_Extension = Parameters.HasParameter("Extension");
            Extension = Parameters.Get<object>("Extension");
            // Publisher
            Has_Publisher = Parameters.HasParameter("Publisher");
            Publisher = Parameters.Get<string>("Publisher");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public UninstallExtensionController(TfsCmdlets.HttpClients.IExtensionManagementHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}