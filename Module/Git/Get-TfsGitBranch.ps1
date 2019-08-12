#define ITEM_TYPE Microsoft.TeamFoundation.SourceControl.WebApi.GitBranchStats
<#
.SYNOPSIS
    Gets information from one or more branches in a Git repository.

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Get-TfsGitBranch
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter()]
        [Alias('RefName')]
        [SupportsWildcards()]
        [object] 
        $Branch = '*',

        [Parameter(ValueFromPipeline=$true)]
        [SupportsWildcards()]
        [object] 
        $Repository,

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
        if((-not $Repository) -and $Project)
        {
            GET_TEAM_PROJECT($tp,$tpc)
            $Repository = $tp.Name
        }

        $repos = Get-TfsGitRepository -Repository $Repository -Project $Project -Collection $Collection

        GET_COLLECTION($tpc)

        GET_CLIENT('Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient')
        
        foreach($repo in $repos)
        {
            CALL_ASYNC($client.GetBranchesAsync($tp.Name,$repo.Id),"Error retrieving branches from repository '$($repo.Name)'")

            Write-Output $result | Where-Object name -Like $Branch | `
                Add-Member -Name  'Project' -MemberType NoteProperty -Value $repo.ProjectReference.Name -PassThru | `
                Add-Member -Name  'Repository' -MemberType NoteProperty -Value $repo.Name -PassThru | `
                Sort-Object Project, Repository
        }
    }
}