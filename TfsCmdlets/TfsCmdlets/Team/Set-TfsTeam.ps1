Function Set-TfsTeam
{
    [CmdletBinding(DefaultParameterSetName="Get by name")]
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
        [switch]
        $Default,

        [Parameter()]
        [string]
        $NewName,

        [Parameter()]
        [string]
        $Description,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $t = Get-TfsTeam -Team $Team -Project $Project -Collection $Collection
        
        if ($Project)
        {
            $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
            $tpc = $tp.Store.TeamProjectCollection
        }
        else
        {
            $tpc = Get-TfsTeamProjectCollection -Collection $Collection
        }

        $teamService = $tpc.GetService([type]'Microsoft.TeamFoundation.Client.TfsTeamService')

        if ($NewName)
        {
            $t.Name = $NewName
        }

        if ($PSBoundParameters.ContainsKey('Description'))
        {
            $t.Description = $Description
        }

        if ($Default)
        {
            $teamService.SetDefaultTeam($t)
        }

        $teamService.UpdateTeam($t)

        return $t
    }
}
