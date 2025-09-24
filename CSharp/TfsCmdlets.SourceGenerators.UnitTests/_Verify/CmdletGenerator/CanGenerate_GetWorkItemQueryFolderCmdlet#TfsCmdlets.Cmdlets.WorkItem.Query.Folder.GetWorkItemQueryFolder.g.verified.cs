//HintName: TfsCmdlets.Cmdlets.WorkItem.Query.Folder.GetWorkItemQueryFolder.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.Query.Folder
{
    [Cmdlet("Get", "TfsWorkItemQueryFolder")]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem))]
    public partial class GetWorkItemQueryFolder: CmdletBase
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
    }
}