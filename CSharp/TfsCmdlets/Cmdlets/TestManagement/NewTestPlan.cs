using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    /// <summary>
    /// Creates a new test plan.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsTestPlan")]
    [OutputType(typeof(TestPlan))]
    [TfsCmdlet(CmdletScope.Project)]
    partial class NewTestPlan
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
        [Parameter]
        public string AreaPath { get; set; }

        /// <summary>
        /// Specifies the owner of the new test plan.
        /// </summary>
        [Parameter]
        public string IterationPath { get; set; }

        /// <summary>
        /// Specifies the start date of the test plan.
        /// </summary>
        [Parameter]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Specifies the end date of the test plan.
        /// </summary>
        [Parameter]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Specifies the owner of the new test plan.
        /// </summary>
        [Parameter]
        public string Owner { get; set; }
    }
}