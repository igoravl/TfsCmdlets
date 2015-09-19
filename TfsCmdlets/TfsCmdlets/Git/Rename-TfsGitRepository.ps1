<#

.SYNOPSIS
    Renames a Git repository in a team project.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Rename-TfsGitRepository
{
    [CmdletBinding(SupportsShouldProcess=$true)]
    [OutputType([Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository])]
    Param
    (
        [Parameter(Mandatory=$true, ValueFromPipeline=$true, Position=0)]
        [object] 
        $Repository,

        [Parameter(Mandatory=$true, Position=1)]
        [string] 
        $NewName,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $Passthru
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
        $tpc = $tp.Store.TeamProjectCollection

        $gitClient = Get-TfsClientObject -Type 'Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient'

        if ($Repository -is [Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository])
        {
            $reposToRename = @($Repository)
        }
        else
        {
            $reposToRename = Get-TfsGitRepository -Name $Repository -Project $Project -Collection $Collection
        }

        foreach($repo in $reposToRename)
        {
            if ($PSCmdlet.ShouldProcess($repo.Name, "Rename Git repository in Team Project $($tp.Name) to $NewName"))
            {
                $task = $gitClient.RenameRepositoryAsync($repo, $NewName)
                $task.Wait()

                if ($Passthru)
                {
                    return $task.Result
                }
            }
        }
    }
}

