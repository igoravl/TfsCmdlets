/*
.SYNOPSIS
Disconnects from the currently connected team project collection.

.DESCRIPTION
The Disconnect-TfsTeamProjectCollection cmdlet removes the global variable set by Connect-TfsTeamProjectCollection. Therefore, cmdlets relying on a "default collection" as provided by "Get-TfsTeamProjectCollection -Current" will no longer work after a call to this cmdlet, unless their -Collection argument is provided or a new call to Connect-TfsTeamProjectCollection is made.

.EXAMPLE
Disconnect-TfsTeamProjectCollection
Disconnects from the currently connected TFS team project collection

*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Connection
{
    [Cmdlet(VerbsCommunications.Disconnect, "TeamProjectCollection")]
    public class DisconnectTeamProjectCollection: PSCmdlet
    {
/*

    protected override void EndProcessing()
	{
        Disconnect-TfsTeamProject

        [TfsCmdlets.CurrentConnections]::Collection = null
	}
}
*/
}
}
