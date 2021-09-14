using System;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;
using TfsCmdlets.Extensions;
using System.Linq;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Deletes one or more Work Item Areas from a given Team Project.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsArea", SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class RemoveArea : RemoveClassificationNode
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Area")]
        public override object Node { get; set; }

        /// <inheritdoc/>
        internal override TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
    }

    /// <summary>
    /// Deletes one or more Iterations from a given Team Project.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsIteration", SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class RemoveIteration : RemoveClassificationNode
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public override object Node { get; set; }

        /// <inheritdoc/>
        internal override TreeStructureGroup StructureGroup => TreeStructureGroup.Iterations;
    }

    /// <summary>
    /// Base implementation for Remove-Area and Remove-Iteration
    /// </summary>
    public abstract class RemoveClassificationNode : CmdletBase
    {
        /// <summary>
        /// Specifies the name and/or path of the node (area or iteration).
        /// </summary>
        public virtual object Node { get; set; }

        /// <summary>
        /// Indicates the type of structure (area or iteration).
        /// </summary>
        [Parameter()]
        internal abstract TreeStructureGroup StructureGroup { get; }

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
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            RemoveItem<ClassificationNode>();
        }
    }

    partial class ClassificationNodeDataService
    {
        protected override void DoRemoveItem()
        {
            var nodes = GetItems<ClassificationNode>().OrderByDescending(n=>n.Path).ToList();
            var moveTo = GetParameter<object>(nameof(RemoveClassificationNode.MoveTo));
            var recurse = GetParameter<bool>(nameof(RemoveClassificationNode.Recurse));
            var structureGroup = GetParameter<TreeStructureGroup>("StructureGroup");
            var structureGroupName = structureGroup.ToString().TrimEnd('s');
            var moveToNode = GetItem<ClassificationNode>(new {
                Node = moveTo
            });

            if (moveToNode == null)
            {
                throw new Exception($"Invalid or non-existent node '{moveTo}'. To remove nodes, supply a valid node in the -MoveTo argument");
            }

            this.Log($"Remove nodes and move orphaned work items to node '{moveToNode.FullPath}'");

            var (_, tp) = this.GetCollectionAndProject();
            var client = GetClient<WorkItemTrackingHttpClient>();

            foreach (var node in nodes)
            {
                if (!ShouldProcess($"Team Project '{tp.Name}'", $"Remove {structureGroupName} '{node.RelativePath}'")) continue;

                if(node.ChildCount > 0 && !recurse && !ShouldContinue($"The {structureGroupName} at '{node.RelativePath}' " +
                    "has children and the Recurse parameter was not specified. If you continue, all children will be removed with " +
                    "the item. Are you sure you want to continue?"))
                {
                    continue;
                }

                client.DeleteClassificationNodeAsync(node.TeamProject, structureGroup, node.RelativePath, moveToNode.Id)
                    .Wait($"Error removing node '{node.FullPath}'");
            }
        }
    }
}