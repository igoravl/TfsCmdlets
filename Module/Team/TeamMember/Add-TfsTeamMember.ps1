#define ITEM_TYPE TfsCmdlets.TeamAdmins
Function Add-TfsTeamMember
{
    [CmdletBinding(SupportsShouldProcess=$true,ConfirmImpact='Medium')]
    [OutputType('ITEM_TYPE')]
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
        GET_TEAM($t,$tp,$tpc)

        $gi = Get-TfsIdentity -Identity $t.Id -Collection $tpc
        $ui = Get-TfsIdentity -Identity $Identity -Collection $tpc

        if(-not $ui)
        {
            throw "Invalid or non-existent identity '$Identity'"
        }

        $client = Get-TfsRestClient 'Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient' -Collection $tpc

        _Log "Adding $($ui.IdentityType) '$($ui.DisplayName) ($($ui.Properties['Account']))' to team '$($t.Name)'"

        if(-not $PSCmdlet.ShouldProcess($t.Name, "Add member '$($ui.DisplayName) ($($ui.Properties['Account']))'"))
        {
            return
        }

        CALL_ASYNC($client.AddMemberToGroupAsync($gi.Descriptor, $ui.Descriptor), "Error adding team member '$($ui.DisplayName)' to team '$($t.Name)'")
    }
}
