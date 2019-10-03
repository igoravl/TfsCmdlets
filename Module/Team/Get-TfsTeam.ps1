#define ITEM_TYPE Microsoft.TeamFoundation.Core.WebApi.WebApiTeam
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
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    param
    (
        [Parameter(Position=0)]
        [Alias("Name")]
        [SupportsWildcards()]
        [object]
        $Team = '*',

        [Parameter()]
        [switch]
        $IncludeMembers,

        [Parameter()]
        [switch]
        $IncludeSettings,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.Work.WebApi)
    }

    Process
    {
        CHECK_ITEM($Team)

        GET_TEAM_PROJECT($tp,$tpc)

        GET_CLIENT('Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient')

        GET_CLIENT('Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient')

        if($Team.ToString().Contains('*'))
        {
            _Log "Get all teams matching '$Team'"
            $teams = $client.GetTeamsAsync($tp.Name).Result | Where-Object Name -like $Team
        }
        else
        {
            _Log "Get team named '$Team'"

            if(_TestGuid $Team)
            {
                $Team = [guid]$Team
            }

            $teams = $client.GetTeamAsync($tp.Name, $Team).Result
        }

        foreach($t in $teams)
        {
            if ($IncludeMembers.IsPresent)
            {
                _Log "Retrieving team membership information for team '$($t.Name)'"

                $members = $client.GetTeamMembersWithExtendedPropertiesAsync($tp.Name, $t.Name).Result
                $t | Add-Member -Name 'Members' -MemberType NoteProperty -Value $members

            }
            else
            {
                $t | Add-Member -Name 'Members' -MemberType NoteProperty -Value @()
            }

            if ($IncludeSettings.IsPresent)
            {
                _Log "Retrieving team settings for team '$($t.Name)'"

                $ctx = New-Object 'Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext' -ArgumentList $tp.Name, $t.Name
                $t | Add-Member -Name 'Settings' -MemberType NoteProperty -Value $workClient.GetTeamSettingsAsync($ctx).Result
            }
            else
            {
                $t | Add-Member -Name 'Settings' -MemberType NoteProperty -Value $null
            }
        }

        return $teams
    }
}
