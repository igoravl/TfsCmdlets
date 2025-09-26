//HintName: TfsCmdlets.Cmdlets.WorkItem.AreasIterations.CopyIteration.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    [Cmdlet("Copy", "TfsIteration", SupportsShouldProcess = true)]
    [OutputType(typeof(TfsCmdlets.Models.ClassificationNode))]
    public partial class CopyIteration: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Alias("Organization")]
        [Parameter()]
        public object Collection { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
        [Parameter]
        internal Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup StructureGroup => 
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Iterations;
    }
}