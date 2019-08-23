#define ITEM_TYPE Microsoft.TeamFoundation.Work.WebApi.BoardCardRuleSettings
Function Get-TfsTeamBoardCardRuleSettings
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter()]
        [SupportsWildcards()]
        [object]
        $Board = '*',

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
        if($Board -is [Microsoft.TeamFoundation.Work.WebApi.Board])
        {
            $boards = @($Board.Name)
            $Team = ([uri] $b.Links.Links.team.Href).Segments[-1]
            $Project = ([uri] $b.Links.Links.project.Href).Segments[-1]

            _Log "Getting card rules for board $($Board.Name) in team $Team"
        }
        elseif ($Board.ToString().Contains('*'))
        {
            _Log "Getting card rules for boards matching '$Board' in team $Team"

            $boards = (Get-TfsTeamBoard -Board $Board -SkipDetails -Team $Team -Project $Project -Collection $Collection).Name

            _Log "$($boards.Count) board(s) found matching '$Board'"
        }
        else
        {
            _Log "Getting card rules for board $($Board.Name) in team $Team"

            $boards = @($Board)
        }

        GET_TEAM($t,$tp,$tpc)
        GET_CLIENT('Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient')

        foreach($boardName in $boards)
        {
            $ctx = New-Object 'Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext' -ArgumentList $tp.Name, $t.Name

            _Log "Fetching card rule settings for board $boardName"
            
            CALL_ASYNC($client.GetBoardCardRuleSettingsAsync($ctx,$boardName),"Error retrieving card rule settings for board '$Board'")

            Write-Output $result `
                | Add-Member -Name 'Team' -MemberType NoteProperty -Value $t.Name -PassThru `
                | Add-Member -Name 'Project' -MemberType NoteProperty -Value $tp.Name -PassThru
        }
    }
}
