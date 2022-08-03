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
}