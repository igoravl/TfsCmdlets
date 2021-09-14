using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Modifies the dates of an iteration.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "TfsIteration")]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class SetIteration : SetClassificationNode
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public override object Node { get; set; }

        /// <summary>
        /// Specifies the start date of the iteration. To clear the start date, set it to $null. Note that when clearing a date, 
        /// both must be cleared at the same time (i.e. setting both StartDate and FinishDate to $null).
        /// </summary>
        [Parameter(Mandatory = true)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Sets the finish date of the iteration. To clear the finish date, set it to $null. Note that when clearing a date, 
        /// both must be cleared at the same time (i.e. setting both StartDate and FinishDate to $null).
        /// </summary>
        [Parameter(Mandatory = true)]
        public DateTime? FinishDate { get; set; }

        /// <inheritdoc/>
        internal override TreeStructureGroup StructureGroup => TreeStructureGroup.Iterations;
    }

    /// <summary>
    /// Base implementation for Set-Iteration
    /// </summary>
    public abstract class SetClassificationNode : SetCmdletBase<ClassificationNode>
    {
        /// <summary>
        /// Specifies the name and/or path of the node (area or iteration)
        /// </summary>
        public abstract object Node { get; set; }

        /// <summary>
        /// Indicates the type of structure (area or iteration)
        /// </summary>
        [Parameter()]
        internal abstract TreeStructureGroup StructureGroup { get; }
    }

    partial class ClassificationNodeDataService
    {
        protected override ClassificationNode DoSetItem()
        {
            var nodeToSet = GetItem<ClassificationNode>();
            var startDate = GetParameter<DateTime?>(nameof(SetIteration.StartDate));
            var finishDate = GetParameter<DateTime?>(nameof(SetIteration.FinishDate));
            var structureGroup = GetParameter<TreeStructureGroup>("StructureGroup");

            ErrorUtil.ThrowIfNotFound(nodeToSet, nameof(SetIteration.Node), GetParameter<string>(nameof(SetIteration.Node)));

            var (_, tp) = GetCollectionAndProject();

            var client = GetClient<WorkItemTrackingHttpClient>();

            if (!ShouldProcess(tp, $"Set dates on iteration '{nodeToSet.RelativePath}'")) return null;

            var patch = new WorkItemClassificationNode()
            {
                Attributes = new Dictionary<string, object>()
                {
                    ["startDate"] = startDate,
                    ["finishDate"] = finishDate
                }
            };

            var result = client.UpdateClassificationNodeAsync(patch, tp.Name, structureGroup, nodeToSet.RelativePath.Substring(1))
                .GetResult($"Error setting dates on iteration '{nodeToSet.FullPath}'");

            return new ClassificationNode(result, tp.Name, client);
        }
    }
}