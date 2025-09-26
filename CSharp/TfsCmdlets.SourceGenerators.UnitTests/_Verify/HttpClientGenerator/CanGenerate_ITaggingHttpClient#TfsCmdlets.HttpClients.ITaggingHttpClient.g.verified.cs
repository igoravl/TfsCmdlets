//HintName: TfsCmdlets.HttpClients.ITaggingHttpClient.g.cs
#pragma warning disable CS8669
using System.Composition;
using Microsoft.TeamFoundation.Core.WebApi;
namespace TfsCmdlets.HttpClients
{
    public partial interface ITaggingHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition> GetTagAsync(System.Guid scopeId, System.Guid tagId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition> GetTagAsync(System.Guid scopeId, string name, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinitionList> GetTagsAsync(System.Guid scopeId, bool includeInactive = false, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition> CreateTagAsync(System.Guid scopeId, string name, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition> UpdateTagAsync(System.Guid scopeId, System.Guid tagId, string name, bool? active, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task DeleteTagAsync(System.Guid scopeId, System.Guid tagId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
    [Export(typeof(ITaggingHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class ITaggingHttpClientImpl: ITaggingHttpClient
    {
        private Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient _client;
        protected IDataManager Data { get; }
        [ImportingConstructor]
        public ITaggingHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        private Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient)) as Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient;
                }
                return _client;
            }
        }
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition> GetTagAsync(System.Guid scopeId, System.Guid tagId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetTagAsync(scopeId, tagId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition> GetTagAsync(System.Guid scopeId, string name, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetTagAsync(scopeId, name, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinitionList> GetTagsAsync(System.Guid scopeId, bool includeInactive = false, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetTagsAsync(scopeId, includeInactive, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition> CreateTagAsync(System.Guid scopeId, string name, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.CreateTagAsync(scopeId, name, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition> UpdateTagAsync(System.Guid scopeId, System.Guid tagId, string name, bool? active, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.UpdateTagAsync(scopeId, tagId, name, active, userState, cancellationToken);
		public System.Threading.Tasks.Task DeleteTagAsync(System.Guid scopeId, System.Guid tagId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.DeleteTagAsync(scopeId, tagId, userState, cancellationToken);
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