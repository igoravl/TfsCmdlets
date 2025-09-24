//HintName: TfsCmdlets.Cmdlets.WorkItem.WorkItemType.GetWorkItemType.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    [Cmdlet("Get", "TfsWorkItemType", DefaultParameterSetName = "Get by type")]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType))]
    public partial class GetWorkItemType: CmdletBase
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