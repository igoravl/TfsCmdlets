<#

.SYNOPSIS
    Gets an Team Foundation Server REST API client object.

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Get-TfsHttpClient
{
    Param
    (
        [Parameter(Mandatory=$true, Position=0)]
        [string]
        $Type,

        [Parameter()]
        [object] 
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection -Collection $Collection

        return Invoke-GenericMethod -InputObject $tpc -MethodName GetClient -GenericType $Type
    }
}