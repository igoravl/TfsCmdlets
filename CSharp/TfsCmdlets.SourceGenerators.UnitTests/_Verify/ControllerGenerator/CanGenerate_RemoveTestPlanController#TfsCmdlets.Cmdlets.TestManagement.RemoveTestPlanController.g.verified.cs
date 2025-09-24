//HintName: TfsCmdlets.Cmdlets.TestManagement.RemoveTestPlanController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
namespace TfsCmdlets.Cmdlets.TestManagement
{
    internal partial class RemoveTestPlanController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITestPlanHttpClient Client { get; }
        // TestPlan
        protected bool Has_TestPlan { get; set; }
        protected object TestPlan { get; set; }
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
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveTestPlanController(TfsCmdlets.HttpClients.ITestPlanHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}