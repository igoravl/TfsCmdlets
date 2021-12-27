using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Moves one or more Work Item Areas to a new parent node
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(Models.ClassificationNode))]
    partial class MoveArea
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Area")]
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
}