/*
.SYNOPSIS
Gets information about a configuration server.

.PARAMETER Server
Specifies either a URL/name of the Team Foundation Server to connect to, or a previously initialized TfsConfigurationServer object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/] 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS.nnTo connect to a Team Foundation Server instance by using its name, it must have been previously registered.

.PARAMETER Current
Returns the configuration server specified in the last call to Connect-TfsConfigurationServer (i.e. the "current" configuration server)

.PARAMETER Credential
Specifies a user account that has permission to perform this action. The default is the cached credential of the user under which the PowerShell process is being run - in most cases that corresponds to the user currently logged in. To provide a user name and password, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its WriteObject(to this argument. For more information, refer to https://msdn.microsoft.com/en-us/library/microsoft.teamfoundation.client.tfsclientcredentials.aspx); return;

.INPUTS
Microsoft.TeamFoundation.Client.TfsConfigurationServer
System.String
System.Uri
*/

using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    [Cmdlet(VerbsCommon.Get, "ConfigurationServer", DefaultParameterSetName = "Get by server")]
    //[OutputType(typeof(Microsoft.TeamFoundation.Client.TfsConfigurationServer))]
    public class GetConfigurationServer : BaseCmdlet
    {
        [Parameter(Position = 0, ParameterSetName = "Get by server", Mandatory = true)]
        [AllowNull]
        public object Server { get; set; }

        [Parameter(Position = 0, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }

        [Parameter(Position = 1, ParameterSetName = "Get by server")]
        public object Credential { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(this.GetServer());
        }
    }
}
