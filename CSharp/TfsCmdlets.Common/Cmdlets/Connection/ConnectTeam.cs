/*
.SYNOPSIS
Connects to a team.

.DESCRIPTION

.PARAMETER Project
Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. If omitted, it defaults to the connection opened by Connect-TfsTeamProject (if any). 

For more details, see the Get-TfsTeamProject cmdlet.

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Credential
Specifies a user account that has permission to perform this action. The default is the cached credential of the user under which the PowerShell process is being run - in most cases that corresponds to the user currently logged in. To provide a user name and password, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its WriteObject(to this argument. For more information, refer to https://msdn.microsoft.com/en-us/library/microsoft.teamfoundation.client.tfsclientcredentials.aspx); return;

.PARAMETER Interactive
Prompts for user credentials. Can be used for any Azure DevOps account - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build).

.PARAMETER Passthru
Returns the results of the command. By default, this cmdlet does not generate any output. 

.INPUTS
Microsoft.TeamFoundation.Core.WebApi.WebApiTeam
System.String
*/

using System.Management.Automation;
using System.Security;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Connection
{
    [Cmdlet(VerbsCommunications.Connect, "Team", DefaultParameterSetName = "Explicit credentials")]
    [OutputType(typeof(WebApiTeam))]
    public class ConnectTeam : BaseCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull()]
        public object Team { get; set; }

        [Parameter()]
        public object Project { get; set; }

        [Parameter()]
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