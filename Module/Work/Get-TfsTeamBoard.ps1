Function Get-TfsTeamBoard
{
    [CmdletBinding()]
    [OutputType('Microsoft.TeamFoundation.Work.WebApi.Board')]
    Param
    (
        # Specifies the board name(s). Wildcards accepted
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias('Name')]
        [object]
        $Board = '*',

        [Parameter()]
        [object]
        $Team,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Begin
    {
        _ImportRequiredAssembly -AssemblyName 'Microsoft.VisualStudio.Services.WebApi'
        _ImportRequiredAssembly -AssemblyName 'Microsoft.TeamFoundation.Core.WebApi'
        _ImportRequiredAssembly -AssemblyName 'Microsoft.TeamFoundation.Work.WebApi'
    }

    Process
    {
        if($Board -is [Microsoft.TeamFoundation.Work.WebApi.Board])
        {
            return $Board
        }

        $t = Get-TfsTeam -Team $Team -Project $Project -Collection $Collection

        if (-not $t)
        {
			throw "Invalid or non-existent team '$Team'"
        }

        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection

        if (-not $tp)
        {
			throw "Invalid or non-existent team project '$Project'"
        }

        $tpc = $tp.Store.TeamProjectCollection
        $client = _GetRestClient 'Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient' -Collection $tpc
        $ctx = New-Object 'Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext' -ArgumentList $tp.Name, $t.Name

        CALL_ASYNC($client.GetBoardsAsync($ctx),'Error retrieving team boards')

        $boards = $result | Where-Object Name -like $Board

        foreach($b in $boards)
        {
            Write-Output $client.GetBoardAsync($ctx, $b.Id).Result
        }
    }
}
