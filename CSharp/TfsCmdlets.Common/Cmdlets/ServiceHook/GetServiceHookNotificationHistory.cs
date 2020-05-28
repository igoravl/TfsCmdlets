/*
*/

using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    [Cmdlet(VerbsCommon.Get, "ServiceHookNotificationHistory")]
    [OutputType(typeof(Notification))]
    public class GetServiceHookNotificationHistory: BaseCmdlet
    {
/*
        [Parameter(Position=0, ValueFromPipeline=true, Mandatory=true)]
        public object Subscription { get; set; }

        [Parameter()]
        public object Collection { get; set; }

    protected override void ProcessRecord()
    {
        tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
        var client = tpc.GetClient<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient>();

        if (Subscription is Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription)
        {
            Subscription = Subscription.Id
        }

        client.GetNotifications([guid] Subscription, null, null, null, null) | Select-Object -ExpandProperty Result
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
