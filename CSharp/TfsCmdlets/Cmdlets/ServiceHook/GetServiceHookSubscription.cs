using System.Management.Automation;
using WebApiConsumer = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer;
using WebApiPublisher = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher;
using WebApiSubscription = Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription;

namespace TfsCmdlets.Cmdlets.ServiceHook
{
    /// <summary>
    /// Gets one or more service hook subscriptions
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsServiceHookSubscription")]
    [OutputType(typeof(WebApiSubscription))]
    [TfsCmdlet(CmdletScope.Collection)]
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
        public object EventType { get; set; }
    }
}