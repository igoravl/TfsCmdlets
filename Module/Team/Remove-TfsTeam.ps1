#define ITEM_TYPE Microsoft.TeamFoundation.Core.WebApi.WebApiTeam
<#
.SYNOPSIS
    Deletes a team.

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.Client.TeamFoundationTeam
    System.String
#>
Function Remove-TfsTeam
{
    [CmdletBinding(SupportsShouldProcess=$true, ConfirmImpact='High')]
    [OutputType('ITEM_TYPE')]
    param
    (
        [Parameter(Position=0, ValueFromPipeline=$true)]
        [Alias("Name")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [ITEM_TYPE])})] 
        [SupportsWildcards()]
        [object]
        $Team = '*',

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        GET_TEAM_PROJECT_FROM_ITEM($tpc,$tp,$Team.ProjectName)
        $t = Get-TfsTeam -Team $Team -Project $Project -Collection $Collection

        if (-not $PSCmdlet.ShouldProcess($t.Name, 'Delete team'))
        {
            return
        }

        GET_CLIENT('Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient')
        $task = $client.DeleteTeamAsync($tp.Name, $t.Name)

        CHECK_ASYNC($task,$result,'Error deleting team')
    }
}
