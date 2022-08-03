using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    /// <summary>
    /// Deletes one or more test plans.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true)]
    partial class RemoveTestPlan
    {
        /// <summary>
        /// Specifies one or more test plans to delete. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [SupportsWildcards]
        [Alias("Id", "Name")]
        [ValidateNotNull()]
        public object TestPlan { get; set; }

        /// <summary>
        /// Forces the deletion of test plans with test suites and/or test cases. 
        /// When omitted, only empty test plans can be deleted.
        /// </summary>
        public SwitchParameter Force { get; set; }
    }
}