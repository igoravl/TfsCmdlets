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

    [CmdletController(typeof(Notification), Client=typeof(IServiceHooksPublisherHttpClient))]
    partial class GetServiceHookNotificationHistoryController
    {
       protected override IEnumerable Run()
       {
           var ids = GetItems<WebApiSubscription>().Select(i => i.Id).ToArray();

           var query = new NotificationsQuery()
           {
               SubscriptionIds = ids,
               MinCreatedDate = From,
               MaxCreatedDate = To,
               Status = Status
           };

           var result = Client.QueryNotificationsAsync(query)
               .GetResult("Error getting service hook notifications")
               .Results;

           foreach (var r in result) yield return r;
       }
    }
}