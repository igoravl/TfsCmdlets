//HintName: TfsCmdlets.Cmdlets.WorkItem.Query.ExportWorkItemQueryController.g.cs
using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    internal partial class ExportWorkItemQueryController: ControllerBase
    {
        // Query
        protected bool Has_Query { get; set; }
        protected object Query { get; set; }
        // Scope
        protected bool Has_Scope { get; set; }
        protected string Scope { get; set; }
        // Destination
        protected bool Has_Destination { get; set; }
        protected string Destination { get; set; }
        // Encoding
        protected bool Has_Encoding { get; set; }
        protected string Encoding { get; set; }
        // FlattenFolders
        protected bool Has_FlattenFolders { get; set; }
        protected bool FlattenFolders { get; set; }
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
        // Items
        protected IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem> Items => Query switch {
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem>()
        };
        protected override void CacheParameters()
        {
            // Query
            Has_Query = Parameters.HasParameter("Query");
            Query = Parameters.Get<object>("Query");
            // Scope
            Has_Scope = Parameters.HasParameter("Scope");
            Scope = Parameters.Get<string>("Scope", "Both");
            // Destination
            Has_Destination = Parameters.HasParameter("Destination");
            Destination = Parameters.Get<string>("Destination");
            // Encoding
            Has_Encoding = Parameters.HasParameter("Encoding");
            Encoding = Parameters.Get<string>("Encoding", "UTF-8");
            // FlattenFolders
            Has_FlattenFolders = Parameters.HasParameter("FlattenFolders");
            FlattenFolders = Parameters.Get<bool>("FlattenFolders");
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
        public ExportWorkItemQueryController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}