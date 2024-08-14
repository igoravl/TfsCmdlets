using System.Management.Automation;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Determines whether the specified iteration exist.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(bool))]
    partial class TestIteration
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Iteration", "Path")]
        [SupportsWildcards()]
        public string Node { get; set; }
    }

    [CmdletController(CustomBaseClass = typeof(TestClassificationNodeController))]
    partial class TestIterationController { 
        // See TestClassificationNodeController
    }
}