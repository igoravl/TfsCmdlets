/*
.SYNOPSIS
Disconnects from the currently connected team project.

.DESCRIPTION
The Disconnect-TfsTeamProject cmdlet removes the global variable set by Connect-TfsTeamProject . Therefore, cmdlets relying on a "default project" as provided by "Get-TfsTeamProject -Current" will no longer work after a call to this cmdlet, unless their -Project argument is provided or a new call to Connect-TfsTeamProject is made.

.EXAMPLE
Disconnect-TfsTeamProject
Disconnects from the currently connected TFS team project

*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Connection
{
    [Cmdlet(VerbsCommunications.Disconnect, "TeamProject")]
    public class DisconnectTeamProject : BaseCmdlet
    {
        protected override void EndProcessing()
        {
            CurrentConnections.Set(
                CurrentConnections.Server,
                CurrentConnections.Collection,
                null
            );
        }
    }
}