//HintName: TfsCmdlets.Cmdlets.Credential.NewCredential.g.cs
namespace TfsCmdlets.Cmdlets.Credential
{
    [Cmdlet("New", "TfsCredential", SupportsShouldProcess = true, DefaultParameterSetName = "Cached credentials")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Common.VssCredentials))]
    public partial class NewCredential: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_CACHED_CREDENTIAL
        /// </summary>
        [Parameter(ParameterSetName = "Cached credentials", Mandatory = true)]
        public SwitchParameter Cached { get; set; }
        /// <summary>
        /// HELP_PARAM_USER_NAME
        /// </summary>
        [Parameter(ParameterSetName = "User name and password", Mandatory = true)]
        public string UserName { get; set; }
        /// <summary>
        /// HELP_PARAM_PASSWORD
        /// </summary>
        [Parameter(ParameterSetName = "User name and password", Mandatory = true)]
        public System.Security.SecureString Password { get; set; }
        /// <summary>
        /// HELP_PARAM_CREDENTIAL
        /// </summary>
        [Parameter(ParameterSetName = "Credential object", Mandatory = true)]
        [ValidateNotNull()]
        public object Credential { get; set; }
        /// <summary>
        /// HELP_PARAM_PERSONAL_ACCESS_TOKEN
        /// </summary>
        [Parameter(ParameterSetName = "Personal Access Token", Mandatory = true)]
        [Alias("Pat")]
        public string PersonalAccessToken { get; set; }
        /// <summary>
        /// HELP_PARAM_INTERACTIVE
        /// </summary>
        [Parameter(ParameterSetName = "Prompt for credential")]
        public SwitchParameter Interactive { get; set; }
        protected override string CommandName => "GetCredential";
        protected override bool ReturnsValue => true;
    }
}