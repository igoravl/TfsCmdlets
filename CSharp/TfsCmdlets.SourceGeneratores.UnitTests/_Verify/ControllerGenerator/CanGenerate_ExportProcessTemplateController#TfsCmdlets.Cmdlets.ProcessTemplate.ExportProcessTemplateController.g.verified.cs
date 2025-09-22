//HintName: TfsCmdlets.Cmdlets.ProcessTemplate.ExportProcessTemplateController.g.cs
using System.Management.Automation;
namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    internal partial class ExportProcessTemplateController: ControllerBase
    {
        // ProcessTemplate
        protected bool Has_ProcessTemplate { get; set; }
        protected object ProcessTemplate { get; set; }
        // DestinationPath
        protected bool Has_DestinationPath { get; set; }
        protected string DestinationPath { get; set; }
        // NewName
        protected bool Has_NewName { get; set; }
        protected string NewName { get; set; }
        // NewDescription
        protected bool Has_NewDescription { get; set; }
        protected string NewDescription { get; set; }
        // Force
        protected bool Has_Force { get; set; }
        protected bool Force { get; set; }
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
        protected IEnumerable Items => Data.Invoke("Get", "ProcessTemplate");
        protected override void CacheParameters()
        {
            // ProcessTemplate
            Has_ProcessTemplate = Parameters.HasParameter("ProcessTemplate");
            ProcessTemplate = Parameters.Get<object>("ProcessTemplate", "*");
            // DestinationPath
            Has_DestinationPath = Parameters.HasParameter("DestinationPath");
            DestinationPath = Parameters.Get<string>("DestinationPath");
            // NewName
            Has_NewName = Parameters.HasParameter("NewName");
            NewName = Parameters.Get<string>("NewName");
            // NewDescription
            Has_NewDescription = Parameters.HasParameter("NewDescription");
            NewDescription = Parameters.Get<string>("NewDescription");
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public ExportProcessTemplateController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}