//HintName: TfsCmdlets.Cmdlets.WorkItem.AreasIterations.NewIterationController.g.cs
using System.Management.Automation;
using TfsCmdlets.Models;
namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    internal partial class NewIterationController: NewClassificationNodeController
    {
        // Node
        protected bool Has_Node { get; set; }
        protected string Node { get; set; }
        // StartDate
        protected bool Has_StartDate { get; set; }
        protected System.DateTime? StartDate { get; set; }
        // FinishDate
        protected bool Has_FinishDate { get; set; }
        protected System.DateTime? FinishDate { get; set; }
        // Force
        protected bool Has_Force { get; set; }
        protected bool Force { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
        // StructureGroup
        protected bool Has_StructureGroup { get; set; }
        protected Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup StructureGroup { get; set; }
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
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.ClassificationNode);
        protected override void CacheParameters()
        {
            // Node
            Has_Node = Parameters.HasParameter("Node");
            Node = Parameters.Get<string>("Node");
            // StartDate
            Has_StartDate = Parameters.HasParameter("StartDate");
            StartDate = Parameters.Get<System.DateTime?>("StartDate");
            // FinishDate
            Has_FinishDate = Parameters.HasParameter("FinishDate");
            FinishDate = Parameters.Get<System.DateTime?>("FinishDate");
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // StructureGroup
            Has_StructureGroup = Parameters.HasParameter("StructureGroup");
            StructureGroup = Parameters.Get<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup>("StructureGroup");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public NewIterationController(INodeUtil nodeUtil, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger, IWorkItemTrackingHttpClient client)
            : base(nodeUtil, powerShell, data, parameters, logger, client)
        {
        }
    }
}
