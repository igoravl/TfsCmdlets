using System.Management.Automation;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Renames an iteration path.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(Models.ClassificationNode))]
    partial class RenameIteration
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public object Node { get; set; }
    }

    [CmdletController(typeof(ClassificationNode), CustomBaseClass = typeof(RenameClassificationNodeController))]
    partial class RenameIterationController { 
        // See RenameClassificationNodeController
    }
}