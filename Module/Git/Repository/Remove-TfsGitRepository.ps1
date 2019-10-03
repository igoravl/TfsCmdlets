<#
.SYNOPSIS
Deletes one or more Git repositories from a team project.

.PARAMETER Project
HELP_PARAM_PROJECT

.PARAMETER Collection
HELP_PARAM_COLLECTION

.INPUTS
Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository
System.String
#>
Function Remove-TfsGitRepository
{
    [CmdletBinding(SupportsShouldProcess=$true, ConfirmImpact='High')]
    Param
    (
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [SupportsWildcards()]
        [Alias('Name')]
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
        Add-Type -AssemblyName 'Microsoft.TeamFoundation.Core.WebApi'
        Add-Type -AssemblyName 'Microsoft.TeamFoundation.SourceControl.WebApi'
    }

    Process
    {
        if ($Repository -is [Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository])
        {
            $Project = $Repository.ProjectReference.Name
        }

        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        #$tpc = $tp.Store.TeamProjectCollection

        GET_CLIENT('Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient')

        if ($Repository -is [Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository])
        {
            $reposToDelete = @($Repository)
        }
        else
        {
            $reposToDelete = Get-TfsGitRepository -Name $Repository -Project $Project -Collection $Collection
        }

        foreach($repo in $reposToDelete)
        {
            if ($PSCmdlet.ShouldProcess($repo.Name, "Delete Git repository from Team Project $($tp.Name)"))
            {
                $client.DeleteRepositoryAsync($repo.Id).Wait()
            }
        }
    }
}

