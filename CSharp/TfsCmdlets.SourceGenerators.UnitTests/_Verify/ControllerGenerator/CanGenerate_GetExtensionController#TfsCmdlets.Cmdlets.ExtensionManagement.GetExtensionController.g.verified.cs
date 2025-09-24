//HintName: TfsCmdlets.Cmdlets.ExtensionManagement.GetExtensionController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;
namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    internal partial class GetExtensionController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IExtensionManagementHttpClient Client { get; }
        // Extension
        protected bool Has_Extension => Parameters.HasParameter(nameof(Extension));
        protected IEnumerable Extension
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Extension), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Publisher
        protected bool Has_Publisher { get; set; }
        protected string Publisher { get; set; }
        // IncludeDisabledExtensions
        protected bool Has_IncludeDisabledExtensions { get; set; }
        protected bool IncludeDisabledExtensions { get; set; }
        // IncludeErrors
        protected bool Has_IncludeErrors { get; set; }
        protected bool IncludeErrors { get; set; }
        // IncludeInstallationIssues
        protected bool Has_IncludeInstallationIssues { get; set; }
        protected bool IncludeInstallationIssues { get; set; }
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
            // Publisher
            Has_Publisher = Parameters.HasParameter("Publisher");
            Publisher = Parameters.Get<string>("Publisher", "*");
            // IncludeDisabledExtensions
            Has_IncludeDisabledExtensions = Parameters.HasParameter("IncludeDisabledExtensions");
            IncludeDisabledExtensions = Parameters.Get<bool>("IncludeDisabledExtensions");
            // IncludeErrors
            Has_IncludeErrors = Parameters.HasParameter("IncludeErrors");
            IncludeErrors = Parameters.Get<bool>("IncludeErrors");
            // IncludeInstallationIssues
            Has_IncludeInstallationIssues = Parameters.HasParameter("IncludeInstallationIssues");
            IncludeInstallationIssues = Parameters.Get<bool>("IncludeInstallationIssues");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetExtensionController(TfsCmdlets.HttpClients.IExtensionManagementHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}