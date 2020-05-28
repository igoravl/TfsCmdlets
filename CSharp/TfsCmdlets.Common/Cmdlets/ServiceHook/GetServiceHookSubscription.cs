using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    [Cmdlet(VerbsCommon.Get, "ServiceHookSubscription")]
    [OutputType(typeof(Subscription))]
    public class GetServiceHookSubscription: BaseCmdlet
    {
/*
        [Parameter(Position=0)]
        [Alias("Name")]
        public string Subscription { get; set; } = "*",

        [Parameter()]
        public object Collection { get; set; }

    protected override void ProcessRecord()
    {
        tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
        var client = tpc.GetClient<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient>();

        client.QuerySubscriptionsAsync().Result
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
