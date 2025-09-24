//HintName: TfsCmdlets.HttpClients.ITeamAdminHttpClient.g.cs
#pragma warning disable CS8669
using System.Composition;
using System.Runtime.Serialization;
namespace TfsCmdlets.HttpClients
{
    public partial interface ITeamAdminHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
		public System.Collections.Generic.IEnumerable<TfsCmdlets.HttpClients.TeamAdmin> AddTeamAdmin(System.Guid projectId, System.Guid teamId, System.Collections.Generic.IEnumerable<System.Guid> userIds);
		public System.Collections.Generic.IEnumerable<TfsCmdlets.HttpClients.TeamAdmin> AddTeamAdmin(string project, System.Guid teamId, System.Collections.Generic.IEnumerable<System.Guid> userIds);
		public bool RemoveTeamAdmin(System.Guid project, System.Guid teamId, System.Guid userId);
		public bool RemoveTeamAdmin(string project, System.Guid teamId, System.Guid userId);
		public System.Uri GetUri();
		public T Get<T>(string apiPath, string apiVersion = "1.0", System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string mediaType = "application/json", object userState = null);
		public System.Net.Http.HttpResponseMessage Get(string apiPath, string apiVersion = "1.0", System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string mediaType = "application/json", object userState = null);
		public TResult Post<T, TResult>(string apiPath, T value, string apiVersion = "1.0", System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string mediaType = "application/json", object userState = null);
		public System.Net.Http.HttpResponseMessage Post(string apiPath, System.Net.Http.HttpContent content, string apiVersion = "1.0", System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string mediaType = "application/json", object userState = null);
		public System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> InvokeAsync(System.Net.Http.HttpMethod method, string apiPath, string content = null, string requestMediaType = "application/json", string responseMediaType = "application/json", System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string apiVersion = "1.0", object userState = null);
		public System.Threading.Tasks.Task<T> InvokeAsync<T>(System.Net.Http.HttpMethod method, string apiPath, string content = null, string requestMediaType = "application/json", string responseMediaType = "application/json", System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string apiVersion = "1.0", object userState = null);
		public T PostForm<T>(string formPath, System.Collections.Generic.Dictionary<string, string> formData, bool sendVerificationToken = false, string tokenRequestPath = null, System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string responseMediaType = "text/html", object userState = null);
    }
    [Export(typeof(ITeamAdminHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class ITeamAdminHttpClientImpl: ITeamAdminHttpClient
    {
        private TfsCmdlets.HttpClients.TeamAdminHttpClient _client;
        protected IDataManager Data { get; }
        [ImportingConstructor]
        public ITeamAdminHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        private TfsCmdlets.HttpClients.TeamAdminHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(TfsCmdlets.HttpClients.TeamAdminHttpClient)) as TfsCmdlets.HttpClients.TeamAdminHttpClient;
                }
                return _client;
            }
        }
		public System.Collections.Generic.IEnumerable<TfsCmdlets.HttpClients.TeamAdmin> AddTeamAdmin(System.Guid projectId, System.Guid teamId, System.Collections.Generic.IEnumerable<System.Guid> userIds)
			=> Client.AddTeamAdmin(projectId, teamId, userIds);
		public System.Collections.Generic.IEnumerable<TfsCmdlets.HttpClients.TeamAdmin> AddTeamAdmin(string project, System.Guid teamId, System.Collections.Generic.IEnumerable<System.Guid> userIds)
			=> Client.AddTeamAdmin(project, teamId, userIds);
		public bool RemoveTeamAdmin(System.Guid project, System.Guid teamId, System.Guid userId)
			=> Client.RemoveTeamAdmin(project, teamId, userId);
		public bool RemoveTeamAdmin(string project, System.Guid teamId, System.Guid userId)
			=> Client.RemoveTeamAdmin(project, teamId, userId);
		public System.Uri GetUri()
			=> Client.GetUri();
		public T Get<T>(string apiPath, string apiVersion = "1.0", System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string mediaType = "application/json", object userState = null)
			=> Client.Get<T>(apiPath, apiVersion, additionalHeaders, queryParameters, mediaType, userState);
		public System.Net.Http.HttpResponseMessage Get(string apiPath, string apiVersion = "1.0", System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string mediaType = "application/json", object userState = null)
			=> Client.Get(apiPath, apiVersion, additionalHeaders, queryParameters, mediaType, userState);
		public TResult Post<T, TResult>(string apiPath, T value, string apiVersion = "1.0", System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string mediaType = "application/json", object userState = null)
			=> Client.Post<T, TResult>(apiPath, value, apiVersion, additionalHeaders, queryParameters, mediaType, userState);
		public System.Net.Http.HttpResponseMessage Post(string apiPath, System.Net.Http.HttpContent content, string apiVersion = "1.0", System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string mediaType = "application/json", object userState = null)
			=> Client.Post(apiPath, content, apiVersion, additionalHeaders, queryParameters, mediaType, userState);
		public System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> InvokeAsync(System.Net.Http.HttpMethod method, string apiPath, string content = null, string requestMediaType = "application/json", string responseMediaType = "application/json", System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string apiVersion = "1.0", object userState = null)
			=> Client.InvokeAsync(method, apiPath, content, requestMediaType, responseMediaType, additionalHeaders, queryParameters, apiVersion, userState);
		public System.Threading.Tasks.Task<T> InvokeAsync<T>(System.Net.Http.HttpMethod method, string apiPath, string content = null, string requestMediaType = "application/json", string responseMediaType = "application/json", System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string apiVersion = "1.0", object userState = null)
			=> Client.InvokeAsync<T>(method, apiPath, content, requestMediaType, responseMediaType, additionalHeaders, queryParameters, apiVersion, userState);
		public T PostForm<T>(string formPath, System.Collections.Generic.Dictionary<string, string> formData, bool sendVerificationToken = false, string tokenRequestPath = null, System.Collections.Generic.IDictionary<string, string> additionalHeaders = null, System.Collections.Generic.IDictionary<string, string> queryParameters = null, string responseMediaType = "text/html", object userState = null)
			=> Client.PostForm<T>(formPath, formData, sendVerificationToken, tokenRequestPath, additionalHeaders, queryParameters, responseMediaType, userState);
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