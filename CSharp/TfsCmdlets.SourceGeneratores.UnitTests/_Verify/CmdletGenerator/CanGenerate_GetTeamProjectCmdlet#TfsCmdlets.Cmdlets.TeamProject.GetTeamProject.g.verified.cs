//HintName: TfsCmdlets.Cmdlets.TeamProject.GetTeamProject.g.cs
namespace TfsCmdlets.Cmdlets.TeamProject
{
    [Cmdlet("Get", "TfsTeamProject", DefaultParameterSetName = "Get by project")]
    [OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.TeamProject))]
    public partial class GetTeamProject: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Alias("Organization")]
        [Parameter(ParameterSetName="Get by project", ValueFromPipeline=true)]
        [Parameter(ParameterSetName="Cached credentials", ValueFromPipeline=true)]
        [Parameter(ParameterSetName="User name and password", ValueFromPipeline=true)]
        [Parameter(ParameterSetName="Credential object", ValueFromPipeline=true)]
        [Parameter(ParameterSetName="Personal Access Token", ValueFromPipeline=true)]
        [Parameter(ParameterSetName="Prompt for credential", ValueFromPipeline=true)]
        public object Collection { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(ParameterSetName="Get by project")]
        [Parameter(ParameterSetName="Cached credentials")]
        [Parameter(ParameterSetName="User name and password")]
        [Parameter(ParameterSetName="Credential object")]
        [Parameter(ParameterSetName="Personal Access Token")]
        [Parameter(ParameterSetName="Prompt for credential")]
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
    }
}