<#

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Rename-TfsTeam
{
    [OutputType([Microsoft.TeamFoundation.Client.TeamFoundationTeam])]
    param
    (
        [Parameter(Position=0, ValueFromPipeline=$true)]
        [Alias("Name")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Client.TeamFoundationTeam])})] 
        [SupportsWildcards()]
        [object]
        $Team = '*',

        [Parameter()]
        [string]
        $NewName,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        Set-TfsTeam -Team $Team -NewName $NewName -Project $Project -Collection $Collection
    }
}
