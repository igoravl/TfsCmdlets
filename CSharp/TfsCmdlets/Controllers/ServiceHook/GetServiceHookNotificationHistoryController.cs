using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using TfsCmdlets.Cmdlets.ServiceHook;
using WebApiSubscription = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription;
using WebApiServiceHookNotification = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification;

namespace TfsCmdlets.Controllers.ServiceHook
{
    [CmdletController(typeof(Notification))]
    partial class GetServiceHookNotificationHistoryController
    {
       public override IEnumerable<WebApiServiceHookNotification> Invoke()
       {
           var subscription = Data.GetItem<WebApiSubscription>();
           var client = Data.GetClient<ServiceHooksPublisherHttpClient>();

           var from = Parameters.Get<DateTime?>(nameof(GetServiceHookNotificationHistory.From));
           var to = Parameters.Get<DateTime?>(nameof(GetServiceHookNotificationHistory.To));
           var status = Parameters.Get<NotificationStatus?>(nameof(GetServiceHookNotificationHistory.Status));

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