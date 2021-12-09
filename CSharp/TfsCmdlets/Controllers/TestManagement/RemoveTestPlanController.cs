using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using TfsCmdlets.Cmdlets.TestManagement;

namespace TfsCmdlets.Controllers.TestManagement
{
    [CmdletController(typeof(TestPlan))]
    partial class RemoveTestPlanController
    {
        public override IEnumerable<TestPlan> Invoke()
        {
            var plans = Data.GetItems<TestPlan>();
            var force = Parameters.Get<bool>(nameof(RemoveTestPlan.Force));
            var tp = Data.GetProject();
            var client = Data.GetClient<TestPlanHttpClient>();

            foreach (var plan in plans)
            {
                if (!PowerShell.ShouldProcess(tp, $"Delete test plan '{plan.Name}'")) continue;

                var hasChildren = true; // TODO: Get children

                if (hasChildren && !force & !PowerShell.ShouldContinue($"Are you sure you want to delete test plan '{plan.Name}' and all of its contents?")) continue;

                client.DeleteTestPlanAsync(tp.Name, plan.Id)
                    .Wait($"Error deleting test plan '{plan.Name}'");
            }
            return null;
        }
    }
}