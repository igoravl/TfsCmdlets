//HintName: TfsCmdlets.Cmdlets.Identity.User.NewUser.g.cs
namespace TfsCmdlets.Cmdlets.Identity.User
{
    [Cmdlet("New", "TfsUser", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Licensing.AccountEntitlement))]
    public partial class NewUser: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
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