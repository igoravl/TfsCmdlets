//HintName: TfsCmdlets.Cmdlets.ServiceHook.GetServiceHookNotificationHistory.g.cs
namespace TfsCmdlets.Cmdlets.ServiceHook
{
    [Cmdlet("Get", "TfsServiceHookNotificationHistory")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification))]
    public partial class GetServiceHookNotificationHistory: CmdletBase
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