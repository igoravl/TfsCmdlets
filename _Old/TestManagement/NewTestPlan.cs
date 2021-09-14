using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    /// <summary>
    /// Creates a new test plan.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsTestPlan")]
    [OutputType(typeof(TestPlan))]
    public class NewTestPlan : NewCmdletBase<TestPlan>
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
        [Parameter()]
        public string AreaPath { get; set; }

        /// <summary>
        /// Specifies the owner of the new test plan.
        /// </summary>
        [Parameter()]
        public string IterationPath { get; set; }

        /// <summary>
        /// Specifies the start date of the test plan.
        /// </summary>
        [Parameter()]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Specifies the end date of the test plan.
        /// </summary>
        [Parameter()]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Specifies the owner of the new test plan.
        /// </summary>
        [Parameter()]
        public string Owner { get; set; }
    }

    partial class TestPlanDataService
    {
        protected override TestPlan DoNewItem()
        {
            var testPlan = GetParameter<string>(nameof(NewTestPlan.TestPlan));
            var owner = GetParameter<string>(nameof(NewTestPlan.Owner));
            var areaPath = GetParameter<string>(nameof(NewTestPlan.AreaPath), "\\");
            var iterationPath = GetParameter<string>(nameof(NewTestPlan.IterationPath), "\\");
            var startDate = GetParameter<DateTime>(nameof(NewTestPlan.StartDate));
            var endDate = GetParameter<DateTime>(nameof(NewTestPlan.EndDate));

            var (_, tp) = GetCollectionAndProject();

            var client = GetClient<TestPlanHttpClient>();

            return client.CreateTestPlanAsync(new TestPlanCreateParams() {
                AreaPath = NodeUtil.NormalizeNodePath(areaPath, tp.Name, "Areas", includeTeamProject: true),
                Iteration = NodeUtil.NormalizeNodePath(iterationPath, tp.Name, "Iterations", includeTeamProject: true),
                Name = testPlan,
                // TODO: Owner = owner,
                StartDate = (startDate == DateTime.MinValue? (DateTime?) null: startDate),
                EndDate = (endDate == DateTime.MinValue? (DateTime?) null: endDate)
            }, tp.Name).GetResult($"Error creating test plan '{testPlan}'");
        }
    }
}