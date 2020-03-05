Function Get-TfsServiceHookPublisher
{
    [CmdletBinding()]
    [OutputType('Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher')]
    Param
    (
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias('Name')]
        [Alias('Id')]
        [string]
        $Publisher = '*',

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        GET_COLLECTION($tpc)
        $client = Get-TfsRestClient 'Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient' -Collection $tpc

        $client.GetPublishersAsync().Result | Where-Object {($_Name -Like $Publisher) -or ($_.Id -Like $Publisher)}
    }
}