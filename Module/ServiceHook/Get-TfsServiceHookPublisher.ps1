Function Get-TfsServiceHookPublisher
{
    [Cmdletbinding()]
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
        GET_CLIENT('Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient')

        $client.GetPublishersAsync().Result | Where-Object {($_Name -Like $Publisher) -or ($_.Id -Like $Publisher)}
    }
}