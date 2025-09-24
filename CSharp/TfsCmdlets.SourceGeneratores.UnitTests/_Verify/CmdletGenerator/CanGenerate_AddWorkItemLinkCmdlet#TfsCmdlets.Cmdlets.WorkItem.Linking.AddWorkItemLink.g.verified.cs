//HintName: TfsCmdlets.Cmdlets.WorkItem.Linking.AddWorkItemLink.g.cs
namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    [Cmdlet("Add", "TfsWorkItemLink", SupportsShouldProcess = true)]
    public partial class AddWorkItemLink: CmdletBase
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