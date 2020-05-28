using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    [Cmdlet(VerbsCommon.Get, "ServiceHookConsumer")]
    [OutputType(typeof(Consumer))]
    public class GetServiceHookConsumer: BaseCmdlet
    {
/*
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias("Name")]
        [Alias("Id")]
        public string Consumer { get; set; } = "*",

        [Parameter()]
        public object Collection { get; set; }

    protected override void ProcessRecord()
    {
        tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
        var client = tpc.GetClient<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient>();

        WriteObject(client.GetConsumersAsync().Result | Where-Object {(_Name -Like Consumer) || (_.Id -Like Consumer)}); return;
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
