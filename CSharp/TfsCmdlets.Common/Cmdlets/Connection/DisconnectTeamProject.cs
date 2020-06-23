using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Connection
{
    /// <summary>
    /// Disconnects from the currently connected team project.
    /// </summary>
    /// <remarks>
    /// The Disconnect-TfsTeamProject cmdlet removes the connection previously set by its 
    /// counterpart Connect-TfsTeamProject. Therefore, cmdlets relying on a "default team project" 
    /// as provided by "Get-TfsTeamProject -Current" will no longer work after a call to 
    /// this cmdlet, unless their -Project argument is provided or a new call to 
    /// Connect-TfsTeamProject is made.
    /// </remarks>
    [Cmdlet(VerbsCommunications.Disconnect, "TfsTeamProject")]
    public class DisconnectTeamProject : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            CurrentConnections.Set(
                CurrentConnections.Server,
                CurrentConnections.Collection,
                null
            );
        }
    }
}