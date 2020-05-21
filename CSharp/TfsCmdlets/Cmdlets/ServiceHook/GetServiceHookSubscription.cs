using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    [Cmdlet(VerbsCommon.Get, "ServiceHookSubscription")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription))]
    public class GetServiceHookSubscription: PSCmdlet
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
        client = Get-TfsRestClient "Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient" -Collection tpc

        client.QuerySubscriptionsAsync().Result
    }
}
*/
}
}
