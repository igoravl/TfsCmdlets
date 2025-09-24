//HintName: TfsCmdlets.Cmdlets.ServiceHook.GetServiceHookConsumer.g.cs
namespace TfsCmdlets.Cmdlets.ServiceHook
{
    [Cmdlet("Get", "TfsServiceHookConsumer")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer))]
    public partial class GetServiceHookConsumer: CmdletBase
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