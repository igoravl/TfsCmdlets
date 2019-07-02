Function Get-TfsRestClient
{
    [CmdletBinding()]
    [OutputType('Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase')]
    Param
    (
        [Parameter(Mandatory=$true, Position=0)]
        [string]
        $Type,

        [Parameter()]
        [object] 
        $Collection
    )

    Process
    {
        return _GetRestClient @PSBoundParameters
    }
}
