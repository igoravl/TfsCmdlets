using System.Management.Automation;

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


    // TODO

    ///// <summary>
    ///// Performs execution of the command
    ///// </summary>
    //protected override void DoProcessRecord()
    //{
    //    var tp = Data.GetProject();

    //    var sourceNodes = GetItems<ClassificationNode>();

    //    if (sourceNodes == null) return;

    //    var destinationNode = GetItem<ClassificationNode>(new { Node = Destination });

    //    if (destinationNode == null && !Force)
    //    {
    //        ErrorUtil.ThrowIfNotFound(destinationNode, nameof(MoveClassificationNode.Destination), Destination);
    //    }

    //    Logger.Log($"Destination node: '{destinationNode.FullPath}'");

    //    foreach (var sourceNode in sourceNodes)
    //    {
    //        Logger.Log($"Source node: '{sourceNode.FullPath}'");

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
