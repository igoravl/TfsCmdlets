<#

.SYNOPSIS
    Creates a new team.

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.PARAMETER Passthru
    HELP_PARAM_PASSTHRU

.INPUTS
    System.String
#>
Function New-TfsTeam
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('Microsoft.TeamFoundation.Client.TeamFoundationTeam')]
    param
    (
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [Alias("Team")]
        [string] 
        $Name,
    
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
        if ($PSCmdlet.ShouldProcess($Name, 'Create team'))
        {
            $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
            $tpc = $tp.Store.TeamProjectCollection
            $teamService = $tpc.GetService([type]"Microsoft.TeamFoundation.Client.TfsTeamService")

            $team = $teamService.CreateTeam($tp.Uri, $Name, $Description, $null)

            if ($Passthru)
            {
                return $team
            }
        }
    }
}
