using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    /// <summary>
    /// Gets the contents of one or more test plans.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(TestPlan))]
    partial class GetTestPlan
    {
        /// <summary>
        /// Specifies the test plan name. Wildcards are supported. When omitted, returns all test cases in the given team project.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Id", "Name")]
        public object TestPlan { get; set; } = "*";

        /// <summary>
        /// Gets only the plans owned by the specified user.
        /// </summary>
        [Parameter]
        public string Owner { get; set; }

        /// <summary>
        /// Get only basic properties of the test plan.
        /// </summary>
        [Parameter]
        public SwitchParameter NoPlanDetails { get; set; }

        /// <summary>
        /// Get only the active plans.
        /// </summary>
        [Parameter]
        public SwitchParameter Active { get; set; }
    }

    [CmdletController(typeof(TestPlan), Client = typeof(ITestPlanHttpClient))]
    partial class GetTestPlanController
    {
        protected override IEnumerable Run()
        {
            var testPlan = Parameters.Get<object>(nameof(GetTestPlan.TestPlan));
            var owner = Parameters.Get<string>(nameof(GetTestPlan.Owner));
            var planDetails = !Parameters.Get<bool>(nameof(GetTestPlan.NoPlanDetails));
            var active = Parameters.Get<bool>(nameof(GetTestPlan.Active));

            while (true) switch (testPlan)
            {
                case TestPlan plan:
                {
                    yield return plan;
                    yield break;
                }
                case int i:
                {
                    var tp = Data.GetProject();
                    yield return Client.GetTestPlanByIdAsync(tp.Id, i)
                        .GetResult($"Error getting test plan '{i}'");
                    yield break;
                }
                case string s:
                {
                    var tp = Data.GetProject();
                    foreach (var plan in Client.GetTestPlansAsync(tp.Id, owner, null, planDetails, active)
                                 .GetResult($"Error getting test plans '{testPlan}'")
                                 .Where(plan => plan.Name.IsLike(s)))
                    {
                        yield return plan;
                    }
                    yield break;
                }
                default:
                {
                    throw new ArgumentException($"Invalid or non-existent test plan '{testPlan}'");
                }
            }
        }
    }
}