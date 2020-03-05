Function Get-TfsServiceHookSubscription
{
    [CmdletBinding()]
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
        $client = Get-TfsRestClient 'Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient' -Collection $tpc

        $client.QuerySubscriptionsAsync().Result
    }
}