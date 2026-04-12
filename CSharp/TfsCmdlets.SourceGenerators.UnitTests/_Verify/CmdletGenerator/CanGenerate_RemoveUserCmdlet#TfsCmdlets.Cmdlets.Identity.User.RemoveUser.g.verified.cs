//HintName: TfsCmdlets.Cmdlets.Identity.User.RemoveUser.g.cs
namespace TfsCmdlets.Cmdlets.Identity.User
{
    [Cmdlet("Remove", "TfsUser", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Licensing.AccountEntitlement))]
    public partial class RemoveUser: CmdletBase
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