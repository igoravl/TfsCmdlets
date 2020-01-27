Function Get-TfsServiceHookNotificationHistory
{
    Param
    (
        [Parameter(Position=0, ValueFromPipeline=$true, Mandatory=$true)]
        [object]
        $Subscription,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        GET_COLLECTION($tpc)
        GET_CLIENT('Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient')

        if ($Subscription -is [Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription])
        {
            $Subscription = $Subscription.Id
        }

        $client.GetNotifications([guid] $Subscription, $null, $null, $null, $null) | Select-Object -ExpandProperty Result
    }
}