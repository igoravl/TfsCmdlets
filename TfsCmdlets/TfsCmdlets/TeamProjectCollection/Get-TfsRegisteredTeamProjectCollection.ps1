<#

.SYNOPSIS
    Gets one or more Team Project Collection addresses registered in the current computer.

#>
Function Get-TfsRegisteredTeamProjectCollection
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.Client.RegisteredProjectCollection[]])]
    Param
    (
        [Parameter(Position=0, ValueFromPipeline=$true)]
        [SupportsWildcards()]
        [string]
        $Name = "*"
    )

    Process
    {
        return [Microsoft.TeamFoundation.Client.RegisteredTfsConnections]::GetProjectCollections() | ? DisplayName -Like $Name
    }
}
