using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    [Cmdlet(VerbsCommon.Get, "ServiceHookPublisher")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher))]
    public class GetServiceHookPublisher: BaseCmdlet
    {
/*
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias("Name")]
        [Alias("Id")]
        public string Publisher { get; set; } = "*",

        [Parameter()]
        public object Collection { get; set; }

    protected override void ProcessRecord()
    {
        tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
        client = Get-TfsRestClient "Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient" -Collection tpc

        client.GetPublishersAsync().Result | Where-Object {(_Name -Like Publisher) || (_.Id -Like Publisher)}
    }
}
*/
}
}
