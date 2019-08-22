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
        [string] 
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
        if(_TestGuid($Repository))
        {
            GET_COLLECTION($tpc)
            
            GET_CLIENT('Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient')

            CALL_ASYNC($client.GetRepositoryAsync($guid),"Error getting repository with ID $guid")

            return $result
        }

        GET_TEAM_PROJECT($tp,$tpc)

        GET_CLIENT('Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient')

        CALL_ASYNC($client.GetRepositoriesAsync($tp.Name), "Error getting repository '$Repository'")
        
        return $result | Where-Object Name -Like $Repository
    }
}