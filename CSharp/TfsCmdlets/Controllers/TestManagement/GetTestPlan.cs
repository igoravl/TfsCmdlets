using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    [CmdletController(typeof(TestPlan))]
    partial class GetTestPlanController
    {
        public override IEnumerable<TestPlan> Invoke()
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
                            var client = Data.GetClient<TestPlanHttpClient>();
                            yield return client.GetTestPlanByIdAsync(tp.Id, i)
                                .GetResult($"Error getting test plan '{i}'");
                            yield break;
                        }
                    case string s:
                        {
                            var tp = Data.GetProject();
                            var client = Data.GetClient<TestPlanHttpClient>();
                            foreach (var plan in client.GetTestPlansAsync(tp.Id, owner, null, planDetails, active)
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