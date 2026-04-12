//HintName: TfsCmdlets.Cmdlets.Identity.PersonalAccessToken.UpdatePersonalAccessToken.g.cs
namespace TfsCmdlets.Cmdlets.Identity.PersonalAccessToken
{
    [Cmdlet("Update", "TfsPersonalAccessToken", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.VisualStudio.Services.DelegatedAuthorization.PatToken))]
    public partial class UpdatePersonalAccessToken: CmdletBase
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
        protected override bool ReturnsValue => true;
    }
}