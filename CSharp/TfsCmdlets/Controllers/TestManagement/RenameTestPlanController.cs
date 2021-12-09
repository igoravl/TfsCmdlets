using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;

namespace TfsCmdlets.Controllers.TestManagement
{
    [CmdletController(typeof(TestPlan))]
    partial class RenameTestPlanController
    {
        public override IEnumerable<TestPlan> Invoke()
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