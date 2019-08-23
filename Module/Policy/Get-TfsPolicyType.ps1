#define ITEM_TYPE Microsoft.TeamFoundation.Policy.WebApi.PolicyType
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
Function Get-TfsPolicyType
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter()]
        [SupportsWildcards()]
        [Alias("Name")]
        [object] 
        $Type = '*',

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.Policy.WebApi)
    }

    Process
    {
        CHECK_ITEM($Type)

        GET_TEAM_PROJECT($tp,$tpc)
        
        $client = _GetRestClient 'Microsoft.TeamFoundation.Policy.WebApi.PolicyHttpClient' -Collection $tpc

        CALL_ASYNC($client.GetPolicyTypesAsync($tp.Name),"Error retrieving policy types")
        
        return $result | Where-Object Display -Like $PolicyType
    }
}