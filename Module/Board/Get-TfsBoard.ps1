Function Get-TfsBoard
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
        REQUIRES(Microsoft.VisualStudio.Services.WebApi)
        REQUIRES(Microsoft.TeamFoundation.Core.WebApi)
        REQUIRES(Microsoft.TeamFoundation.Work.WebApi)
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
        $client = Get-RestClient 'Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient' -Collection $tpc
        $ctx = New-Object 'Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext' -ArgumentList $tp.Name, $t.Name
        $boards = $client.GetBoardsAsync($ctx).Result | Where-Object Name -like $Board

        foreach($b in $boards)
        {
            Write-Output $client.GetBoardAsync($ctx, $b.Id).Result
        }
    }
}