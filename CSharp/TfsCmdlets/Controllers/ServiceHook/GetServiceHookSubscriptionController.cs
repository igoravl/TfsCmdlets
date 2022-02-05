using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using WebApiConsumer = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer;
using WebApiPublisher = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher;
using WebApiSubscription = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription;

namespace TfsCmdlets.Controllers.ServiceHook
{
    [CmdletController(typeof(WebApiSubscription))]
    partial class GetServiceHookSubscriptionController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<ServiceHooksPublisherHttpClient>();
            var publisher = Has_Publisher ? GetItem<WebApiPublisher>() : null;
            var consumer = Has_Consumer ? GetItem<WebApiConsumer>() : null;

            foreach (var subscription in Subscription)
            {
                switch (subscription)
                {
                    case WebApiSubscription p:
                        {
                            yield return p;
                            break;
                        }
                    case string s:
                        {
                            var query = new SubscriptionsQuery()
                            {
                                PublisherId = publisher?.Id,
                                ConsumerId = consumer?.Id,
                                EventType = EventType
                            };

                            var result = client.QuerySubscriptionsAsync(query)
                                .GetResult("Error getting service hook subscriptions")
                                .Results
                                .Where(sub => sub.ActionDescription.IsLike(s));

                            foreach (var r in result) yield return r;
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent Subscription '{subscription}'"));
                            break;
                        }
                }
            }
        }
    }
}