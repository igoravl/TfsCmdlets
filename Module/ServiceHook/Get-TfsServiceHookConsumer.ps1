Function Get-TfsServiceHookConsumer
{
    [Cmdletbinding()]
    [OutputType([Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer])]
    Param
    (
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias('Name')]
        [Alias('Id')]
        [string]
        $Consumer = '*',

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection -Collection $Collection
        $client = Get-RestClient -Type 'Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient' -Collection $tpc

        $client.GetConsumersAsync().Result | Where-Object {($_Name -Like $Consumer) -or ($_.Id -Like $Consumer)}
    }
}