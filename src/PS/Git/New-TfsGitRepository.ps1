<#

.SYNOPSIS
    Creates a new Git repository in a team project.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.PARAMETER Passthru
    ${HelpParam_Passthru}

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function New-TfsGitRepository
{
    [CmdletBinding(ConfirmImpact='Medium')]
    [OutputType([Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository])]
    Param
    (
        [Parameter(Mandatory=$true)]
        [string] 
        $Name,

        [Parameter(ValueFromPipeline=$true)]
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
        Add-Type -AssemblyName 'Microsoft.TeamFoundation.Common'
    }

    Process
    {
        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        $tpc = $tp.Store.TeamProjectCollection

        $gitClient = Get-TfsHttpClient -Type 'Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient'
        $tpRef = [Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference] @{Id = $tp.Guid; Name = $tp.Name}
        $repoToCreate = [Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository] @{Name = $Name; ProjectReference = $tpRef}
        $task = $gitClient.CreateRepositoryAsync($repoToCreate, $tp.Name)

        $result = $task.Result
        
        if ($Passthru)
        {
            return $result
        }
    }
}

