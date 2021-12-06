using System.Management.Automation;
using System.Security;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Connects to a TFS team project collection or Azure DevOps organization. 
    /// </summary>
    /// <remarks>
    /// The Connect-TfsTeamProjectCollection cmdlet connects to a TFS Team Project Collection or 
    /// Azure DevOps organization.
    /// 
    /// That connection can be later reused by other TfsCmdlets commands until it's closed 
    /// by a call to Disconnect-TfsTeamProjectCollection.
    /// </remarks>
    /// <notes>
    /// Most cmdlets in the TfsCmdlets module require a Collection object to be provided via their 
    /// -Collection argument in order to access a TFS instance. Those cmdlets will use the connection 
    /// opened by Connect-TfsTeamProjectCollection as their "default connection".
    /// 
    /// In other words, TFS cmdlets (e.g. New-TfsWorkItem) that have a -Collection argument will use the connection 
    /// provided by Connect-TfsTeamProjectCollection by default.
    /// </notes>
    /// <example>
    ///   <code>Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection</code>
    ///   <para>Connects to a collection called "DefaultCollection" in a TF server called "tfs" 
    ///         using the cached credentials of the logged-on user</para>
    /// </example>
    /// <example>
    ///   <code>Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection -Interactive</code>
    ///   <para>Connects to a collection called "DefaultCollection" in a Team Foundation server called 
    ///         "tfs", firstly prompting the user for credentials (it ignores the cached credentials for 
    ///         the currently logged-in user). It's equivalent to the command: `Connect-TfsTeamProjectCollection 
    ///         -Collection http://tfs:8080/tfs/DefaultCollection -Credential (Get-TfsCredential -Interactive)`
    ///   </para>
    /// </example>
    [Cmdlet(VerbsCommunications.Connect, "TfsTeamProjectCollection", DefaultParameterSetName = "Prompt for credential")]
    [OutputType(typeof(VssConnection))]
    partial class ConnectTeamProjectCollection
    {
        /// <summary>
        ///  Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, 
        ///  a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. 
        ///  You can also connect to an Azure DevOps Services organizations by simply providing its name 
        ///  instead of the full URL. 
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_CACHED_CREDENTIAL
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
        [Alias("Pat")]
        public string PersonalAccessToken { get; set; }

        /// <summary>
        /// HELP_PARAM_INTERACTIVE
        /// </summary>
        [Parameter(ParameterSetName = "Prompt for credential")]
        public SwitchParameter Interactive { get; set; }

        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter]
        public object Server { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter]
        public SwitchParameter Passthru { get; set; }

        // TODO

    }
}