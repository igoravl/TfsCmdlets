using System.Management.Automation;
using System.Security;
using Microsoft.VisualStudio.Services.Client;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Connection
{
    /// <summary>
    /// Provides credentials to use when you connect to a Team Foundation Server or Azure DevOps organization.
    /// </summary>
    [Cmdlet("Get", "Credential", DefaultParameterSetName = "Cached credentials")]
    [OutputType("Microsoft.VisualStudio.Services.Client.VssClientCredentials")]
    public class GetCredential : BaseCmdlet
    {
        /// <summary>
        /// HELP_PARAM_CACHED_CREDENTIALS
        /// </summary>
        [Parameter(ParameterSetName = "Cached credentials")]
        public SwitchParameter Cached { get; set; }

        /// <summary>
        /// HELP_PARAM_USER_NAME
        /// </summary>
        [Parameter(ParameterSetName = "User name and password", Mandatory = true, Position = 1)]
        public string UserName { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSWORD
        /// </summary>
        [Parameter(ParameterSetName = "User name and password", Position = 2)]
        public SecureString Password { get; set; }

        /// <summary>
        /// HELP_PARAM_CREDENTIAL
        /// </summary>
        [Parameter(ParameterSetName = "Credential object", Mandatory = true)]
        [AllowNull]
        public object Credential { get; set; }

        /// <summary>
        /// HELP_PARAM_PERSONAL_ACCESS_TOKEN
        /// </summary>
        [Parameter(ParameterSetName = "Personal Access Token", Mandatory = true)]
        [Alias("Pat", "PersonalAccessToken")]
        public string AccessToken { get; set; }

        /// <summary>
        /// HELP_PARAM_INTERACTIVE
        /// </summary>
        [Parameter(ParameterSetName = "Prompt for credential", Mandatory = true)]
        public SwitchParameter Interactive { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            WriteObject(this.GetInstanceOf<VssClientCredentials>());
        }
    }
}