<#
.SYNOPSIS
Creates a new Git repository in a team project.

.PARAMETER Project
HELP_PARAM_PROJECT

.PARAMETER Collection
HELP_PARAM_COLLECTION

.PARAMETER Passthru
HELP_PARAM_PASSTHRU

.INPUTS
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String
#>
Function New-TfsGitRepository
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository')]
    Param
    (
        [Parameter(Mandatory=$true)]
        [Alias('Name')]
        [string] 
        $Repository,

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
        if($PSCmdlet.ShouldProcess($Repository, 'Create Git repository'))
        {
            $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
            #$tpc = $tp.Store.TeamProjectCollection

            $gitClient = _GetRestClient -Type 'Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient'
            $tpRef = [Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference] @{Id = $tp.Guid; Name = $tp.Name}
            $repoToCreate = [Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository] @{Name = $Repository; ProjectReference = $tpRef}
            $task = $gitClient.CreateRepositoryAsync($repoToCreate, $tp.Name)

            $result = $task.Result
            
            if ($Passthru)
            {
                return $result
            }
        }
    }
}
