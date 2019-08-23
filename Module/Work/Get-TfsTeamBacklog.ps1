#define ITEM_TYPE Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration
<#
.SYNOPSIS
Gets information about one or more backlogs of the given team.

.PARAMETER Project
HELP_PARAM_PROJECT

.PARAMETER Collection
HELP_PARAM_COLLECTION

.INPUTS
Microsoft.TeamFoundation.Core.WebApi.WebApiTeam
System.String
#>
Function Get-TfsTeamBacklog
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    param
    (
        [Parameter(Position=0)]
        [Alias("Name")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [ITEM_TYPE])})] 
        [SupportsWildcards()]
        [object]
        $Backlog = '*',

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
        # REQUIRES(Microsoft.TeamFoundation.Work.WebApi)
        # REQUIRES(Microsoft.TeamFoundation.WorkItemTracking.WebApi)
    }

    Process
    {
        CHECK_ITEM($Backlog)
        $t = Get-TfsTeam -Team $Team -Project $Project -Collection $Collection
        GET_TEAM_PROJECT_FROM_ITEM($tp,$tpc,$t.ProjectName)

        $client = _GetRestClient 'Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient' -Collection $tpc
        $ctx = New-Object 'Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext' -ArgumentList @($tp.Name, $t.Name)

        if (-not $Backlog.ToString().Contains('*'))
        {
            _Log "Get backlog '$Backlog'"
            $task = $client.GetBacklogAsync($ctx, $Backlog)

            CHECK_ASYNC($task,$result,"Error getting backlog '$Backlog'")
        }
        else
        {
            _Log "Get all backlogs matching '$Backlog'"
            $task = $client.GetBacklogsAsync($ctx)
            CHECK_ASYNC($task,$result,'Error enumerating backlogs')
            
            $result = $result | Where-Object Name -like $Backlog
        }

        return $result
    }
}
