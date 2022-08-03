using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Deletes one or more Work Item Areas from a given Team Project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(Models.ClassificationNode))]
    partial class RemoveArea 
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Area")]
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
    }
}