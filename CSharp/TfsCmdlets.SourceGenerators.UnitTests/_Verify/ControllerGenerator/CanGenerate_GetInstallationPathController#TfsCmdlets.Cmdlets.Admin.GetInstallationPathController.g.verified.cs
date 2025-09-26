//HintName: TfsCmdlets.Cmdlets.Admin.GetInstallationPathController.g.cs
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using TfsCmdlets.Models;
namespace TfsCmdlets.Cmdlets.Admin
{
    internal partial class GetInstallationPathController: ControllerBase
    {
        // ComputerName
        protected bool Has_ComputerName { get; set; }
        protected string ComputerName { get; set; }
        // Session
        protected bool Has_Session { get; set; }
        protected System.Management.Automation.Runspaces.PSSession Session { get; set; }
        // Component
        protected bool Has_Component { get; set; }
        protected TfsCmdlets.TfsComponent Component { get; set; }
        // Version
        protected bool Has_Version { get; set; }
        protected int Version { get; set; }
        // Credential
        protected bool Has_Credential { get; set; }
        protected System.Management.Automation.PSCredential Credential { get; set; }
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.TfsInstallationPath);
        protected override void CacheParameters()
        {
            // ComputerName
            Has_ComputerName = Parameters.HasParameter("ComputerName");
            ComputerName = Parameters.Get<string>("ComputerName", "localhost");
            // Session
            Has_Session = Parameters.HasParameter("Session");
            Session = Parameters.Get<System.Management.Automation.Runspaces.PSSession>("Session");
            // Component
            Has_Component = Parameters.HasParameter("Component");
            Component = Parameters.Get<TfsCmdlets.TfsComponent>("Component", TfsComponent.BaseInstallation);
            // Version
            Has_Version = Parameters.HasParameter("Version");
            Version = Parameters.Get<int>("Version");
            // Credential
            Has_Credential = Parameters.HasParameter("Credential");
            Credential = Parameters.Get<System.Management.Automation.PSCredential>("Credential", PSCredential.Empty);
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetInstallationPathController(IRegistryService registry, ITfsVersionTable tfsVersionTable, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Registry = registry;
            TfsVersionTable = tfsVersionTable;
        }
    }
}