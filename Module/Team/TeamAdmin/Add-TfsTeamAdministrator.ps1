#define ITEM_TYPE TfsCmdlets.TeamAdmins
Function Add-TfsTeamAdministrator
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        # Specifies the board name(s). Wildcards accepted
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias('Name')]
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

        $id = Get-TfsIdentity -Identity $Identity -Collection $rpc

        GET_CLIENT('TfsCmdlets.TeamAdminHttpClient')

        _Log "Adding $($id.IdentityType) '$($id.DisplayName) ($($id.Properties['Account']))' to team '$($t.Name)'"

        CALL_ASYNC($client.AddTeamAdministratorAsync($tp.Name, $t.Id, $id.Id),'Error setting team administrator')

        return $result
    }
}
