//HintName: TfsCmdlets.Cmdlets.WorkItem.Linking.ExportWorkItemAttachment.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    [Cmdlet("Export", "TfsWorkItemAttachment", SupportsShouldProcess = true)]
    public partial class ExportWorkItemAttachment: CmdletBase
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