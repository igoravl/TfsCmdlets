//HintName: TfsCmdlets.Cmdlets.WorkItem.WorkItemType.ImportWorkItemType.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    [Cmdlet("Import", "TfsWorkItemType", SupportsShouldProcess = true)]
    public partial class ImportWorkItemType: CmdletBase
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