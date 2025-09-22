//HintName: TfsCmdlets.Cmdlets.TestManagement.NewTestPlanController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
namespace TfsCmdlets.Cmdlets.TestManagement
{
    internal partial class NewTestPlanController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITestPlanHttpClient Client { get; }
        // TestPlan
        protected bool Has_TestPlan { get; set; }
        protected string TestPlan { get; set; }
        // AreaPath
        protected bool Has_AreaPath { get; set; }
        protected string AreaPath { get; set; }
        // IterationPath
        protected bool Has_IterationPath { get; set; }
        protected string IterationPath { get; set; }
        // StartDate
        protected bool Has_StartDate { get; set; }
        protected System.DateTime StartDate { get; set; }
        // EndDate
        protected bool Has_EndDate { get; set; }
        protected System.DateTime EndDate { get; set; }
        // Owner
        protected bool Has_Owner { get; set; }
        protected object Owner { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan);
        protected override void CacheParameters()
        {
            // TestPlan
            Has_TestPlan = Parameters.HasParameter("TestPlan");
            TestPlan = Parameters.Get<string>("TestPlan");
            // AreaPath
            Has_AreaPath = Parameters.HasParameter("AreaPath");
            AreaPath = Parameters.Get<string>("AreaPath");
            // IterationPath
            Has_IterationPath = Parameters.HasParameter("IterationPath");
            IterationPath = Parameters.Get<string>("IterationPath");
            // StartDate
            Has_StartDate = Parameters.HasParameter("StartDate");
            StartDate = Parameters.Get<System.DateTime>("StartDate");
            // EndDate
            Has_EndDate = Parameters.HasParameter("EndDate");
            EndDate = Parameters.Get<System.DateTime>("EndDate");
            // Owner
            Has_Owner = Parameters.HasParameter("Owner");
            Owner = Parameters.Get<object>("Owner");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public NewTestPlanController(INodeUtil nodeUtil, TfsCmdlets.HttpClients.ITestPlanHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            NodeUtil = nodeUtil;
            Client = client;
        }
    }
}