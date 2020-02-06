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
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Position=0,Mandatory=$true,ParameterSetName='Get Identity')]
        [object]
        $Identity,

        [Parameter(ParameterSetName='Get Identity')]
        [switch]
        $QueryMembership,

        [Parameter(Mandatory=$true,ParameterSetName='Get current user')]
        [switch]
        $Current,

        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Server
    )

    Process
    {
        if($PSCmdlet.ParameterSetName -eq 'Get current user')
        {
            $srv = Get-TfsConfigurationServer -Current

            if(-not $srv)
            {
                return
            }

            $Identity = $srv.AuthorizedIdentity.TeamFoundationId
        }
        else
        {
            CHECK_ITEM($Identity)
            GET_SERVER($srv)
        }
        
        $client = Get-TfsRestClient 'Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient' -Server $srv

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