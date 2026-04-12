//HintName: TfsCmdlets.Cmdlets.TestManagement.GetTestPlanController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
namespace TfsCmdlets.Cmdlets.TestManagement
{
    internal partial class GetTestPlanController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITestPlanHttpClient Client { get; }
        // TestPlan
        protected bool Has_TestPlan => Parameters.HasParameter(nameof(TestPlan));
        protected IEnumerable TestPlan
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(TestPlan), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Owner
        protected bool Has_Owner { get; set; }
        protected string Owner { get; set; }
        // NoPlanDetails
        protected bool Has_NoPlanDetails { get; set; }
        protected bool NoPlanDetails { get; set; }
        // Active
        protected bool Has_Active { get; set; }
        protected bool Active { get; set; }
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
            // Owner
            Has_Owner = Parameters.HasParameter("Owner");
            Owner = Parameters.Get<string>("Owner");
            // NoPlanDetails
            Has_NoPlanDetails = Parameters.HasParameter("NoPlanDetails");
            NoPlanDetails = Parameters.Get<bool>("NoPlanDetails");
            // Active
            Has_Active = Parameters.HasParameter("Active");
            Active = Parameters.Get<bool>("Active");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetTestPlanController(TfsCmdlets.HttpClients.ITestPlanHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}