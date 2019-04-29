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
Function Get-AzDevGitRepository
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
        $Organization
    )

    Begin
    {
        Import-RequiredAssembly 'Microsoft.TeamFoundation.SourceControl.WebApi'
    }

    Process
    {
        $org = Get-AzDevOrganization -Current

        $gitClient = $org.GetClient([type]'Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient')

        $repos = $gitClient.GetRepositoriesAsync($Project).Result
        
        return $repos | Where-Object Name -Like $Name
    }
}

