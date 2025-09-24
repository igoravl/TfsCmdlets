//HintName: TfsCmdlets.Cmdlets.WorkItem.AreasIterations.RemoveIterationController.g.cs
using System.Management.Automation;
using TfsCmdlets.Models;
namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    internal partial class RemoveIterationController: RemoveClassificationNodeController
    {
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
        public RemoveIterationController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger, IWorkItemTrackingHttpClient client)
            : base(powerShell, data, parameters, logger, client)
        {
        }
    }
}