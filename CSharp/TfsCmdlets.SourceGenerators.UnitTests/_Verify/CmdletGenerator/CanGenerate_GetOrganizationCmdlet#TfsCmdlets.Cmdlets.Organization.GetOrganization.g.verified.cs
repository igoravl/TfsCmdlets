//HintName: TfsCmdlets.Cmdlets.Organization.GetOrganization.g.cs
namespace TfsCmdlets.Cmdlets.Organization
{
    [Cmdlet("Get", "TfsOrganization", DefaultParameterSetName = "Get by organization")]
    [OutputType(typeof(Microsoft.TeamFoundation.Client.TfsTeamProjectCollection))]
    public partial class GetOrganization: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(ParameterSetName="Get by organization", ValueFromPipeline=true)]
        [Parameter(ParameterSetName="Cached credentials", ValueFromPipeline=true)]
        [Parameter(ParameterSetName="User name and password", ValueFromPipeline=true)]
        [Parameter(ParameterSetName="Credential object", ValueFromPipeline=true)]
        [Parameter(ParameterSetName="Personal Access Token", ValueFromPipeline=true)]
        [Parameter(ParameterSetName="Prompt for credential", ValueFromPipeline=true)]
        public object Server { get; set; }
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
        protected override string CommandName => "GetTeamProjectCollection";
    }
}