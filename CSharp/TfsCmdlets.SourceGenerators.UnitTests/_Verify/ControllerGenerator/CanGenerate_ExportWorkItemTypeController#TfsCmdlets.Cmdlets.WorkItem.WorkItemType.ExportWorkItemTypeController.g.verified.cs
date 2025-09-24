//HintName: TfsCmdlets.Cmdlets.WorkItem.WorkItemType.ExportWorkItemTypeController.g.cs
using System.Management.Automation;
namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    internal partial class ExportWorkItemTypeController: ControllerBase
    {
        // Type
        protected bool Has_Type { get; set; }
        protected string Type { get; set; }
        // IncludeGlobalLists
        protected bool Has_IncludeGlobalLists { get; set; }
        protected bool IncludeGlobalLists { get; set; }
        // Destination
        protected bool Has_Destination { get; set; }
        protected string Destination { get; set; }
        // Encoding
        protected bool Has_Encoding { get; set; }
        protected string Encoding { get; set; }
        // Force
        protected bool Has_Force { get; set; }
        protected bool Force { get; set; }
        // AsXml
        protected bool Has_AsXml { get; set; }
        protected bool AsXml { get; set; }
        // Project
        protected bool Has_Project => Parameters.HasParameter("Project");
        protected WebApiTeamProject Project => Data.GetProject();
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
            // Type
            Has_Type = Parameters.HasParameter("Type");
            Type = Parameters.Get<string>("Type", "*");
            // IncludeGlobalLists
            Has_IncludeGlobalLists = Parameters.HasParameter("IncludeGlobalLists");
            IncludeGlobalLists = Parameters.Get<bool>("IncludeGlobalLists");
            // Destination
            Has_Destination = Parameters.HasParameter("Destination");
            Destination = Parameters.Get<string>("Destination");
            // Encoding
            Has_Encoding = Parameters.HasParameter("Encoding");
            Encoding = Parameters.Get<string>("Encoding", "UTF-8");
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // AsXml
            Has_AsXml = Parameters.HasParameter("AsXml");
            AsXml = Parameters.Get<bool>("AsXml");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public ExportWorkItemTypeController(IWorkItemStore store, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Store = store;
        }
    }
}