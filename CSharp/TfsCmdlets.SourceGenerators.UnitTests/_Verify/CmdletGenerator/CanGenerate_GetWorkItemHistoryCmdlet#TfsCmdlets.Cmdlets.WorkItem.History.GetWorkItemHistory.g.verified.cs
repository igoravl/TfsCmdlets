//HintName: TfsCmdlets.Cmdlets.WorkItem.History.GetWorkItemHistory.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.History
{
    [Cmdlet("Get", "TfsWorkItemHistory")]
    [OutputType(typeof(TfsCmdlets.Models.WorkItemHistoryEntry))]
    public partial class GetWorkItemHistory: CmdletBase
    {
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