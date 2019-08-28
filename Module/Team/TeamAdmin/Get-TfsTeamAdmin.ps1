#define ITEM_TYPE Microsoft.VisualStudio.Services.Identity.Identity
Function Get-TfsTeamAdmin
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        # Specifies the board name(s). Wildcards accepted
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [object]
        $Identity = '*',

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
        if($Team -is [Microsoft.TeamFoundation.Core.WebApi.WebApiTeam])
        {
            $Project = $Team.ProjectId
        }

        $t = Get-TfsTeam -Team $Team -Project $Project -Collection $Collection -IncludeMembers

        GET_COLLECTION($tpc)

        _Log "Returning team admins from team '$($t.Name)'"

        foreach($member in $t.Members)
        {
            if(-not $member.IsTeamAdmin)
            {
            continue
            }

            $i = Get-TfsIdentity -Identity $member.Identity.Id -Collection $Collection

            if (($i.DisplayName -like $Identity) -or ($i.Properties['Account'] -like $Identity))
            {
                Write-Output $i | `
                    Add-Member -Name TeamId -MemberType NoteProperty -Value $t.Id -PassThru | `
                    Add-Member -Name ProjectId -MemberType NoteProperty -Value $t.ProjectId -PassThru
            }
        }
    }
}
