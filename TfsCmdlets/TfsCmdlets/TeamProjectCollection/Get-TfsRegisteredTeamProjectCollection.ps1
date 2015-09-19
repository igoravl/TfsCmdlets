<#
.SYNOPSIS

.DESCRIPTION

.PARAMETER computername

.PARAMETER filePath

.EXAMPLE

.EXAMPLE

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
