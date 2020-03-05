Function Add-TfsGroupMember
{
    [CmdletBinding(SupportsShouldProcess=$true,ConfirmImpact='Medium')]
    Param
    (
        # Specifies the board name(s). Wildcards accepted
        [Parameter(Position=0)]
        [Alias('Name')]
        [Alias('Member')]
        [Alias('User')]
        [object]
        $Identity,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Group,

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

        $client = Get-TfsRestClient 'Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient' -Collection $tpc

        _Log "Adding $($ui.IdentityType) '$($ui.DisplayName) ($($ui.Properties['Account']))' to group '$($gi.DisplayName)'"

        if(-not $PSCmdlet.ShouldProcess($gi.DisplayName, "Add member '$($ui.DisplayName) ($($ui.Properties['Account']))'"))
        {
            return
        }

        CALL_ASYNC($client.AddMemberToGroupAsync($gi.Descriptor, $ui.Descriptor), "Error adding member '$($ui.DisplayName)' to group '$($gi.DisplayName)'")
    }
}
