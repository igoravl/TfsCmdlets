using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Disconnects from the currently connected team.
    /// </summary>
    /// <remarks>
    /// The Disconnect-TfsTeam cmdlet removes the connection previously set by its 
    /// counterpart Connect-TfsTeam. Therefore, cmdlets relying on a "default team" 
    /// as provided by "Get-TfsTeam -Current" will no longer work after a call to 
    /// this cmdlet, unless their -Team argument is provided or a new call to 
    /// Connect-TfsTeam is made.
    /// </remarks>
    [TfsCmdlet(CmdletScope.None)]
    partial class DisconnectTeam
    { 
    }
}