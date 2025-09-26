//HintName: TfsCmdlets.Cmdlets.ProcessTemplate.ImportProcessTemplateController.g.cs
using System.Management.Automation;
namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    internal partial class ImportProcessTemplateController: ControllerBase
    {
        // Path
        protected bool Has_Path { get; set; }
        protected string Path { get; set; }
        // State
        protected bool Has_State { get; set; }
        protected string State { get; set; }
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
            // State
            Has_State = Parameters.HasParameter("State");
            State = Parameters.Get<string>("State", "Visible");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public ImportProcessTemplateController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}