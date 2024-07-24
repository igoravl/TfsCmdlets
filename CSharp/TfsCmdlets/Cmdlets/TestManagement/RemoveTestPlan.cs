using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    /// <summary>
    /// Deletes one or more test plans.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true)]
    partial class RemoveTestPlan
    {
        /// <summary>
        /// Specifies one or more test plans to delete. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [SupportsWildcards]
        [Alias("Id", "Name")]
        [ValidateNotNull()]
        public object TestPlan { get; set; }

        /// <summary>
        /// Forces the deletion of test plans with test suites and/or test cases. 
        /// When omitted, only empty test plans can be deleted.
        /// </summary>
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(TestPlan))]
    partial class RemoveTestPlanController
    {
        protected override IEnumerable Run()
        {
            var plans = Data.GetItems<TestPlan>();
            var force = Parameters.Get<bool>(nameof(RemoveTestPlan.Force));
            var client = Data.GetClient<TestPlanHttpClient>();

            foreach (var plan in plans)
            {
                if (!PowerShell.ShouldProcess($"[Project: {plan.Project.Name}]/[Plan: #{plan.Id} ({plan.Name})]", $"Delete test plan")) continue;

                var suites = client.GetTestSuitesForPlanAsync(plan.Project.Name, plan.Id, SuiteExpand.Children)
                    .GetResult($"Error retrieving test suites for test plan '{plan.Name}'");

                var hasChildren = (suites.Count > 1 || suites[0].Children?.Count > 0);

                if (hasChildren && !force & !PowerShell.ShouldContinue($"Are you sure you want to delete test plan '{plan.Name}' and all of its contents?")) continue;

                client.DeleteTestPlanAsync(plan.Project.Name, plan.Id)
                    .Wait($"Error deleting test plan '{plan.Name}'");
            }
            return null;
        }
    }
}