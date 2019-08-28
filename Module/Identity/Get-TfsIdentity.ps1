#define ITEM_TYPE Microsoft.VisualStudio.Services.Identity.Identity
<#
.SYNOPSIS
    Short description
.DESCRIPTION
    Long description
.EXAMPLE
    PS C:\> <example usage>
    Explanation of what the example does
.INPUTS
    Inputs (if any)
.OUTPUTS
    Output (if any)
.NOTES
    General notes
#>
Function Get-TfsIdentity
{
    [CmdletBinding()]
    Param
    (
        [Parameter(Position=0,Mandatory=$true)]
        [object]
        $Identity,

        [Parameter()]
        [switch]
        $QueryMembership,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Collection
    )

    Process
    {
        CHECK_ITEM($Identity)

        GET_COLLECTION($tpc)
        
        GET_CLIENT('Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient')

        if($QueryMembership.IsPresent)
        {
            $qm = [Microsoft.VisualStudio.Services.Identity.QueryMembership]::Direct
        }
        else
        {
            $qm = [Microsoft.VisualStudio.Services.Identity.QueryMembership]::None
        }

        if(_TestGuid $Identity)
        {
            _Log "Finding identity with ID [$Identity] and QueryMembership=$qm"
            CALL_ASYNC($client.ReadIdentityAsync([guid]$Identity),"Error retrieving information from identity [$Identity]")
        }
        else
        {
            _Log "Finding identity with account name [$Identity] and QueryMembership=$qm"
            CALL_ASYNC($client.ReadIdentitiesAsync([Microsoft.VisualStudio.Services.Identity.IdentitySearchFilter]::AccountName, [string]$Identity, 'None', $qm),"Error retrieving information from identity [$Identity]")
        }

        return $result
    }
}