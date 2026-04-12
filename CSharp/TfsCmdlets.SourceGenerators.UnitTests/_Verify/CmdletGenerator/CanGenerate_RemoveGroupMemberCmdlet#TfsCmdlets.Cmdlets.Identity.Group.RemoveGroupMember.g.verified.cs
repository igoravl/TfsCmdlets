//HintName: TfsCmdlets.Cmdlets.Identity.Group.RemoveGroupMember.g.cs
namespace TfsCmdlets.Cmdlets.Identity.Group
{
    [Cmdlet("Remove", "TfsGroupMember", SupportsShouldProcess = true)]
    public partial class RemoveGroupMember: CmdletBase
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