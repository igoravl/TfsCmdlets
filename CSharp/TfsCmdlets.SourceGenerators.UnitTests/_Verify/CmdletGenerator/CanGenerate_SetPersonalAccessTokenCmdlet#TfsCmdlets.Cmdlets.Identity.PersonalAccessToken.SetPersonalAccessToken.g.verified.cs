//HintName: TfsCmdlets.Cmdlets.Identity.PersonalAccessToken.SetPersonalAccessToken.g.cs
namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    [Cmdlet("Set", "TfsPersonalAccessToken", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken))]
    public partial class SetPersonalAccessToken: CmdletBase
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