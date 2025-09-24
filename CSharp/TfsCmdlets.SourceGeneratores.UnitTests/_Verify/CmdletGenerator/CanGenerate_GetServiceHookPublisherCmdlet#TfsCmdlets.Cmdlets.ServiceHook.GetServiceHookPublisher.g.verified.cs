//HintName: TfsCmdlets.Cmdlets.ServiceHook.GetServiceHookPublisher.g.cs
namespace TfsCmdlets.Cmdlets.ServiceHook
{
    [Cmdlet("Get", "TfsServiceHookPublisher")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher))]
    public partial class GetServiceHookPublisher: CmdletBase
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