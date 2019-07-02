#define ITEM_TYPE Microsoft.TeamFoundation.Client.TeamFoundationTeam
<#

.SYNOPSIS
    Gets information about one or more teams.

.PARAMETER Project
    HELP_PARAM_PROJECT

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Get-TfsTeam
{
    [CmdletBinding(DefaultParameterSetName="Get by name")]
    [OutputType('ITEM_TYPE')]
    param
    (
        [Parameter(Position=0, ParameterSetName="Get by name")]
        [Alias("Name")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [ITEM_TYPE])})] 
        [SupportsWildcards()]
        [object]
        $Team = '*',

        [Parameter(Position=0, ParameterSetName="Get default team")]
        [switch]
        $Default,

        [Parameter()]
        [switch]
        $IncludeMembers,

        [Parameter()]
        [Microsoft.TeamFoundation.Framework.Common.MembershipQuery]
        $QueryMembership = [Microsoft.TeamFoundation.Framework.Common.MembershipQuery]::Direct,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        CHECK_ITEM($Team)

        GET_TEAM_PROJECT($tp,$tpc)

        $teamService = $tpc.GetService([type]'Microsoft.TeamFoundation.Client.TfsTeamService')

        if ($Default)
        {
            $teams = @($teamService.GetDefaultTeam($tp.Uri, $null))
        }
        else
        {
            $teams = $teamService.QueryTeams([string]$tp.Uri) | Where-Object Name -like $Team
        }

        foreach($t in $teams)
        {
            if ($IncludeMembers)
            {
                $members = $t.GetMembers($tpc, $QueryMembership)
                $t | Add-Member -Name 'Members' -MemberType ([System.Management.Automation.PSMemberTypes]::NoteProperty) -Value $members -PassThru

            }
            else
            {
                $t | Add-Member -Name 'Members' -MemberType ([System.Management.Automation.PSMemberTypes]::NoteProperty) -Value ([Microsoft.TeamFoundation.Framework.Client.TeamFoundationIdentity[]] @()) -PassThru
            }
        }
    }
}
