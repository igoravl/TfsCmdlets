//HintName: TfsCmdlets.Cmdlets.WorkItem.AreasIterations.CopyIterationController.g.cs
using System.Management.Automation;
namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    internal partial class CopyIterationController: CopyClassificationNodeController
    {
        // Node
        protected bool Has_Node { get; set; }
        protected object Node { get; set; }
        // Destination
        protected bool Has_Destination { get; set; }
        protected object Destination { get; set; }
        // DestinationProject
        protected bool Has_DestinationProject { get; set; }
        protected object DestinationProject { get; set; }
        // Force
        protected bool Has_Force { get; set; }
        protected bool Force { get; set; }
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
            // Destination
            Has_Destination = Parameters.HasParameter("Destination");
            Destination = Parameters.Get<object>("Destination");
            // DestinationProject
            Has_DestinationProject = Parameters.HasParameter("DestinationProject");
            DestinationProject = Parameters.Get<object>("DestinationProject");
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
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
        public CopyIterationController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}
