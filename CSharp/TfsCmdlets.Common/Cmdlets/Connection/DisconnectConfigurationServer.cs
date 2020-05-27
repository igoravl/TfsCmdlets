/*
.SYNOPSIS
Disconnects from the currently connected configuration server.

.DESCRIPTION
The Disconnect-TfsConfigurationServer cmdlet removes the global variable set by Connect-TfsConfigurationServer. Therefore, cmdlets relying on a "default server" as provided by "Get-TfsConfigurationServer -Current" will no longer work after a call to this cmdlet, unless their -Server argument is provided or a new call to Connect-TfsConfigurationServer is made.

.EXAMPLE
Disconnect-TfsConfigurationServer
Disconnects from the currently connected TFS configuration server

*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Connection
{
    [Cmdlet(VerbsCommunications.Disconnect, "ConfigurationServer")]
    public class DisconnectConfigurationServer : BaseCmdlet
    {
        protected override void EndProcessing()
        {
            CurrentConnections.Reset();
        }
    }
}