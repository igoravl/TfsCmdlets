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
        [Alias('Name')]
        [SupportsWildcards()]
        [string]
        $Collection = "*"
    )

    Process
    {
        $registeredCollections = [Microsoft.TeamFoundation.Client.RegisteredTfsConnections]::GetProjectCollections() 
        
        foreach($tpc in $registeredCollections)
        {
            $tpcName = ([uri]$tpc.Uri).Segments[-1]

            if($tpcName -like $Collection)
            {
                Write-Output $tpc
            }
        }
    }
}
