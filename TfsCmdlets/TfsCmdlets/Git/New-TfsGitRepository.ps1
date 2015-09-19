<#

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function New-TfsGitRepository
{
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
    }

    Process
    {
        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        $tpc = $tp.Store.TeamProjectCollection

        $gitClient = Get-TfsClientObject -Type 'Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient'
        $tpRef = [Microsoft.TeamFoundation.Common.TeamProjectReference] @{Id = $tp.Guid; Name = $tp.Name}
        $repoToCreate = [Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository] @{Name = $Name; ProjectReference = $tpRef}
        $task = $gitClient.CreateRepositoryAsync($repoToCreate, $tp.Name)

        $task.Wait()
        
        if ($Passthru)
        {
            return $task.Result
        }
    }
}

