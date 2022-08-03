using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ConfigServer
{
    /// <summary>
    /// Disconnects from the currently connected configuration server.
    /// </summary>
    /// <remarks>
    /// The Disconnect-TfsConfigurationServer cmdlet removes the connection previously set by its 
    /// counterpart Connect-TfsConfigurationServer. Therefore, cmdlets relying on a "default server" 
    /// as provided by "Get-TfsConfigurationServer -Current" will no longer work after a call to this cmdlet, 
    /// unless their -Server argument is provided or a new call to Connect-TfsConfigurationServer is made.
    /// </remarks>
    [TfsCmdlet(CmdletScope.None)]
    partial class DisconnectConfigurationServer
    {
    }
}