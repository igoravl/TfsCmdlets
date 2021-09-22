using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Gets one or more Work Item Areas from a given Team Project.
    /// </summary>
    [Cmdlet(VerbsCommon.Move, "TfsArea", SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class MoveArea : CmdletBase
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

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Returns the type name for the underlying ICommand implementing the logic of this cmdlet
        /// </summary>
        internal TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
    }

    /// <summary>
    /// Gets one or more Iterations from a given Team Project.
    /// </summary>
    /// <example>
    ///   <code>Get-TfsIteration</code>
    ///   <para>Returns all aiterations in the currently connected Team Project (as defined by a previous call to Connect-TfsTeamProject)</para>
    /// </example>
    /// <example>
    ///   <code>Get-TfsIteration '\\**\\Support' -Project Tailspin</code>
    ///   <para>Performs a recursive search and returns all iterations named 'Support' that may exist in a team project called Tailspin</para>
    /// </example>
    [Cmdlet(VerbsCommon.Move, "TfsIteration", SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class MoveIteration : MoveClassificationNode
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public override object Node { get; set; } = @"\**";

        /// <inheritdoc/>
        internal override TreeStructureGroup StructureGroup => TreeStructureGroup.Iterations;
    }

    /// <summary>
    /// Base implementation for Move-Area and Move-Iteration
    /// </summary>
    public abstract class MoveClassificationNode : CmdletBase
    {
        /// <summary>
        /// Specifies the name and/or path of the node (area or iteration)
        /// </summary>
        public virtual object Node { get; set; }

        /// <summary>
        /// Indicates the type of structure (area or iteration)
        /// </summary>
        [Parameter()]
        internal abstract TreeStructureGroup StructureGroup { get; }

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

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        // TODO

        ///// <summary>
        ///// Performs execution of the command
        ///// </summary>
        //protected override void DoProcessRecord()
        //{
        //    var tp = Project;

        //    var sourceNodes = GetItems<ClassificationNode>();

        //    if (sourceNodes == null) return;

        //    var destinationNode = GetItem<ClassificationNode>(new { Node = Destination });

        //    if (destinationNode == null && !Force)
        //    {
        //        ErrorUtil.ThrowIfNotFound(destinationNode, nameof(MoveClassificationNode.Destination), Destination);
        //    }

        //    this.Log($"Destination node: '{destinationNode.FullPath}'");

        //    foreach (var sourceNode in sourceNodes)
        //    {
        //        this.Log($"Source node: '{sourceNode.FullPath}'");

        //        var moveTo = $@"{destinationNode.Path}\{sourceNode.Name}";

        //        if (!PowerShell.ShouldProcess($"Team Project '{sourceNode.TeamProject}'", $"Move {StructureGroup} '{sourceNode.FullPath}'"))
        //        {
        //            return;
        //        }

        //        var patch = new WorkItemClassificationNode()
        //        {
        //            Id = sourceNode.Id
        //        };

        //        var client = Data.GetClient<WorkItemTrackingHttpClient>(parameters);

        //        var result = client.CreateOrUpdateClassificationNodeAsync(patch, tp.Name, StructureGroup, destinationNode.RelativePath.Substring(1))
        //            .GetResult($"Error moving node {sourceNode.RelativePath} to {destinationNode.RelativePath}");

        //        if (Passthru.IsPresent)
        //        {
        //            WriteObject(result);
        //        }
        //    }
        //}
    }
}
