//HintName: TfsCmdlets.Cmdlets.Identity.Group.AddGroupMember.g.cs
namespace TfsCmdlets.Cmdlets.Identity.Group
{
    [Cmdlet("Add", "TfsGroupMember", SupportsShouldProcess = true)]
    public partial class AddGroupMember: CmdletBase
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