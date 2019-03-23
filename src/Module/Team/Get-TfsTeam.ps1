<#

.SYNOPSIS
    Gets information about one or more teams.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Get-TfsTeam
{
    [CmdletBinding(DefaultParameterSetName="Get by name")]
    [OutputType([Microsoft.TeamFoundation.Client.TeamFoundationTeam])]
    param
    (
        [Parameter(Position=0, ParameterSetName="Get by name")]
        [Alias("Name")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Client.TeamFoundationTeam])})] 
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
        if ($Team -is [Microsoft.TeamFoundation.Client.TeamFoundationTeam])
        {
            return $Team
        }

        $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
        $tpc = $tp.Store.TeamProjectCollection
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
