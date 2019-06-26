Function _GetRestClient
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
        $tpc = Get-TfsTeamProjectCollection -Collection $Collection

        return _InvokeGenericMethod -InputObject $tpc -MethodName GetClient -GenericType $Type
    }
}
