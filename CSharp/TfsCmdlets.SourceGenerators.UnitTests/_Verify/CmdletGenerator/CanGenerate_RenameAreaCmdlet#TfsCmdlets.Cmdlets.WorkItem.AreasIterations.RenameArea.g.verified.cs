//HintName: TfsCmdlets.Cmdlets.WorkItem.AreasIterations.RenameArea.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    [Cmdlet("Rename", "TfsArea", SupportsShouldProcess = true)]
    [OutputType(typeof(TfsCmdlets.Models.ClassificationNode))]
    public partial class RenameArea: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_NEWNAME
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        public string NewName { get; set; }
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