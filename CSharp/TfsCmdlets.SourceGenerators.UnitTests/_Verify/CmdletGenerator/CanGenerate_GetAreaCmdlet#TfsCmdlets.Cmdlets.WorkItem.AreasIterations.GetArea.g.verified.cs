//HintName: TfsCmdlets.Cmdlets.WorkItem.AreasIterations.GetArea.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    [Cmdlet("Get", "TfsArea")]
    [OutputType(typeof(TfsCmdlets.Models.ClassificationNode))]
    public partial class GetArea: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline=true)]
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
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Areas;
    }
}