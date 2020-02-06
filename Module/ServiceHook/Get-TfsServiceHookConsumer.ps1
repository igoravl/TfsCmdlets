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
        GET_CLIENT('Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient')

        return $client.GetConsumersAsync().Result | Where-Object {($_Name -Like $Consumer) -or ($_.Id -Like $Consumer)}
    }
}