Function Remove-TfsTeamMember
{
    [CmdletBinding(SupportsShouldProcess=$true,ConfirmImpact='High')]
    Param
    (
        # Specifies the board name(s). Wildcards accepted
        [Parameter(Position=0,ValueFromPipeline=$true)]
        [Alias('Name')]
        [Alias('User')]
        [Alias('Member')]
        [object]
        $Identity,

        [Parameter()]
        [object]
        $Team,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        if($Identity.TeamId -and $Identity.ProjectId)
        {
            $Project = $Identity.ProjectId 
            $t = Get-TfsTeam -Team $Identity.TeamId -Project $Project -Collection $Collection
            
            GET_TEAM_PROJECT($tp,$tpc)
        }
        else
        {
            GET_TEAM($t,$tp,$tpc)
        }

        $gi = Get-TfsIdentity -Identity $t.Id -Collection $tpc
        $ui = Get-TfsIdentity -Identity $Identity -Collection $tpc

        if(-not $ui)
        {
            throw "Invalid or non-existent identity '$Identity'"
        }

        GET_CLIENT('Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient')

        _Log "Removing $($ui.IdentityType) '$($ui.DisplayName) ($($ui.Properties['Account']))' from team '$($t.Name)'"

        if(-not $PSCmdlet.ShouldProcess($t.Name, "Remove member '$($ui.DisplayName) ($($ui.Properties['Account']))'"))
        {
            return
        }

        CALL_ASYNC($client.RemoveMemberFromGroupAsync($gi.Descriptor, $ui.Descriptor), "Error removing team member '$($ui.DisplayName)' from team '$($t.Name)'")
    }
}
