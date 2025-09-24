//HintName: TfsCmdlets.Cmdlets.WorkItem.Query.GetWorkItemQuery.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.Query
{
    [Cmdlet("Get", "TfsWorkItemQuery")]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem))]
    public partial class GetWorkItemQuery: CmdletBase
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