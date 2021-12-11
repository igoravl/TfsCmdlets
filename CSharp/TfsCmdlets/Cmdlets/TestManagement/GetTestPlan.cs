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
}