using System.Management.Automation;
using WebApiConsumer = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer;
using WebApiPublisher = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher;
using WebApiSubscription = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    /// <summary>
    /// Gets one or more service hook subscriptions
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WebApiSubscription))]
    partial class GetServiceHookSubscription
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
        [Parameter]
        public object Publisher { get; set; }

        /// <summary>
        /// Specifies the name or ID of the service hook consumer to filter subscriptions by. 
        /// When omitted, returns all subscriptions regardless of their consumers.
        /// </summary>
        [Parameter]
        public object Consumer { get; set; }

        /// <summary>
        /// Specifies the event type to filter subscriptions by. 
        /// When omitted, returns all subscriptions regardless of their event types.
        /// </summary>
        [Parameter]
        public string EventType { get; set; }
    }

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