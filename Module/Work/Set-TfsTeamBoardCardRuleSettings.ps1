#define ITEM_TYPE Microsoft.TeamFoundation.Work.WebApi.BoardCardRuleSettings
Function Set-TfsTeamBoardCardRuleSettings
{
    [CmdletBinding(SupportsShouldProcess=$true,ConfirmImpact='Medium')]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter()]
        [object]
        $Board,

        [Parameter(ParameterSetName="Bulk set")]
        [Microsoft.TeamFoundation.Work.WebApi.BoardCardRuleSettings]
        $Rules,

        [Parameter(ParameterSetName="Set individual rules")]
        [string]
        $CardStyleRuleName,

        [Parameter(ParameterSetName="Set individual rules")]
        [string]
        $CardStyleRuleFilter,

        [Parameter(ParameterSetName="Set individual rules")]
        [hashtable]
        $CardStyleRuleSettings,

        [Parameter(ParameterSetName="Set individual rules")]
        [string]
        $TagStyleRuleName,

        [Parameter(ParameterSetName="Set individual rules")]
        [string]
        $TagStyleRuleFilter,

        [Parameter(ParameterSetName="Set individual rules")]
        [hashtable]
        $TagStyleRuleSettings,

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
        Write-Verbose "Getting card rules for team $Team"

        if($Board -is [Microsoft.TeamFoundation.Work.WebApi.Board])
        {
            $boards = @($Board.Name)
            $Team = ([uri] $b.Links.Links.team.Href).Segments[-1]
            $Project = ([uri] $b.Links.Links.project.Href).Segments[-1]
        }
        elseif ($Board.ToString().Contains('*'))
        {
            $boards = (Get-TfsTeamBoard -Board $Board -Team $Team -Project $Project -Collection $Collection).Name
        }
        else
        {
            $boards = @($Board)
        }

        GET_TEAM($t,$tp,$tpc)
        GET_CLIENT('Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient')

        foreach($boardName in $boards)
        {
            if(-not $PSCmdlet.ShouldProcess($boardName, 'Set board card rule settings'))
            {
                continue
            }
            
            $ctx = New-Object 'Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext' -ArgumentList $tp.Name, $t.Name

            CALL_ASYNC($client.GetBoardCardRuleSettingsAsync($ctx,$boardName),"Error retrieving card rule settings for board '$Board'")

            Write-Output $result `
                | Add-Member -Name 'Team' -MemberType NoteProperty -Value $t.Name -PassThru `
                | Add-Member -Name 'Project' -MemberType NoteProperty -Value $tp.Name -PassThru
        }
    }
}
