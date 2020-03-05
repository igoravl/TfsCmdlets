#define ITEM_TYPE Microsoft.TeamFoundation.Core.WebApi.WebApiTeam
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
    [OutputType('ITEM_TYPE')]
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

    Begin
    {

    }

    Process
    {
        if (-not $PSCmdlet.ShouldProcess($Project, "Create team $Team"))
        {
            return
        }

        GET_TEAM_PROJECT($tp,$tpc)

        $client = Get-TfsRestClient 'Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient' -Collection $tpc

        $result = $client.CreateTeamAsync((New-Object 'ITEM_TYPE' -Property @{
            Name = $Team
            Description = $Description
        }), $tp.Name).Result

        if ($Passthru)
        {
            return $result
        }
    }
}
