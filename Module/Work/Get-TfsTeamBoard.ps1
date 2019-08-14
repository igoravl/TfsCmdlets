#define ITEM_TYPE Microsoft.TeamFoundation.Work.WebApi.Board
Function Get-TfsTeamBoard
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        # Specifies the board name(s). Wildcards accepted
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias('Name')]
        [object]
        $Board = '*',

        [Parameter()]
        [switch]
        $SkipDetails,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Team,

        [Parameter()]
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
        CHECK_ITEM($Board)

        GET_TEAM($t,$tp,$tpc)

        GET_CLIENT('Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient')

        $ctx = New-Object 'Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext' -ArgumentList $tp.Name, $t.Name

        _Log "Getting boards matching '$Board' in team '$($t.Name)'"

        CALL_ASYNC($client.GetBoardsAsync($ctx),'Error retrieving team boards')

        $boardRefs = $result | Where-Object Name -like $Board

        _Log "Found $($boardRefs.Count) boards matching '$Board' in team '$($t.Name)'"

        if($SkipDetails.IsPresent)
        {
            _Log "SkipDetails switch is present. Returning board references without details"
            return $boardRefs
        }

        foreach($b in $boardRefs)
        {
            _Log "Fetching details for board '$($b.Name)'"

            CALL_ASYNC($client.GetBoardAsync($ctx, $b.Id),"Error fetching board data")
            Write-Output $result
        }
    }
}
