using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    [Cmdlet(VerbsCommon.Get, "TfsServiceHookSubscription")]
    [OutputType(typeof(Subscription))]
    public class GetServiceHookSubscription : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Position=0)]
        //         [Alias("Name")]
        //         public string Subscription { get; set; } = "*";

        //         [Parameter()]
        //         public object Collection { get; set; }

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void ProcessRecord()
        //     {
        //         tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
        //         var client = GetClient<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient>();

        //         client.QuerySubscriptionsAsync().Result
        //     }
        // }
    }
}
