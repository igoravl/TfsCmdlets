using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Copies one or more Iterations recursively
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(Models.ClassificationNode))]
    partial class CopyIteration
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public object Node { get; set; }

        /// <summary>
        /// Specifies the name and/or path of the destination parent node.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [Alias("CopyTo")]
        public object Destination { get; set; }

        /// <summary>
        /// Specifies the name and/or path of the destination team project. 
        /// When omitted, copies the iteration to the same team project.
        /// </summary>
        [Parameter]
        public object DestinationProject { get; set; }

        /// <summary>
        /// Allows the cmdlet to create destination parent node(s) if they're missing.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Copies nodes recursively. When omitted, sub-nodes are not copied.
        /// </summary>
        [Parameter()]
        public SwitchParameter Recurse { get; set; }
    }

    [CmdletController(typeof(Models.ClassificationNode), CustomBaseClass = typeof(CopyClassificationNodeController))]
    partial class CopyIterationController { 
        // See CopyClassificationNodeController
    }

}