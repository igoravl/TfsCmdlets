<#

.SYNOPSIS
    Gets information from one or more Git repositories in a team project.

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Get-TfsGitRepository
{
    [CmdletBinding()]
    [OutputType('Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository')]
    Param
    (
        [Parameter()]
        [SupportsWildcards()]
        [string] 
        $Name = '*',

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.SourceControl.WebApi)
    }

    Process
    {
        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        CHECK_TEAM_PROJECT($tp)
        
        $tpc = $tp.Store.TeamProjectCollection

        $gitClient = _GetRestClient 'Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient' -Collection $tpc

        $repos = $gitClient.GetRepositoriesAsync($tp.Name).Result
        
        return $repos | Where-Object Name -Like $Name
    }
}