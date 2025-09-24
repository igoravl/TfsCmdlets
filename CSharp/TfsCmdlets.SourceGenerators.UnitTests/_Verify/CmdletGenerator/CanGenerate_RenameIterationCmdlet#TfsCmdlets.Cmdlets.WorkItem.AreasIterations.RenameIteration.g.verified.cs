//HintName: TfsCmdlets.Cmdlets.WorkItem.AreasIterations.RenameIteration.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.AreasIterations
{
    [Cmdlet("Rename", "TfsIteration", SupportsShouldProcess = true)]
    [OutputType(typeof(TfsCmdlets.Models.ClassificationNode))]
    public partial class RenameIteration: CmdletBase
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
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Iterations;
    }
}