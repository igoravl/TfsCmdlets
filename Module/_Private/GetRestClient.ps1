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
        $Provider
    )

    Process
    {
        return _InvokeGenericMethod -InputObject $Provider -MethodName GetClient -GenericType $Type
    }
}
