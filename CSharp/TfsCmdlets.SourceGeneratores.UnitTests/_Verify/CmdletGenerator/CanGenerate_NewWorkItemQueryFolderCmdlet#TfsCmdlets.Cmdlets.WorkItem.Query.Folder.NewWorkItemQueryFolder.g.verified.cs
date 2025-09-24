//HintName: TfsCmdlets.Cmdlets.WorkItem.Query.Folder.NewWorkItemQueryFolder.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.Query.Folder
{
    [Cmdlet("New", "TfsWorkItemQueryFolder", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem))]
    public partial class NewWorkItemQueryFolder: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
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