using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using TfsCmdlets.Cmdlets.TestManagement;

namespace TfsCmdlets.Controllers.TestManagement
{
    [CmdletController(typeof(TestPlan))]
    partial class NewTestPlanController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        public override IEnumerable<TestPlan> Invoke()
        {
            var testPlan = Parameters.Get<string>(nameof(NewTestPlan.TestPlan));
            var owner = Parameters.Get<object>(nameof(NewTestPlan.Owner));
            var areaPath = Parameters.Get<string>(nameof(NewTestPlan.AreaPath), "\\");
            var iterationPath = Parameters.Get<string>(nameof(NewTestPlan.IterationPath), "\\");
            var startDate = Parameters.Get<DateTime>(nameof(NewTestPlan.StartDate));
            var endDate = Parameters.Get<DateTime>(nameof(NewTestPlan.EndDate));

            WebApiIdentityRef ownerRef = null;

            if (owner != null)
            {
                var ownerIdentity = Data.GetItem<Models.Identity>(new { Identity = owner });
                ownerRef = new Microsoft.VisualStudio.Services.WebApi.IdentityRef();
                ownerRef.CopyFrom(ownerIdentity);
            }

            var tp = Data.GetProject();

            var client = Data.GetClient<TestPlanHttpClient>();

            yield return client.CreateTestPlanAsync(new TestPlanCreateParams()
            {
                AreaPath = NodeUtil.NormalizeNodePath(areaPath, tp.Name, "Areas", includeTeamProject: true),
                Iteration = NodeUtil.NormalizeNodePath(iterationPath, tp.Name, "Iterations", includeTeamProject: true),
                Name = testPlan,
                Owner = ownerRef,
                StartDate = (startDate == DateTime.MinValue ? (DateTime?)null : startDate),
                EndDate = (endDate == DateTime.MinValue ? (DateTime?)null : endDate)
            }, tp.Name).GetResult($"Error creating test plan '{testPlan}'");
        }
    }
}