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
        $tpc = Get-TfsTeamProjectCollection -Collection $Collection
        GET_CLIENT('Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient')

        if ($Subscription -is [Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription])
        {
            $Subscription = $Subscription.Id
        }

        $client.GetNotifications([guid] $Subscription, $null, $null, $null, $null) | Select-Object -ExpandProperty Result
    }
}