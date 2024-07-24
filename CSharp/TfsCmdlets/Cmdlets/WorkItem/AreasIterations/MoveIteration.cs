using System.Management.Automation;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Moves one or more Iterations to a new parent node
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(Models.ClassificationNode))]
    partial class MoveIteration
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public object Node { get; set; } = @"\**";

        /// <summary>
        /// Specifies the name and/or path of the destination parent node.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [Alias("MoveTo")]
        public object Destination { get; set; }

        /// <summary>
        /// Allows the cmdlet to create destination parent node(s) if they're missing.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(ClassificationNode), CustomBaseClass = typeof(MoveClassificationNodeController))]
    partial class MoveIterationController { 
        // See MoveClassificationNodeController
    }
}
