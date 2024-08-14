using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    /// <summary>
    /// Creates a new test plan.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(TestPlan))]
    partial class NewTestPlan
    {
        /// <summary>
        /// Specifies the test plan name.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("Name")]
        public string TestPlan { get; set; }

        /// <summary>
        /// Specifies the owner of the new test plan.
        /// </summary>
        [Parameter]
        public string AreaPath { get; set; }

        /// <summary>
        /// Specifies the owner of the new test plan.
        /// </summary>
        [Parameter]
        public string IterationPath { get; set; }

        /// <summary>
        /// Specifies the start date of the test plan.
        /// </summary>
        [Parameter]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Specifies the end date of the test plan.
        /// </summary>
        [Parameter]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Specifies the owner of the new test plan.
        /// </summary>
        [Parameter]
        public object Owner { get; set; }
    }

    [CmdletController(typeof(TestPlan), Client=typeof(ITestPlanHttpClient))]
    partial class NewTestPlanController
    {
        [Import]
        private INodeUtil NodeUtil { get; set; }

        protected override IEnumerable Run()
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

            yield return Client.CreateTestPlanAsync(new TestPlanCreateParams()
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