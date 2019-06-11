Function Get-TfsServiceHookSubscription
{
    [Cmdletbinding()]
    [OutputType('Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription')]
    Param
    (
        [Parameter(Position=0)]
        [string]
        $Name = '*',

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection -Collection $Collection
        $client = Get-RestClient -Type 'Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient' -Collection $tpc

        $client.QuerySubscriptionsAsync().Result
    }
}