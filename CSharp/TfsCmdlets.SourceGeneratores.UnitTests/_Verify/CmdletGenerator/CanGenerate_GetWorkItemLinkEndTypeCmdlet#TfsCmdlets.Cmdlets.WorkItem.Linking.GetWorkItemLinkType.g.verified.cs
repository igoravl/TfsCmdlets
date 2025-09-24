//HintName: TfsCmdlets.Cmdlets.WorkItem.Linking.GetWorkItemLinkType.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    [Cmdlet("Get", "TfsWorkItemLinkType")]
    public partial class GetWorkItemLinkType: CmdletBase
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