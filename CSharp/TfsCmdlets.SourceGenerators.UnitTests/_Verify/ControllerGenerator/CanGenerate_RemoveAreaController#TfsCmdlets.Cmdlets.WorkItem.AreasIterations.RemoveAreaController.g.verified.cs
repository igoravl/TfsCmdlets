//HintName: TfsCmdlets.Cmdlets.WorkItem.AreasIterations.RemoveAreaController.g.cs
using System.Management.Automation;
using TfsCmdlets.Models;
namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    internal partial class RemoveAreaController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient Client { get; }
        // Node
        protected bool Has_Node { get; set; }
        protected object Node { get; set; }
        // MoveTo
        protected bool Has_MoveTo { get; set; }
        protected object MoveTo { get; set; }
        // Recurse
        protected bool Has_Recurse { get; set; }
        protected bool Recurse { get; set; }
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
        protected IEnumerable<ClassificationNode> Items => Node switch {
            ClassificationNode item => new[] { item },
            IEnumerable<ClassificationNode> items => items,
            _ => Data.GetItems<ClassificationNode>()
        };
        // DataType
        public override Type DataType => typeof(ClassificationNode);
        protected override void CacheParameters()
        {
            // Node
            Has_Node = Parameters.HasParameter("Node");
            Node = Parameters.Get<object>("Node");
            // MoveTo
            Has_MoveTo = Parameters.HasParameter("MoveTo");
            MoveTo = Parameters.Get<object>("MoveTo", "\\");
            // Recurse
            Has_Recurse = Parameters.HasParameter("Recurse");
            Recurse = Parameters.Get<bool>("Recurse");
            // StructureGroup
            Has_StructureGroup = Parameters.HasParameter("StructureGroup");
            StructureGroup = Parameters.Get<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup>("StructureGroup");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveAreaController(TfsCmdlets.HttpClients.IWorkItemTrackingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}