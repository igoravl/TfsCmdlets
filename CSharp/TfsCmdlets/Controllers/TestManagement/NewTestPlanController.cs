using System;
using System.Collections.Generic;
using System.Composition;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using TfsCmdlets.Cmdlets.TestManagement;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.TestManagement
{
    [CmdletController(typeof(TestPlan))]
    partial class NewTestPlanController
    {
        [Import]
        private INodeUtil NodeUtil {get;set;}
        
        public override IEnumerable<TestPlan> Invoke()
        {
            var testPlan = Parameters.Get<string>(nameof(NewTestPlan.TestPlan));
            var owner = Parameters.Get<string>(nameof(NewTestPlan.Owner));
            var areaPath = Parameters.Get<string>(nameof(NewTestPlan.AreaPath), "\\");
            var iterationPath = Parameters.Get<string>(nameof(NewTestPlan.IterationPath), "\\");
            var startDate = Parameters.Get<DateTime>(nameof(NewTestPlan.StartDate));
            var endDate = Parameters.Get<DateTime>(nameof(NewTestPlan.EndDate));

            var tp = Data.GetProject();

            var client = Data.GetClient<TestPlanHttpClient>();

            yield return client.CreateTestPlanAsync(new TestPlanCreateParams()
            {
                AreaPath = NodeUtil.NormalizeNodePath(areaPath, tp.Name, "Areas", includeTeamProject: true),
                Iteration = NodeUtil.NormalizeNodePath(iterationPath, tp.Name, "Iterations", includeTeamProject: true),
                Name = testPlan,
                // TODO: Owner = owner,
                StartDate = (startDate == DateTime.MinValue ? (DateTime?)null : startDate),
                EndDate = (endDate == DateTime.MinValue ? (DateTime?)null : endDate)
            }, tp.Name).GetResult($"Error creating test plan '{testPlan}'");
        }
    }
}