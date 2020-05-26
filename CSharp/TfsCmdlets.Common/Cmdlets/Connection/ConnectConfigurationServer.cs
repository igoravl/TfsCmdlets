/*
.SYNOPSIS
Connects to a configuration server.

.PARAMETER Server
#Specifies either a URL/name of the Team Foundation Server to connect to, or a previously initialized TfsConfigurationServer object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/] 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS.nnTo connect to a Team Foundation Server instance by using its name, it must have been previously registered.

.PARAMETER Credential
Specifies a user account that has permission to perform this action. The default is the cached credential of the user under which the PowerShell process is being run - in most cases that corresponds to the user currently logged in. To provide a user name and password, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its WriteObject(to this argument. For more information, refer to https://msdn.microsoft.com/en-us/library/microsoft.teamfoundation.client.tfsclientcredentials.aspx); return;

.PARAMETER Interactive
Prompts for user credentials. Can be used for any Azure DevOps account - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build).

.PARAMETER Passthru
Returns the results of the command. By default, this cmdlet does not generate any output. 

.DESCRIPTION
The Connect-TfsConfigurationServer function connects to a TFS configuration server. Functions that operate on a server level (as opposed to those operation on a team project collection level) will use by default a connection opened by this function.

.NOTES
A TFS Configuration Server represents the server that is running Team Foundation Server. On a database level, it is represented by the Tfs_Configuration database. Operations that should be performed on a server level (such as setting server-level permissions) require a connection to a TFS configuration server. Internally, this connection is represented by an instance of the Microsoft.TeamFoundation.Client.TfsConfigurationServer class and is kept in a PowerShell global variable caled TfsServerConnection.

.EXAMPLE
Connect-TfsConfigurationServer -Server http://vsalm:8080/tfs
Connects to the TFS server specified by the URL in the Server argument

.EXAMPLE
Connect-TfsConfigurationServer -Server vsalm
Connects to a previously registered TFS server by its user-defined name "vsalm". For more information, see Get-TfsRegisteredConfigurationServer

.INPUTS
Microsoft.TeamFoundation.Client.TfsConfigurationServer
System.String
System.Uri

.LINK
Microsoft.TeamFoundation.Client.TfsConfigurationServer

.LINK
https://blogs.msdn.microsoft.com/taylaf/2010/02/23/introducing-the-tfsconnection-tfsconfigurationserver-and-tfsteamprojectcollection-classes/
*/

using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Connection
{
    [Cmdlet(VerbsCommunications.Connect, "ConfigurationServer", DefaultParameterSetName="Explicit credentials")]
	[OutputType(typeof(VssConnection))]
    public class ConnectConfigurationServer: BaseCmdlet
    {
		[Parameter(Mandatory=true, Position=0, ValueFromPipeline=true)]
		[ValidateNotNull]
		public object Server { get; set; }
	
		[Parameter(ParameterSetName="Explicit credentials")]
		public object Credential { get; set; }

		[Parameter(ParameterSetName="Prompt for credentials", Mandatory=true)]
		public SwitchParameter Interactive { get; set; }

		[Parameter]
		public SwitchParameter Passthru { get; set; }

        protected override void ProcessRecord()
        {
            var srv = this.GetServer();
            srv.Connect();

            //var client = tpc.GetClient<ProjectCollectionHttpClient>();
            //var col = client.GetProjectCollection(tpc.ServerId.ToString()).Result;

            CurrentConnections.Set(srv);

            this.Log($"Connected to {srv.Uri}, ID {srv.ServerId}, as {srv.AuthorizedIdentity.DisplayName}");

            if (Passthru)
            {
                WriteObject(srv);
            }
        }
	}
}
