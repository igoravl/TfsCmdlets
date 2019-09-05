#define ITEM_TYPE Microsoft.VisualStudio.Services.Identity.Identity
Function Get-TfsGroupMember
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
        $Group,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        GET_COLLECTION($tpc)

        _Log "Returning members from group '$Group'"

        $gi = Get-TfsIdentity -Identity $Group -Collection $tpc -QueryMembership

        if(-not $gi)
        {
            throw "Invalid or non-existent group '$Group'"
        }

        foreach($mid in $gi.MemberIds)
        {
            $i = Get-TfsIdentity -Identity $mid -Collection $Collection

            if (($i.DisplayName -like $Identity) -or ($i.Properties['Account'] -like $Identity))
            {
                Write-Output $i
            }
        }
    }
}
