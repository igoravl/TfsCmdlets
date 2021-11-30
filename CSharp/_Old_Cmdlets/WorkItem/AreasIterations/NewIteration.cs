using System;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Creates a new Iteration in the given Team Project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsIteration", SupportsShouldProcess = true)]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class NewIteration : CmdletBase
    {
        /// <summary>
        /// Specifies the path of the new Iteration. When supplying a path, use a backslash ("\\") 
        /// between the path segments. Leading and trailing backslashes are optional. 
        /// The last segment in the path will be the iteration name.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Iteration", "Path")]
        public string Node { get; set; }

        /// <summary>
        /// Specifies the start date of the iteration.
        /// </summary>
        [Parameter()]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Sets the finish date of the iteration. 
        /// </summary>
        [Parameter()]
        public DateTime? FinishDate { get; set; }

        /// <summary>
        /// Allows the cmdlet to create parent nodes if they're missing.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Indicates the type of structure (area or iteration)
        /// </summary>
        internal TreeStructureGroup StructureGroup => TreeStructureGroup.Iterations;

        /// <summary>
        /// Returns the type name for the underlying IController implementing the logic of this cmdlet
        /// </summary>
        protected override string CommandName =>
            nameof(TfsCmdlets.Controllers.WorkItem.AreasIterations.NewClassificationNode);
    }
}