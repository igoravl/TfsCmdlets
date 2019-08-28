#define ITEM_TYPE TfsCmdlets.TeamAdmins
Function Remove-TfsTeamAdmin
{
    [CmdletBinding(SupportsShouldProcess=$true,ConfirmImpact='High')]
    [OutputType('ITEM_TYPE')]
    Param
    (
        # Specifies the board name(s). Wildcards accepted
        [Parameter(Position=0,ValueFromPipeline=$true)]
        [Alias('Name')]
        [Alias('User')]
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

        $id = Get-TfsIdentity -Identity $Identity -Collection $rpc

        GET_CLIENT('TfsCmdlets.TeamAdminHttpClient')

        _Log "Removing $($id.IdentityType) '$($id.DisplayName) ($($id.Properties['Account']))' from team '$($t.Name)'"

        if(-not $PSCmdlet.ShouldProcess($t.Name, "Remove administrator '$($id.DisplayName) ($($id.Properties['Account']))'"))
        {
            return
        }

        if(-not ([bool] $client.RemoveTeamAdmin($tp.Name, $t.Id, $id.Id).success))
        {
            throw 'Error removing team administrator'
        }
    }
}
