//HintName: TfsCmdlets.Cmdlets.WorkItem.WorkItemType.ExportWorkItemType.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    [Cmdlet("Export", "TfsWorkItemType", SupportsShouldProcess = true, DefaultParameterSetName = "Export to file")]
    [OutputType(typeof(string))]
    public partial class ExportWorkItemType: CmdletBase
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