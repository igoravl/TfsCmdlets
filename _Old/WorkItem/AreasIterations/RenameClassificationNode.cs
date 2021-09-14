using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Renames a Work Area.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsArea", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class RenameAreaNode : RenameClassificationNode
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Area")]
        public override object Node { get; set; }

        /// <inheritdoc/>
        internal override TreeStructureGroup StructureGroup => TreeStructureGroup.Areas;
    }

    /// <summary>
    /// Renames a Iteration.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsIteration", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class RenameIterationNode : RenameClassificationNode
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public override object Node { get; set; }

        /// <inheritdoc/>
        internal override TreeStructureGroup StructureGroup => TreeStructureGroup.Iterations;
    }

    /// <summary>
    /// Base implementation for Rename-Area and Rename-Iteration
    /// </summary>
    public abstract class RenameClassificationNode : CmdletBase
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
        /// HELP_PARAM_NEWNAME
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string NewName { get; set; }

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
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            var result = RenameItem<ClassificationNode>();

            if (Passthru)
            {
                WriteObject(result);
            }
        }
    }

    partial class ClassificationNodeDataService
    {
        protected override ClassificationNode DoRenameItem()
        {
            var (_, tp) = GetCollectionAndProject();
            var client = GetClient<WorkItemTrackingHttpClient>();
            var nodeToRename = GetItem<ClassificationNode>();
            var structureGroup = GetParameter<TreeStructureGroup>("StructureGroup");
            var structureGroupName = structureGroup.ToString().TrimEnd('s');
            var newName = GetParameter<string>(nameof(RenameClassificationNode.NewName));

            if (!ShouldProcess($"{structureGroupName} '{nodeToRename.FullPath}'", $"Rename to '{newName}'"))
            {
                return null;
            }

            var patch = new WorkItemClassificationNode()
            {
                Name = newName,
                Attributes = nodeToRename.Attributes
            };
            
            return new ClassificationNode(client.UpdateClassificationNodeAsync(patch, tp.Name, structureGroup, nodeToRename.RelativePath.Substring(1))
                .GetResult($"Error renaming node '{nodeToRename.RelativePath}'"), tp.Name, client);
        }
    }
}