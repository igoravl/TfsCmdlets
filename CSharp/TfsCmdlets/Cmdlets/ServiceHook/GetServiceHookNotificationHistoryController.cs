using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using TfsCmdlets.Cmdlets.ServiceHook;
using WebApiSubscription = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription;
using WebApiServiceHookNotification = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification;

namespace TfsCmdlets.Controllers.ServiceHook
{
    [CmdletController(typeof(Notification))]
    partial class GetServiceHookNotificationHistoryController
    {
       protected override IEnumerable Run()
       {
           var ids = GetItems<WebApiSubscription>().Select(i => i.Id).ToArray();
           var client = Data.GetClient<ServiceHooksPublisherHttpClient>();

           var query = new NotificationsQuery()
           {
               SubscriptionIds = ids,
               MinCreatedDate = From,
               MaxCreatedDate = To,
               Status = Status
           };

           var result = client.QueryNotificationsAsync(query)
               .GetResult("Error getting service hook notifications")
               .Results;

           foreach (var r in result) yield return r;
       }
    }
}