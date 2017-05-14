<#

.SYNOPSIS
    Creates a new team.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.PARAMETER Passthru
    ${HelpParam_Passthru}

.INPUTS
    System.String
#>
Function New-TfsTeam
{
    [CmdletBinding(ConfirmImpact='Medium')]
    [OutputType([Microsoft.TeamFoundation.Client.TeamFoundationTeam])]
    param
    (
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [Alias("Name")]
        [string] 
        $Team,
    
        [Parameter()]
        [string] 
        $Description,
    
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

    Process
    {
        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        $tpc = $tp.Store.TeamProjectCollection
        $teamService = $tpc.GetService([type]"Microsoft.TeamFoundation.Client.TfsTeamService")

        $newTeam = $teamService.CreateTeam($tp.Uri, $Team, $Description, $null)

        if ($Passthru)
        {
            return $team
        }
    }
}
