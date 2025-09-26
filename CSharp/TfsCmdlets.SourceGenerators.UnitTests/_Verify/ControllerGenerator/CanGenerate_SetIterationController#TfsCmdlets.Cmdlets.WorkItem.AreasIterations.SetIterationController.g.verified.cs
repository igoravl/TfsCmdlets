//HintName: TfsCmdlets.Cmdlets.WorkItem.AreasIterations.SetIterationController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;
using TfsCmdlets.Util;
namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    internal partial class SetIterationController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient Client { get; }
        // Node
        protected bool Has_Node { get; set; }
        protected object Node { get; set; }
        // StartDate
        protected bool Has_StartDate { get; set; }
        protected System.DateTime? StartDate { get; set; }
        // FinishDate
        protected bool Has_FinishDate { get; set; }
        protected System.DateTime? FinishDate { get; set; }
        // Length
        protected bool Has_Length { get; set; }
        protected int Length { get; set; }
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
        // Items
        protected IEnumerable<TfsCmdlets.Models.ClassificationNode> Items => Node switch {
            TfsCmdlets.Models.ClassificationNode item => new[] { item },
            IEnumerable<TfsCmdlets.Models.ClassificationNode> items => items,
            _ => Data.GetItems<TfsCmdlets.Models.ClassificationNode>()
        };
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.ClassificationNode);
        protected override void CacheParameters()
        {
            // Node
            Has_Node = Parameters.HasParameter("Node");
            Node = Parameters.Get<object>("Node");
            // StartDate
            Has_StartDate = Parameters.HasParameter("StartDate");
            StartDate = Parameters.Get<System.DateTime?>("StartDate");
            // FinishDate
            Has_FinishDate = Parameters.HasParameter("FinishDate");
            FinishDate = Parameters.Get<System.DateTime?>("FinishDate");
            // Length
            Has_Length = Parameters.HasParameter("Length");
            Length = Parameters.Get<int>("Length", 0);
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
        public SetIterationController(TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}