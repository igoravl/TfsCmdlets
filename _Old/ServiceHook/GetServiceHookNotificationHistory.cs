using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using TfsCmdlets.Services;
using WebApiSubscription = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription;
using WebApiServiceHookNotification = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification;
using System;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    /// <summary>
    /// Gets the notification history for a given service hook subscription
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsServiceHookNotificationHistory")]
    [OutputType(typeof(Notification))]
    public class GetServiceHookNotificationHistory : GetCmdletBase<WebApiServiceHookNotification>
    {
        /// <summary>
        /// Specifies the subscription to get the notification history from.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        public object Subscription { get; set; }

        /// <summary>
        /// Specifies the beginning of a date interval to filter notifications on.
        /// </summary>
        [Parameter()]
        public DateTime? From { get; set; }

        /// <summary>
        /// Specifies the end of a date interval to filter notifications on.
        /// </summary>
        [Parameter()]
        public DateTime? To { get; set; }

        /// <summary>
        /// Specifies the notification status to filter on.
        /// </summary>
        [Parameter()]
        public NotificationStatus Status { get; set; }
    }

    [Exports(typeof(WebApiServiceHookNotification))]
    internal class ServiceHookNotificationDataService : ControllerBase<WebApiServiceHookNotification>
    {
        protected override IEnumerable<WebApiServiceHookNotification> DoGetItems()
        {
            var subscription = GetItem<WebApiSubscription>();
            var client = GetClient<ServiceHooksPublisherHttpClient>();

            var from = GetParameter<DateTime?>(nameof(GetServiceHookNotificationHistory.From));
            var to = GetParameter<DateTime?>(nameof(GetServiceHookNotificationHistory.To));
            var status = GetParameter<NotificationStatus?>(nameof(GetServiceHookNotificationHistory.Status));

            var query = new NotificationsQuery()
            {
                SubscriptionIds = new[] { subscription.Id },
                MinCreatedDate = from,
                MaxCreatedDate = to,
                Status = status
            };

            var result = client.QueryNotificationsAsync(query)
                .GetResult("Error getting service hook notifications")
                .Results;

            foreach (var r in result) yield return r;
        }
    }
}