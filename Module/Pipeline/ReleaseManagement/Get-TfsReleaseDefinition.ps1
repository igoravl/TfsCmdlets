#define ITEM_TYPE Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.ReleaseDefinition
<#
.SYNOPSIS
    Gets information from one or more release definitions in a team project.

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Get-TfsReleaseDefinition
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias('Name')]
        [object] 
        $Definition = '*',

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Begin
    {
    }

    Process
    {
        CHECK_ITEM($Definition)
        
        # if(_TestGuid($Definition))
        # {
        #     GET_COLLECTION($tpc)
            
        #     GET_CLIENT('Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient')

        #     CALL_ASYNC($client.GetRepositoryAsync($guid),"Error getting repository with ID $guid")

        #     return $result
        # }

        GET_TEAM_PROJECT($tp,$tpc)

        GET_CLIENT('Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients.ReleaseHttpClient2')

        CALL_ASYNC($client.GetReleaseDefinitionsAsync($tp.Name), "Error getting release definition '$Definition'")
        
        return $result | Where-Object Name -Like $Definition
    }
}