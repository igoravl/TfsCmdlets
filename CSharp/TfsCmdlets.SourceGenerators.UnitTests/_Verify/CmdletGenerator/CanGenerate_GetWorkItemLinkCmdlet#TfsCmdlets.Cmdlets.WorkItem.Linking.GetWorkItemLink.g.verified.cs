//HintName: TfsCmdlets.Cmdlets.WorkItem.Linking.GetWorkItemLink.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    [Cmdlet("Get", "TfsWorkItemLink")]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation))]
    public partial class GetWorkItemLink: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Alias("Organization")]
        [Parameter(ValueFromPipeline=true)]
        public object Collection { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}