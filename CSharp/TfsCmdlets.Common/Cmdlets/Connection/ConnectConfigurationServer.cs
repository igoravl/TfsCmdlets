using System.Management.Automation;
using System.Security;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Connection
{
    /// <summary>
    ///  Connects to a configuration server.
    /// </summary>
    /// <remarks>
    ///  A TFS Configuration Server represents the server that is running Team Foundation Server. On a database level, 
    ///  it is represented by the Tfs_Configuration database. Operations that should be performed on a server level 
    ///  (such as setting server-level permissions) require a connection to a TFS configuration server. 
    ///  Internally, this connection is represented by an instance of the Microsoft.TeamFoundation.Client.TfsConfigurationServer. 
    ///  NOTE: Currently it is only supported in Windows PowerShell.
    /// </remarks>
    /// <example>
    ///   <code>Connect-TfsConfigurationServer -Server http://vsalm:8080/tfs</code>
    ///   <para>Connects to the TFS server specified by the URL in the Server argument</para>
    /// </example>
    /// <example>
    ///   <code>Connect-TfsConfigurationServer -Server vsalm</code>
    ///   <para>Connects to a previously registered TFS server by its user-defined name "vsalm". For more information, see Get-TfsRegisteredConfigurationServer</para>
    /// </example>
    /// <para type="input">Microsoft.TeamFoundation.Client.TfsConfigurationServer</para>
    /// <para type="input">System.String</para>
    /// <para type="input">System.Uri</para>

    [Cmdlet(VerbsCommunications.Connect, "TfsConfigurationServer", DefaultParameterSetName="Prompt for credential")]
    [DesktopOnly]
    public partial class ConnectConfigurationServer: CmdletBase
    {
        /// <summary>
        ///   Specifies either a URL/name of the Team Foundation Server to connect to, or a previously 
        ///   initialized TfsConfigurationServer object. When using a URL, it must be fully qualified. 
        ///   To connect to a Team Foundation Server instance by using its name, it must have been 
        ///   previously registered.
        /// </summary>
		[Parameter(Mandatory=true, Position=0, ValueFromPipeline=true)]
		[ValidateNotNull]
		public object Server { get; set; }

        /// <summary>
        /// HELP_PARAM_CACHED_CREDENTIALS
        /// </summary>
        [Parameter(ParameterSetName = "Cached credentials", Mandatory = true)]
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
        [ValidateNotNull]
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
        [Parameter(ParameterSetName = "Prompt for credential")]
        public SwitchParameter Interactive { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter]
        public SwitchParameter Passthru { get; set; }
	}
}
