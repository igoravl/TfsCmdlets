Function Remove-TfsGroupMember
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
        $Group,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        GET_COLLECTION($tpc)

        $gi = Get-TfsIdentity -Identity $Group -Collection $tpc
        $ui = Get-TfsIdentity -Identity $Identity -Collection $tpc

        if(-not $gi)
        {
            throw "Invalid or non-existent group '$Group'"
        }

        if(-not $ui)
        {
            throw "Invalid or non-existent identity '$Identity'"
        }

        GET_CLIENT('Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient')

        _Log "Removing $($ui.IdentityType) '$($ui.DisplayName) ($($ui.Properties['Account']))' from group '$($gi.DisplayName)'"

        if(-not $PSCmdlet.ShouldProcess($gi.DisplayName, "Remove member '$($ui.DisplayName) ($($ui.Properties['Account']))'"))
        {
            return
        }

        CALL_ASYNC($client.RemoveMemberFromGroupAsync($gi.Descriptor, $ui.Descriptor), "Error removing member '$($ui.DisplayName)' from group '$($gi.DisplayName)'")
    }
}
