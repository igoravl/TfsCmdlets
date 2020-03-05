#define ITEM_TYPE Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository
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
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias('Name')]
        [object] 
        $Repository = '*',

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
        CHECK_ITEM($Repository)
        
        if(_TestGuid($Repository))
        {
            GET_COLLECTION($tpc)
            
            $client = Get-TfsRestClient 'Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient' -Collection $tpc

            CALL_ASYNC($client.GetRepositoryAsync($guid),"Error getting repository with ID $guid")

            return $result
        }

        GET_TEAM_PROJECT($tp,$tpc)

        $client = Get-TfsRestClient 'Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient' -Collection $tpc

        CALL_ASYNC($client.GetRepositoriesAsync($tp.Name), "Error getting repository '$Repository'")
        
        return $result | Where-Object Name -Like $Repository
    }
}