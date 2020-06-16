using System;
using System.IO;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Creates a new Work Item Area in the given Team Project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsArea", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class NewArea : NewClassificationNode
    {
        /// <summary>
        /// Specifies the path of the new Area. When supplying a path, use a backslash ("\\") 
        /// between the path segments. Leading and trailing backslashes are optional. 
        /// The last segment in the path will be the area name.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Area", "Path")]
        public override string Node { get; set; }

        /// <inheritdoc/>
        protected override TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
    }

    /// <summary>
    /// Creates a new Iteration in the given Team Project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsIteration", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class NewIteration : NewClassificationNode
    {
        /// <summary>
        /// Specifies the path of the new Iteration. When supplying a path, use a backslash ("\\") 
        /// between the path segments. Leading and trailing backslashes are optional. 
        /// The last segment in the path will be the iteration name.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Iteration", "Path")]
        public override string Node { get; set; }

        /// <inheritdoc/>
        protected override TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
    }

    /// <summary>
    /// Base implementation for New-Area and New-Iteration
    /// </summary>
    public abstract class NewClassificationNode : NewCmdletBase<ClassificationNode>
    {
        /// <summary>
        /// Specifies the name and/or path of the node (area or iteration)
        /// </summary>
        public virtual string Node { get; set; }

        /// <summary>
        /// Indicates the type of structure (area or iteration)
        /// </summary>
        [Parameter()]
        protected abstract TreeStructureGroup StructureGroup { get; }

        /// <summary>
        /// Allows the cmdlet to create parent nodes if they're missing.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }
    }

    partial class ClassificationNodeDataService
    {
        protected override ClassificationNode DoNewItem()
        {
            var node = GetParameter<string>(nameof(NewClassificationNode.Node));
            var structureGroup = GetParameter<TreeStructureGroup>("StructureGroup");
            var force = GetParameter<bool>(nameof(NewClassificationNode.Force));

            var (_, tp) = GetCollectionAndProject();
            var nodePath = NodeUtil.NormalizeNodePath(node, tp.Name, structureGroup.ToString(), false, false, true);
            var client = GetClient<WorkItemTrackingHttpClient>();
            var parentPath = Path.GetDirectoryName(nodePath);
            var nodeName = Path.GetFileName(nodePath);

            if (!ShouldProcess($"Team Project {tp.Name}", $"Create node '{nodePath}'")) return null;

            if (!TestItem<ClassificationNode>(new { Node = parentPath }))
            {
                if (!force)
                {
                    this.Log($"Parent node '{parentPath}' does not exist");
                    throw new Exception($"Parent node '{parentPath}' does not exist. Check the path or use -Force the create any missing parent nodes.");
                }

                NewItem<ClassificationNode>(new { Node = parentPath });
            }

            var patch = new WorkItemClassificationNode()
            {
                Name = nodeName
            };

            var result = client.CreateOrUpdateClassificationNodeAsync(patch, tp.Name, structureGroup, parentPath)
                .GetResult($"Error creating node {nodePath}");

            return new ClassificationNode(result, tp.Name, client);
        }
    }
}