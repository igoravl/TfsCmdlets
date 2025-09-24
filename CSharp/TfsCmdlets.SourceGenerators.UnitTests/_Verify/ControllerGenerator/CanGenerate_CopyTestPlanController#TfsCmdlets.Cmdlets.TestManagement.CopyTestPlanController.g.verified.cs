//HintName: TfsCmdlets.Cmdlets.TestManagement.CopyTestPlanController.g.cs
using System.Management.Automation;
using System.Threading;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using TfsCmdlets.Cmdlets.TestManagement;
namespace TfsCmdlets.Cmdlets.TestManagement
{
    internal partial class CopyTestPlanController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITestPlanHttpClient Client { get; }
        // TestPlan
        protected bool Has_TestPlan { get; set; }
        protected object TestPlan { get; set; }
        // NewName
        protected bool Has_NewName { get; set; }
        protected string NewName { get; set; }
        // Destination
        protected bool Has_Destination { get; set; }
        protected object Destination { get; set; }
        // AreaPath
        protected bool Has_AreaPath { get; set; }
        protected string AreaPath { get; set; }
        // IterationPath
        protected bool Has_IterationPath { get; set; }
        protected string IterationPath { get; set; }
        // DeepClone
        protected bool Has_DeepClone { get; set; }
        protected bool DeepClone { get; set; }
        // Recurse
        protected bool Has_Recurse { get; set; }
        protected bool Recurse { get; set; }
        // CopyAncestorHierarchy
        protected bool Has_CopyAncestorHierarchy { get; set; }
        protected bool CopyAncestorHierarchy { get; set; }
        // CloneRequirements
        protected bool Has_CloneRequirements { get; set; }
        protected bool CloneRequirements { get; set; }
        // DestinationWorkItemType
        protected bool Has_DestinationWorkItemType { get; set; }
        protected string DestinationWorkItemType { get; set; }
        // SuiteIds
        protected bool Has_SuiteIds { get; set; }
        protected int[] SuiteIds { get; set; }
        // RelatedLinkComment
        protected bool Has_RelatedLinkComment { get; set; }
        protected string RelatedLinkComment { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected string Passthru { get; set; }
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
        protected IEnumerable<Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan> Items => TestPlan switch {
            Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan item => new[] { item },
            IEnumerable<Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan> items => items,
            _ => Data.GetItems<Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan);
        protected override void CacheParameters()
        {
            // TestPlan
            Has_TestPlan = Parameters.HasParameter("TestPlan");
            TestPlan = Parameters.Get<object>("TestPlan");
            // NewName
            Has_NewName = Parameters.HasParameter("NewName");
            NewName = Parameters.Get<string>("NewName");
            // Destination
            Has_Destination = Parameters.HasParameter("Destination");
            Destination = Parameters.Get<object>("Destination");
            // AreaPath
            Has_AreaPath = Parameters.HasParameter("AreaPath");
            AreaPath = Parameters.Get<string>("AreaPath");
            // IterationPath
            Has_IterationPath = Parameters.HasParameter("IterationPath");
            IterationPath = Parameters.Get<string>("IterationPath");
            // DeepClone
            Has_DeepClone = Parameters.HasParameter("DeepClone");
            DeepClone = Parameters.Get<bool>("DeepClone");
            // Recurse
            Has_Recurse = Parameters.HasParameter("Recurse");
            Recurse = Parameters.Get<bool>("Recurse");
            // CopyAncestorHierarchy
            Has_CopyAncestorHierarchy = Parameters.HasParameter("CopyAncestorHierarchy");
            CopyAncestorHierarchy = Parameters.Get<bool>("CopyAncestorHierarchy");
            // CloneRequirements
            Has_CloneRequirements = Parameters.HasParameter("CloneRequirements");
            CloneRequirements = Parameters.Get<bool>("CloneRequirements");
            // DestinationWorkItemType
            Has_DestinationWorkItemType = Parameters.HasParameter("DestinationWorkItemType");
            DestinationWorkItemType = Parameters.Get<string>("DestinationWorkItemType", "Test Case");
            // SuiteIds
            Has_SuiteIds = Parameters.HasParameter("SuiteIds");
            SuiteIds = Parameters.Get<int[]>("SuiteIds");
            // RelatedLinkComment
            Has_RelatedLinkComment = Parameters.HasParameter("RelatedLinkComment");
            RelatedLinkComment = Parameters.Get<string>("RelatedLinkComment");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<string>("Passthru", "None");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public CopyTestPlanController(TfsCmdlets.HttpClients.ITestPlanHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}