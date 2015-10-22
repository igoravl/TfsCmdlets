<#

.SYNOPSIS
    Gets information from one or more Git repositories in a team project.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Get-TfsGitRepository
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository])]
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

    Process
    {
        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        $tpc = $tp.Store.TeamProjectCollection
        $id = $tp.Guid

        $gitService = $tpc.GetService([type]'Microsoft.TeamFoundation.Git.Client.GitRepositoryService')

        return $gitService.QueryRepositories($tp.Name) | ? Name -Like $Name
    }
}

