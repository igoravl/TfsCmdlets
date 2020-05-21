/*
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    [Cmdlet(VerbsCommon.Get, "ServiceHookNotificationHistory")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification))]
    public class GetServiceHookNotificationHistory: PSCmdlet
    {
/*
        [Parameter(Position=0, ValueFromPipeline=true, Mandatory=true)]
        public object Subscription { get; set; }

        [Parameter()]
        public object Collection { get; set; }

    protected override void ProcessRecord()
    {
        tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
        client = Get-TfsRestClient "Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient" -Collection tpc

        if (Subscription is Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription)
        {
            Subscription = Subscription.Id
        }

        client.GetNotifications([guid] Subscription, null, null, null, null) | Select-Object -ExpandProperty Result
    }
}
*/
}
}
