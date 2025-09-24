//HintName: TfsCmdlets.Cmdlets.WorkItem.AreasIterations.GetAreaController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;
namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    internal partial class GetAreaController: GetClassificationNodeController
    {
        // Node
        protected bool Has_Node => Parameters.HasParameter(nameof(Node));
        protected IEnumerable Node
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Node), @"\**");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
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
            // StructureGroup
            Has_StructureGroup = Parameters.HasParameter("StructureGroup");
            StructureGroup = Parameters.Get<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup>("StructureGroup");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetAreaController(INodeUtil nodeUtil, IWorkItemTrackingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(nodeUtil, client, powerShell, data, parameters, logger)
        {
        }
    }
}