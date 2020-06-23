using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Connection
{
    /// <summary>
    /// Disconnects from the currently connected TFS team project collection or Azure DevOps organization.
    /// </summary>
    /// <remarks>
    /// The Disconnect-TfsTeamProjectCollection cmdlet removes the connection previously set by its 
    /// counterpart Connect-TfsTeamProjectCollection. Therefore, cmdlets relying on a "default collection" 
    /// as provided by "Get-TfsTeamProjectCollection -Current" will no longer work after a call to 
    /// this cmdlet, unless their -Collection argument is provided or a new call to 
    /// Connect-TfsTeam is made.
    /// </remarks>
    [Cmdlet(VerbsCommunications.Disconnect, "TfsTeamProjectCollection")]
    public class DisconnectTeamProjectCollection : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            CurrentConnections.Set(
                CurrentConnections.Server,
                null
            );
        }
    }
}