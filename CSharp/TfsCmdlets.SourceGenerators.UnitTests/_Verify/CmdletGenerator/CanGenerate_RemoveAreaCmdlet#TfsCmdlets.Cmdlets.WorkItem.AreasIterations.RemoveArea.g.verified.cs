//HintName: TfsCmdlets.Cmdlets.WorkItem.AreasIterations.RemoveArea.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    [Cmdlet("Remove", "TfsArea", SupportsShouldProcess = true)]
    [OutputType(typeof(TfsCmdlets.Models.ClassificationNode))]
    public partial class RemoveArea: CmdletBase
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
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Areas;
    }
}