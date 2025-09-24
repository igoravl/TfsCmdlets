//HintName: TfsCmdlets.Cmdlets.WorkItem.Tagging.RemoveWorkItemTag.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    [Cmdlet("Remove", "TfsWorkItemTag", SupportsShouldProcess = true)]
    public partial class RemoveWorkItemTag: CmdletBase
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