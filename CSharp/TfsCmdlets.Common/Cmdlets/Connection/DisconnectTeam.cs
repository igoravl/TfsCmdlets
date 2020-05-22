/*
.SYNOPSIS
Disconnects from the currently connected team.

.DESCRIPTION
The Disconnect-TfsTeamProject cmdlet removes the global variable set by Connect-TfsTeam. Therefore, cmdlets relying on a "default team" as provided by "Get-TfsTeam -Current" will no longer work after a call to this cmdlet, unless their -Team argument is provided or a new call to Connect-TfsTeam is made.

.EXAMPLE
Disconnect-TfsTeam
Disconnects from the currently connected TFS team

*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Connection
{
    [Cmdlet(VerbsCommunications.Disconnect, "Team")]
    public class DisconnectTeam : BaseCmdlet
    {
        protected override void EndProcessing()
        {
            CurrentConnections.Set(
                CurrentConnections.Server,
                CurrentConnections.Collection,
                CurrentConnections.Project,
                null
            );
        }
    }
}
