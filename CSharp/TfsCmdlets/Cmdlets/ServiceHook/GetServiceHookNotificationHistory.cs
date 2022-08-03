using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using WebApiSubscription = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription;
using WebApiServiceHookNotification = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    /// <summary>
    /// Gets the notification history for a given service hook subscription
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(Notification))]
    partial class GetServiceHookNotificationHistory
    {
        /// <summary>
        /// Specifies the subscription to get the notification history from.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        public object Subscription { get; set; }

        /// <summary>
        /// Specifies the beginning of a date interval to filter notifications on.
        /// </summary>
        [Parameter]
        public DateTime? From { get; set; }

        /// <summary>
        /// Specifies the end of a date interval to filter notifications on.
        /// </summary>
        [Parameter]
        public DateTime? To { get; set; }

        /// <summary>
        /// Specifies the notification status to filter on.
        /// </summary>
        [Parameter]
        public NotificationStatus Status { get; set; }
    }
}