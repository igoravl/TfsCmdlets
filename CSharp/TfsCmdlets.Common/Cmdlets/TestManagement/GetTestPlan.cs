using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    /// <summary>
    /// Gets the contents of one or more test plans.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTestPlan")]
    [OutputType(typeof(TestPlan))]
    public class GetTestPlan : GetCmdletBase<TestPlan>
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
        [Parameter()]
        public string Owner { get; set; }

        /// <summary>
        /// Get only basic properties of the test plan.
        /// </summary>
        [Parameter()]
        public SwitchParameter NoPlanDetails { get; set; }

        /// <summary>
        /// Get only the active plans.
        /// </summary>
        [Parameter()]
        public SwitchParameter Active { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        /// <value></value>
        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }
    }

    [Exports(typeof(TestPlan))]
    internal partial class TestPlanDataService : BaseDataService<TestPlan>
    {
        protected override IEnumerable<TestPlan> DoGetItems()
        {
            var testPlan = GetParameter<object>(nameof(GetTestPlan.TestPlan));
            var owner = GetParameter<string>(nameof(GetTestPlan.Owner));
            var planDetails = !GetParameter<bool>(nameof(GetTestPlan.NoPlanDetails));
            var active = GetParameter<bool>(nameof(GetTestPlan.Active));

            while (true) switch (testPlan)
                {
                    case TestPlan plan:
                        {
                            yield return plan;
                            yield break;
                        }
                    case int i:
                        {
                            var (_, tp) = GetCollectionAndProject();
                            var client = GetClient<TestPlanHttpClient>();
                            yield return client.GetTestPlanByIdAsync(tp.Id, i)
                                .GetResult($"Error getting test plan '{i}'");
                            yield break;
                        }
                    case string s:
                        {
                            var (_, tp) = GetCollectionAndProject();
                            var client = GetClient<TestPlanHttpClient>();
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