#define ITEM_TYPE Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification
<#
#>
Function Get-TfsServiceHookNotificationHistory
{
    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
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
        $client = Get-TfsRestClient 'Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient' -Collection $tpc

        if ($Subscription -is [Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription])
        {
            $Subscription = $Subscription.Id
        }

        $client.GetNotifications([guid] $Subscription, $null, $null, $null, $null) | Select-Object -ExpandProperty Result
    }
}