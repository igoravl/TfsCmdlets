//HintName: TfsCmdlets.Cmdlets.ServiceHook.GetServiceHookSubscription.g.cs
namespace TfsCmdlets.Cmdlets.ServiceHook
{
    [Cmdlet("Get", "TfsServiceHookSubscription")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription))]
    public partial class GetServiceHookSubscription: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Alias("Organization")]
        [Parameter(ValueFromPipeline=true)]
        public object Collection { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}