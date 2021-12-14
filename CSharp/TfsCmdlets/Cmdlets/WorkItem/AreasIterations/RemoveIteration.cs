using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Deletes one or more iterations from a given Team Project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(Models.ClassificationNode))]
    partial class RemoveIteration
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public object Node { get; set; }

        /// <summary>
        /// Specifies the new parent node for the work items currently assigned to the node 
        /// being deleted, if any. When omitted, defaults to the root node (the "\" node, at the
        /// team project level).
        /// </summary>
        [Parameter(Position = 1)]
        [Alias("NewPath")]
        public object MoveTo { get; set; } = "\\";

        /// <summary>
        /// Removes node(s) recursively.
        /// </summary>
        [Parameter()]
        public SwitchParameter Recurse { get; set; }

        [Parameter]
        internal TreeStructureGroup StructureGroup => TreeStructureGroup.Iterations;
    }
}