/*
.SYNOPSIS
Gets information about one or more team project collections.

.DESCRIPTION
The Get-TfsTeamProjectCollection cmdlets gets one or more Team Project Collection objects (an instance of Microsoft.TeamFoundation.Client.TfsTeamProjectCollection) from a TFS instance. 
Team Project Collection objects can either be obtained by providing a fully-qualified URL to the collection or by collection name (in which case a TFS Configuration Server object is required).

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Server
Specifies either a URL/name of the Team Foundation Server configuration server (the "root" of a TFS installation) to connect to, or a previously initialized Microsoft.TeamFoundation.Client.TfsConfigurationServer object.

.PARAMETER Current
Returns the team project collection specified in the last call to Connect-TfsTeamProjectCollection (i.e. the "current" project collection)

.PARAMETER Credential
Specifies a user account that has permission to perform this action. The default is the cached credential of the user under which the PowerShell process is being run - in most cases that corresponds to the user currently logged in. To provide a user name and password, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its WriteObject(to this argument. For more information, refer to https://msdn.microsoft.com/en-us/library/microsoft.teamfoundation.client.tfsclientcredentials.aspx); return;

.EXAMPLE
Get-TfsTeamProjectCollection http://

.INPUTS
Microsoft.TeamFoundation.Client.TfsConfigurationServer
System.String
System.Uri

.NOTES
Cmdlets in the TfsCmdlets module that operate on a collection level require a TfsConfigurationServer object to be provided via the -Server argument. If absent, it will default to the connection opened by Connect-TfsConfigurationServer.
*/

using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    [Cmdlet(VerbsCommon.Get, "TfsTeamProjectCollection", DefaultParameterSetName = "Get by collection")]
    [OutputType(typeof(VssConnection))]
    public class GetTeamProjectCollection : BaseCmdlet
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by collection")]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(ValueFromPipeline = true, ParameterSetName = "Get by collection")]
        public object Server { get; set; }

        /// <summary>
        /// HELP_PARAM_CREDENTIAL
        /// </summary>
        [Parameter(ParameterSetName = "Get by collection")]
        public object Credential { get; set; }

        /// <summary>
        /// Returns the team project collection specified in the last call to 
        /// Connect-TfsTeamProjectCollection (i.e. the "current" project collection)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                WriteObject(this.GetItems<TfsCmdlets.Services.Connection>(new {
                    ConnectionType = ClientScope.Collection
                }), true);
            }
            catch
            {
                if(!Current) throw;
            }
        }
    }
}