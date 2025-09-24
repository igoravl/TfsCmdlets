//HintName: TfsCmdlets.Cmdlets.WorkItem.Query.UndoWorkItemQueryFolderRemoval.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    [Cmdlet("Undo", "TfsWorkItemQueryFolderRemoval", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem))]
    public partial class UndoWorkItemQueryFolderRemoval: CmdletBase
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
    }
}