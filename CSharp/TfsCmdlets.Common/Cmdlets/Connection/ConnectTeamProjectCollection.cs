/*
.SYNOPSIS
Connects to a team project collection. 

.DESCRIPTION
The Connect-TfsTeamProjectCollection cmdlet "connects" (initializes a Microsoft.TeamFoundation.Client.TfsTeamProjectCollection object) to a TFS Team Project Collection. That connection is subsequently kept in a global variable to be later reused until it"s closed by a call to Disconnect-TfsTeamProjectCollection.
Most cmdlets in the TfsCmdlets module require a TfsTeamProjectCollection object to be provided via their -Collection argument in order to access a TFS instance. Those cmdlets will use the connection opened by Connect-TfsTeamProjectCollection as their "default connection". In other words, TFS cmdlets (e.g. New-TfsWorkItem) that have a -Collection argument will use the connection provided by Connect-TfsTeamProjectCollection by default.

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Server
Specifies either a URL/name of the Team Foundation Server to connect to, or a previously initialized TfsConfigurationServer object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/] 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS.nnTo connect to a Team Foundation Server instance by using its name, it must have been previously registered.

.PARAMETER Credential
Specifies a user account that has permission to perform this action. The default is the cached credential of the user under which the PowerShell process is being run - in most cases that corresponds to the user currently logged in. To provide a user name and password, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its WriteObject(to this argument. For more information, refer to https://msdn.microsoft.com/en-us/library/microsoft.teamfoundation.client.tfsclientcredentials.aspx); return;

.PARAMETER Interactive
Prompts for user credentials. Can be used for any Azure DevOps account - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build).

.PARAMETER Passthru
Returns the results of the command. By default, this cmdlet does not generate any output. 

.EXAMPLE
Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection
Connects to a collection called "DefaultCollection" in a TF server called "tfs" using the cached credentials of the logged-on user

.EXAMPLE
Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection -Interactive
Connects to a collection called "DefaultCollection" in a Team Foundation server called "tfs", firstly prompting the user for credentials (it ignores the cached credentials for the currently logged-in user). It"s equivalent to the command:

PS> Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection -Credential (Get-TfsCredential -Interactive)

.LINK
Get-TfsTeamProjectCollection

.LINK
https://msdn.microsoft.com/en-us/library/microsoft.teamfoundation.client.tfsteamprojectcollection.aspx

.INPUTS
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri
*/

using System.Management.Automation;
using System.Security;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Connection
{
    [Cmdlet(VerbsCommunications.Connect, "TeamProjectCollection", DefaultParameterSetName = "Prompt for credential")]
    [OutputType(typeof(VssConnection))]
    public class ConnectTeamProjectCollection : BaseCmdlet
    {

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull]
        public object Collection { get; set; }

        [Parameter(ParameterSetName = "Cached credentials", Mandatory = true)]
        public SwitchParameter Cached { get; set; }

        [Parameter(ParameterSetName = "User name and password", Mandatory = true, Position = 1)]
        public string UserName { get; set; }

        [Parameter(ParameterSetName = "User name and password", Position = 2)]
        public SecureString Password { get; set; }

        [Parameter(ParameterSetName = "Credential object", Mandatory = true)]
        [ValidateNotNull]
        public object Credential { get; set; }

        [Parameter(ParameterSetName = "Personal Access Token", Mandatory = true)]
        [Alias("Pat", "PersonalAccessToken")]
        public string AccessToken { get; set; }

        [Parameter(ParameterSetName = "Prompt for credential")]
        public SwitchParameter Interactive { get; set; }

        [Parameter]
        public object Server { get; set; }

        [Parameter]
        public SwitchParameter Passthru { get; set; }

        protected override void ProcessRecord()
        {
            var tpc = this.GetCollection();
            tpc.Connect();

            //var client = tpc.GetClient<ProjectCollectionHttpClient>();
            //var col = client.GetProjectCollection(tpc.ServerId.ToString()).Result;

            var srv = tpc.ConfigurationServer;

            CurrentConnections.Set(srv, tpc);

            this.Log($"Connected to {tpc.Uri}, ID {tpc.ServerId}, as '{tpc.AuthorizedIdentity.DisplayName}'");

            if (Passthru)
            {
                WriteObject(tpc);
            }
        }
    }
}