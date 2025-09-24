//HintName: TfsCmdlets.Cmdlets.ExtensionManagement.InstallExtensionController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;
namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    internal partial class InstallExtensionController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IExtensionManagementHttpClient Client { get; }
        // Extension
        protected bool Has_Extension { get; set; }
        protected string Extension { get; set; }
        // Publisher
        protected bool Has_Publisher { get; set; }
        protected string Publisher { get; set; }
        // Version
        protected bool Has_Version { get; set; }
        protected string Version { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension);
        protected override void CacheParameters()
        {
            // Extension
            Has_Extension = Parameters.HasParameter("Extension");
            Extension = Parameters.Get<string>("Extension");
            // Publisher
            Has_Publisher = Parameters.HasParameter("Publisher");
            Publisher = Parameters.Get<string>("Publisher");
            // Version
            Has_Version = Parameters.HasParameter("Version");
            Version = Parameters.Get<string>("Version");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public InstallExtensionController(TfsCmdlets.HttpClients.IExtensionManagementHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}