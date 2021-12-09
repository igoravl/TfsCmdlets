using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using TfsCmdlets.Cmdlets.ServiceHook;
using WebApiConsumer = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer;
using WebApiPublisher = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher;
using WebApiSubscription = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription;

namespace TfsCmdlets.Controllers.ServiceHook
{
    [CmdletController(typeof(WebApiSubscription))]
    partial class GetServiceHookSubscriptionController
    {
        public override IEnumerable<WebApiSubscription> Invoke()
        {
            var client = Data.GetClient<ServiceHooksPublisherHttpClient>();
            var subscription = Parameters.Get<object>(nameof(GetServiceHookSubscription.Subscription));
            var eventType = Parameters.Get<string>(nameof(GetServiceHookSubscription.EventType));

            var publisher = Parameters.HasParameter(nameof(GetServiceHookSubscription.Publisher)) ?
                Data.GetItem<WebApiPublisher>() : null;
            var consumer = Parameters.HasParameter(nameof(GetServiceHookSubscription.Consumer)) ?
                Data.GetItem<WebApiConsumer>() : null;

            while (true) switch (subscription)
                {
                    case WebApiSubscription p:
                        {
                            yield return p;
                            yield break;
                        }
                    case string s:
                        {
                            var query = new SubscriptionsQuery()
                            {
                                PublisherId = publisher?.Id,
                                ConsumerId = consumer?.Id,
                                EventType = eventType
                            };

                            var result = client.QuerySubscriptionsAsync(query)
                                .GetResult("Error getting service hook subscriptions")
                                .Results
                                .Where(sub => sub.ActionDescription.IsLike(s));

                            foreach (var r in result) yield return r;
                            yield break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent Subscription '{subscription}'");
                        }
                }
        }
    }
}