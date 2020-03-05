Function Get-TfsServiceHookConsumer
{
    [CmdletBinding()]
    [OutputType('Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer')]
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
        GET_COLLECTION($tpc)
        $client = Get-TfsRestClient 'Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient' -Collection $tpc

        return $client.GetConsumersAsync().Result | Where-Object {($_Name -Like $Consumer) -or ($_.Id -Like $Consumer)}
    }
}