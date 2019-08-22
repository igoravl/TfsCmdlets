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
        $tpc = Get-TfsTeamProjectCollection -Collection $Collection
        $client = _GetRestClient -Type 'Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient' -Collection $tpc

        $client.QuerySubscriptionsAsync().Result
    }
}