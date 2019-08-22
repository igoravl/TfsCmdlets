#define ITEM_TYPE Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration
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
Function Get-TfsGitBranchPolicy
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Position=0, ValueFromPipeline=$true)]
        [SupportsWildcards()]
        [object] 
        $Repository = '*',

        [Parameter()]
        [Alias('RefName')]
        [AllowNull()]
        [object] 
        $Branch = 'master',

        [Parameter()]
        [object] 
        $PolicyType,

        [Parameter()]
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
        GET_TEAM_PROJECT_FROM_ITEM($tp,$tpc,$Repository.ProjectReference.Name)

        GET_CLIENT('Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient')
        
        if($Branch -and ($Branch -notlike 'refs/*'))
        {
            $Branch = "refs/heads/$Branch"
        }

        $policyTypeId = $null

        if($PolicyType)
        {
            $policy = Get-TfsPolicyType -Type $PolicyType -Project $tp -Collection $tpc

            if(-not $policy)
            {
                throw "Invalid or non-existent policy type '$PolicyType'"
            }
            
            $policyTypeId = $PolicyType.Id
        }

        $repos = Get-TfsGitRepository -Repository $Repository -Project $tp -Collection $tpc

        foreach($repo in $repos)
        {
            CALL_ASYNC($client.GetPolicyConfigurationsAsync($tp.Name, $repo.Id, $Branch, $policyTypeId),"Error retrieving branch policy configurations for repository '$($repo.Name)'")
        }
        
        return $result.PolicyConfigurations
    }
}