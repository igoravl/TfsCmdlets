using System.Management.Automation;
using System.Security;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Connection
{
    /// <summary>
    /// Connects to a team.
    /// </summary>
    [Cmdlet(VerbsCommunications.Connect, "Team", DefaultParameterSetName = "Explicit credentials")]
    [OutputType(typeof(WebApiTeam))]
    public class ConnectTeam : BaseCmdlet
    {
        /// <summary>
        ///   Specifies the name of the Team, its ID (a GUID), or a 
        ///   Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. For more details, 
        ///   see the Get-TfsTeam cmdlet.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull()]
        public object Team { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

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
        /// HELP_PARAM_CREDENTIAL_OBJECT
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
        /// HELP_PARAM_CREDENTIAL_INTERACTIVE
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

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            var (tpc, tp, t) = this.GetCollectionProjectAndTeam();

            CurrentConnections.Set(tpc.ConfigurationServer, tpc, tp, t);

            // TODO: 
            //this.Log($"Adding '{tp.Name} to the MRU list");
            //_SetMru "Server" - Value(srv.Uri)
            //_SetMru "Collection" - Value(tpc.Uri)
            //_SetMru "Project" - Value(tp.Name)

            this.Log($"Connected to '{t.Name}'");

            if (Passthru)
            {
                WriteObject(tp);
            }
        }
    }
}