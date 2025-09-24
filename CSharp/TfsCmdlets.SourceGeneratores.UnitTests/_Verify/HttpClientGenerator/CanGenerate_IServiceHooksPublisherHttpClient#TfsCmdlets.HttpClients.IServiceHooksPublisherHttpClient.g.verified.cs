//HintName: TfsCmdlets.HttpClients.IServiceHooksPublisherHttpClient.g.cs
#pragma warning disable CS8669
using System.Composition;
using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;
namespace TfsCmdlets.HttpClients
{
    public partial interface IServiceHooksPublisherHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher> GetPublisherAsync(string publisherId, object userState = null);
		public System.Threading.Tasks.Task<System.Collections.Generic.IList<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher>> GetPublishersAsync(object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.PublishersQuery> QueryPublishersAsync(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.PublishersQuery query, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer> GetConsumerAsync(string consumerId, object userState = null);
		public System.Threading.Tasks.Task<System.Collections.Generic.IList<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer>> GetConsumersAsync(object userState = null);
		public System.Threading.Tasks.Task<System.Collections.Generic.IList<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ConsumerAction>> GetConsumerActionsAsync(string consumerId, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ConsumerAction> GetConsumerActionAsync(string consumerId, string consumerActionId, object userState = null);
		public System.Threading.Tasks.Task<System.Collections.Generic.IList<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription>> QuerySubscriptionsAsync(string publisherId = null, System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.FormInput.InputFilter> inputFilters = null, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.SubscriptionsQuery> QuerySubscriptionsAsync(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.SubscriptionsQuery subscriptionsQuery, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription> GetSubscriptionAsync(System.Guid subscriptionId, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription> CreateSubscriptionAsync(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription subscription, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription> UpdateSubscriptionAsync(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription subscription, object userState = null);
		public System.Threading.Tasks.Task DeleteSubscriptionAsync(System.Guid subscriptionId, object userState = null);
		public System.Threading.Tasks.Task<System.Collections.Generic.IList<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification>> GetNotifications(System.Guid subscriptionId, int? maxResults, Microsoft.VisualStudio.Services.ServiceHooks.WebApi.NotificationStatus? status, Microsoft.VisualStudio.Services.ServiceHooks.WebApi.NotificationResult? result, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification> GetNotification(System.Guid subscriptionId, int notificationId, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.NotificationsQuery> QueryNotificationsAsync(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.NotificationsQuery query, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification> PostTestNotification(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification notification, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.SubscriptionInputValuesQuery> QueryInputValuesAsync(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.SubscriptionInputValuesQuery query, object userState = null);
    }
    [Export(typeof(IServiceHooksPublisherHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class IServiceHooksPublisherHttpClientImpl: IServiceHooksPublisherHttpClient
    {
        private Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient _client;
        protected IDataManager Data { get; }
        [ImportingConstructor]
        public IServiceHooksPublisherHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        private Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient)) as Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ServiceHooksPublisherHttpClient;
                }
                return _client;
            }
        }
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher> GetPublisherAsync(string publisherId, object userState = null)
			=> Client.GetPublisherAsync(publisherId, userState);
		public System.Threading.Tasks.Task<System.Collections.Generic.IList<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Publisher>> GetPublishersAsync(object userState = null)
			=> Client.GetPublishersAsync(userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.PublishersQuery> QueryPublishersAsync(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.PublishersQuery query, object userState = null)
			=> Client.QueryPublishersAsync(query, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer> GetConsumerAsync(string consumerId, object userState = null)
			=> Client.GetConsumerAsync(consumerId, userState);
		public System.Threading.Tasks.Task<System.Collections.Generic.IList<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Consumer>> GetConsumersAsync(object userState = null)
			=> Client.GetConsumersAsync(userState);
		public System.Threading.Tasks.Task<System.Collections.Generic.IList<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ConsumerAction>> GetConsumerActionsAsync(string consumerId, object userState = null)
			=> Client.GetConsumerActionsAsync(consumerId, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.ConsumerAction> GetConsumerActionAsync(string consumerId, string consumerActionId, object userState = null)
			=> Client.GetConsumerActionAsync(consumerId, consumerActionId, userState);
		public System.Threading.Tasks.Task<System.Collections.Generic.IList<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription>> QuerySubscriptionsAsync(string publisherId = null, System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.FormInput.InputFilter> inputFilters = null, object userState = null)
			=> Client.QuerySubscriptionsAsync(publisherId, inputFilters, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.SubscriptionsQuery> QuerySubscriptionsAsync(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.SubscriptionsQuery subscriptionsQuery, object userState = null)
			=> Client.QuerySubscriptionsAsync(subscriptionsQuery, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription> GetSubscriptionAsync(System.Guid subscriptionId, object userState = null)
			=> Client.GetSubscriptionAsync(subscriptionId, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription> CreateSubscriptionAsync(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription subscription, object userState = null)
			=> Client.CreateSubscriptionAsync(subscription, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription> UpdateSubscriptionAsync(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Subscription subscription, object userState = null)
			=> Client.UpdateSubscriptionAsync(subscription, userState);
		public System.Threading.Tasks.Task DeleteSubscriptionAsync(System.Guid subscriptionId, object userState = null)
			=> Client.DeleteSubscriptionAsync(subscriptionId, userState);
		public System.Threading.Tasks.Task<System.Collections.Generic.IList<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification>> GetNotifications(System.Guid subscriptionId, int? maxResults, Microsoft.VisualStudio.Services.ServiceHooks.WebApi.NotificationStatus? status, Microsoft.VisualStudio.Services.ServiceHooks.WebApi.NotificationResult? result, object userState = null)
			=> Client.GetNotifications(subscriptionId, maxResults, status, result, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification> GetNotification(System.Guid subscriptionId, int notificationId, object userState = null)
			=> Client.GetNotification(subscriptionId, notificationId, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.NotificationsQuery> QueryNotificationsAsync(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.NotificationsQuery query, object userState = null)
			=> Client.QueryNotificationsAsync(query, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification> PostTestNotification(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.Notification notification, object userState = null)
			=> Client.PostTestNotification(notification, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ServiceHooks.WebApi.SubscriptionInputValuesQuery> QueryInputValuesAsync(Microsoft.VisualStudio.Services.ServiceHooks.WebApi.SubscriptionInputValuesQuery query, object userState = null)
			=> Client.QueryInputValuesAsync(query, userState);
        public Uri BaseAddress
           => Client.BaseAddress;
        public bool ExcludeUrlsHeader
        {
           get => Client.ExcludeUrlsHeader;
           set => Client.ExcludeUrlsHeader = value;
        }
        public Microsoft.VisualStudio.Services.WebApi.VssResponseContext LastResponseContext
           => Client.LastResponseContext;
        public bool LightweightHeader
        {
           get => Client.LightweightHeader;
           set => Client.LightweightHeader = value;
        }
        public bool IsDisposed()
           => Client.IsDisposed();
        public void SetResourceLocations(Microsoft.VisualStudio.Services.WebApi.ApiResourceLocationCollection resourceLocations)
           => Client.SetResourceLocations(resourceLocations);
        public void Dispose()
	        => Client.Dispose();
   }
}