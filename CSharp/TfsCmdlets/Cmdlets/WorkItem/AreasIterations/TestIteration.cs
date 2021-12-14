using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Determines whether the specified iteration exist.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(Models.ClassificationNode))]
    partial class TestIteration
    {
        /// <summary>
        /// HELP_PARAM_AREA
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Iteration", "Path")]
        [SupportsWildcards()]
        public string Node { get; set; }

        [Parameter]
        internal TreeStructureGroup StructureGroup => TreeStructureGroup.Iterations;
    }
}