<#
.SYNOPSIS
    Gets one or more Team Project Collection addresses registered in the current computer.

.PARAMETER Name
    Specifies the name of a registered collection. When omitted, all registered collections are returned. Wildcards are permitted.

.INPUTS
    System.String
#>
Function Get-TfsRegisteredTeamProjectCollection
{
    [CmdletBinding()]
    [OutputType('Microsoft.TeamFoundation.Client.RegisteredProjectCollection[]')]
    Param
    (
        [Parameter(Position=0, ValueFromPipeline=$true)]
        [SupportsWildcards()]
        [string]
        $Name = "*"
    )

    Process
    {
        return [Microsoft.TeamFoundation.Client.RegisteredTfsConnections]::GetProjectCollections() | Where-Object DisplayName -Like $Name
    }
}
