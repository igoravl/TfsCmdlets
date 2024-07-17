using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Models;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    /// <summary>
    /// Modifies the dates of an iteration.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(Models.ClassificationNode))]
    partial class SetIteration 
    {
        /// <summary>
        /// HELP_PARAM_ITERATION
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "Iteration")]
        public object Node { get; set; }

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
        [Parameter(Mandatory = true, ParameterSetName = "Set by finish date")]
        public DateTime? FinishDate { get; set; }

        /// <summary>
        /// Sets the length (in days) of the iteration. To clear the finish date, set it to 0. Note that when clearing a date, 
        /// both must be cleared at the same time (i.e. setting both StartDate to $null and Length to 0).
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Set by iteration length")]
        [ValidateRange(0, int.MaxValue)]
        public int Length { get; set; } = 0;
    }

    [CmdletController(typeof(ClassificationNode))]
    partial class SetIterationController 
    {
        protected override IEnumerable Run()
        {
            var nodeToSet = Data.GetItem<ClassificationNode>();
            var startDate = Parameters.Get<DateTime?>("StartDate");
            var finishDate = Parameters.Get<DateTime?>("FinishDate");
            var structureGroup = Parameters.Get<TreeStructureGroup>("StructureGroup");

            ErrorUtil.ThrowIfNotFound(nodeToSet, "Node", Parameters.Get<string>("Node"));

            var tp = Data.GetProject();
            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            if (!PowerShell.ShouldProcess(tp, $"Set dates on iteration '{nodeToSet.RelativePath}'")) yield break;

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

            yield return new ClassificationNode(result, tp.Name, client);
        }
    }
}