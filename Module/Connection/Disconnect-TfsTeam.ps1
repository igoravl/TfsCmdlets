<#
.SYNOPSIS
Disconnects from the currently connected team.

.DESCRIPTION
The Disconnect-TfsTeamProject cmdlet removes the global variable set by Connect-TfsTeam. Therefore, cmdlets relying on a "default team" as provided by "Get-TfsTeam -Current" will no longer work after a call to this cmdlet, unless their -Team argument is provided or a new call to Connect-TfsTeam is made.

.EXAMPLE
Disconnect-TfsTeam
Disconnects from the currently connected TFS team

#>
Function Disconnect-TfsTeam
{
    [CmdletBinding()]
    Param
    (
    )

    End
	{
        [TfsCmdlets.CurrentConnections]::Team = $null
	}
}
