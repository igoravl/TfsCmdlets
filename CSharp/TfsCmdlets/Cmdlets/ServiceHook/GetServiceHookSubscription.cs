using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
using TfsCmdlets.Services;
using WebApiConsumer = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer;
using WebApiPublisher = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher;
using WebApiSubscription = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription;
using TfsCmdlets.Extensions;
using System.Linq;
using System;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    /// <summary>
    /// Gets one or more service hook subscriptions
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsServiceHookSubscription")]
    [OutputType(typeof(WebApiSubscription))]
    public class GetServiceHookSubscription : CmdletBase
    {
        /// <summary>
        /// Specifies the name ("action description") of the subscription. Wildcards are supported. 
        /// When omitted, returns all service hook subscriptions in the given 
        /// team project collection.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Name")]
        public object Subscription { get; set; } = "*";

        /// <summary>
        /// Specifies the name or ID of the service hook publisher to filter subscriptions by.
        /// When omitted, returns all subscriptions regardless of their publishers.
        /// </summary>
        [Parameter()]
        public object Publisher { get; set; }

        /// <summary>
        /// Specifies the name or ID of the service hook consumer to filter subscriptions by. 
        /// When omitted, returns all subscriptions regardless of their consumers.
        /// </summary>
        [Parameter()]
        public object Consumer { get; set; }

        /// <summary>
        /// Specifies the event type to filter subscriptions by. 
        /// When omitted, returns all subscriptions regardless of their event types.
        /// </summary>
        [Parameter()]
        public object EventType { get; set; }
    }

    // TODO

    //[Exports(typeof(WebApiSubscription))]
    //internal partial class ServiceHookSubscriptionDataService : CollectionLevelController<WebApiSubscription>
    //{
    //    protected override IEnumerable<WebApiSubscription> DoGetItems()
    //    {
    //        var client = GetClient<ServiceHooksPublisherHttpClient>();
    //        var subscription = parameters.Get<object>(nameof(GetServiceHookSubscription.Subscription));
    //        var eventType = parameters.Get<string>(nameof(GetServiceHookSubscription.EventType));

    //        var publisher = HasParameter(nameof(GetServiceHookSubscription.Publisher))?
    //             GetItem<WebApiPublisher>(): null;
    //        var consumer = HasParameter(nameof(GetServiceHookSubscription.Consumer))?
    //            GetItem<WebApiConsumer>(): null;

    //        while (true) switch (subscription)
    //            {
    //                case WebApiSubscription p:
    //                    {
    //                        yield return p;
    //                        yield break;
    //                    }
    //                case string s:
    //                    {
    //                        var query = new SubscriptionsQuery() {
    //                            PublisherId = publisher?.Id,
    //                            ConsumerId = consumer?.Id,
    //                            EventType = eventType
    //                        };

    //                        var result = client.QuerySubscriptionsAsync(query)
    //                            .GetResult("Error getting service hook subscriptions")
    //                            .Results
    //                            .Where(sub => sub.ActionDescription.IsLike(s));

    //                        foreach (var r in result) yield return r;
    //                        yield break;
    //                    }
    //                default:
    //                    {
    //                        throw new ArgumentException($"Invalid or non-existent Subscription '{subscription}'");
    //                    }
    //            }
    //    }
    //}
}