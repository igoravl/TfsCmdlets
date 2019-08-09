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
        [Parameter()]
        [SupportsWildcards()]
        [object] 
        $Repository,

        [Parameter()]
        [Alias('RefName')]
        [AllowNull()]
        [string] 
        $Branch,

        [Parameter()]
        [object] 
        $PolicyType,

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
        GET_TEAM_PROJECT($tp,$tpc)

        GET_CLIENT('Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient')
        
        if($Branch -and ($Branch -notlike 'refs/*'))
        {
            $Branch = "refs/heads/$Branch"
        }

        $policyTypeId = $null

        if($PolicyType)
        {
            $PolicyType = Get-TfsPolicyType -Type $PolicyType -Collection $Collection
            $policyTypeId = $PolicyType.Id
        }

        if($Repository)
        {
            $repos = Get-TfsGitRepository -Repository $Repository -Project $Project -Collection $Collection

            foreach($repo in $repos)
            {
                CALL_ASYNC($client.GetPolicyConfigurationsAsync($tp.Name, $repo.Id, $Branch, $policyTypeId),"Error retrieving branch policy configurations")
                Write-Output $result.PolicyConfigurations
            }

            return
        }

        CALL_ASYNC($client.GetPolicyConfigurationsAsync($tp.Name, $null, $Branch, $policyTypeId),"Error retrieving branch policy configurations")
        
        return $result.PolicyConfigurations
    }
}