using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    /// <summary>
    /// Renames a test plans.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess=true, OutputType = typeof(TestPlan))]
    partial class RenameTestPlan
    {
        /// <summary>
        /// Specifies the test plan name.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline=true)]
        [Alias("Id", "Name")]
        public object TestPlan { get; set; }
    }

    [CmdletController(typeof(TestPlan))]
    partial class RenameTestPlanController
    {
        protected override IEnumerable Run()
        {
            var plan = Data.GetItem<TestPlan>();
            var newName = Parameters.Get<string>("NewName");

            var tp = Data.GetProject();

            if (!PowerShell.ShouldProcess(tp, $"Rename test plan '{plan.Name}' to '{newName}'")) yield break;

            var client = Data.GetClient<TestPlanHttpClient>();

            yield return client.UpdateTestPlanAsync(new TestPlanUpdateParams() { Name = newName }, tp.Name, plan.Id)
                .GetResult($"Error renaming test plan '{plan.Name}'");
        }
    }
}