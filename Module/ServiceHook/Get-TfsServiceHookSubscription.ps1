Function Get-TfsServiceHookSubscription
{
    [Cmdletbinding()]
    [OutputType('Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription')]
    Param
    (
        [Parameter(Position=0)]
        [Alias('Name')]
        [string]
        $Subscription = '*',

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        GET_COLLECTION($tpc)
        GET_CLIENT('Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient')

        $client.QuerySubscriptionsAsync().Result
    }
}