//HintName: TfsCmdlets.HttpClients.ISearchHttpClient.g.cs
#pragma warning disable CS8669
using System.Composition;
using Microsoft.VisualStudio.Services.Search.WebApi;
namespace TfsCmdlets.HttpClients
{
    public partial interface ISearchHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchAdvancedCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchRequest request, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchAdvancedCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchRequest request, string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchAdvancedCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchRequest request, System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchScrollCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.ScrollSearchRequest request, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchScrollCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.ScrollSearchRequest request, string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchScrollCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.ScrollSearchRequest request, System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchRequest request, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchRequest request, string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchRequest request, System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.RepositoryStatus.CustomRepositoryStatusResponse> GetCustomRepositoryStatusAsync(string project, string repository, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.RepositoryStatus.CustomRepositoryStatusResponse> GetCustomRepositoryStatusAsync(System.Guid project, string repository, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.RepositoryStatus.CustomRepositoryBranchStatusResponse> GetCustomRepositoryBranchStatusAsync(string project, string repository, string branch, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.RepositoryStatus.CustomRepositoryBranchStatusResponse> GetCustomRepositoryBranchStatusAsync(System.Guid project, string repository, string branch, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Package.PackageSearchResponse> FetchPackageSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Package.PackageSearchRequest request, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.RepositoryStatus.RepositoryStatusResponse> GetRepositoryStatusAsync(string project, string repository, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.RepositoryStatus.RepositoryStatusResponse> GetRepositoryStatusAsync(System.Guid project, string repository, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.TfvcRepositoryStatus.TfvcRepositoryStatusResponse> GetTfvcRepositoryStatusAsync(string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.TfvcRepositoryStatus.TfvcRepositoryStatusResponse> GetTfvcRepositoryStatusAsync(System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki.WikiSearchResponse> FetchWikiSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki.WikiSearchRequest request, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki.WikiSearchResponse> FetchWikiSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki.WikiSearchRequest request, string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki.WikiSearchResponse> FetchWikiSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki.WikiSearchRequest request, System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem.WorkItemSearchResponse> FetchWorkItemSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem.WorkItemSearchRequest request, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem.WorkItemSearchResponse> FetchWorkItemSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem.WorkItemSearchRequest request, string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem.WorkItemSearchResponse> FetchWorkItemSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem.WorkItemSearchRequest request, System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
    [Export(typeof(ISearchHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class ISearchHttpClientImpl: ISearchHttpClient
    {
        private Microsoft.VisualStudio.Services.Search.WebApi.SearchHttpClient _client;
        protected IDataManager Data { get; }
        [ImportingConstructor]
        public ISearchHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        private Microsoft.VisualStudio.Services.Search.WebApi.SearchHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(Microsoft.VisualStudio.Services.Search.WebApi.SearchHttpClient)) as Microsoft.VisualStudio.Services.Search.WebApi.SearchHttpClient;
                }
                return _client;
            }
        }
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchAdvancedCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchRequest request, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchAdvancedCodeSearchResultsAsync(request, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchAdvancedCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchRequest request, string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchAdvancedCodeSearchResultsAsync(request, project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchAdvancedCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchRequest request, System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchAdvancedCodeSearchResultsAsync(request, project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchScrollCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.ScrollSearchRequest request, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchScrollCodeSearchResultsAsync(request, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchScrollCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.ScrollSearchRequest request, string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchScrollCodeSearchResultsAsync(request, project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchScrollCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.ScrollSearchRequest request, System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchScrollCodeSearchResultsAsync(request, project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchRequest request, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchCodeSearchResultsAsync(request, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchRequest request, string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchCodeSearchResultsAsync(request, project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchResponse> FetchCodeSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.Code.CodeSearchRequest request, System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchCodeSearchResultsAsync(request, project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.RepositoryStatus.CustomRepositoryStatusResponse> GetCustomRepositoryStatusAsync(string project, string repository, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetCustomRepositoryStatusAsync(project, repository, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.RepositoryStatus.CustomRepositoryStatusResponse> GetCustomRepositoryStatusAsync(System.Guid project, string repository, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetCustomRepositoryStatusAsync(project, repository, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.RepositoryStatus.CustomRepositoryBranchStatusResponse> GetCustomRepositoryBranchStatusAsync(string project, string repository, string branch, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetCustomRepositoryBranchStatusAsync(project, repository, branch, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.RepositoryStatus.CustomRepositoryBranchStatusResponse> GetCustomRepositoryBranchStatusAsync(System.Guid project, string repository, string branch, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetCustomRepositoryBranchStatusAsync(project, repository, branch, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Package.PackageSearchResponse> FetchPackageSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Package.PackageSearchRequest request, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchPackageSearchResultsAsync(request, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.RepositoryStatus.RepositoryStatusResponse> GetRepositoryStatusAsync(string project, string repository, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetRepositoryStatusAsync(project, repository, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.RepositoryStatus.RepositoryStatusResponse> GetRepositoryStatusAsync(System.Guid project, string repository, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetRepositoryStatusAsync(project, repository, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.TfvcRepositoryStatus.TfvcRepositoryStatusResponse> GetTfvcRepositoryStatusAsync(string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetTfvcRepositoryStatusAsync(project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.TfvcRepositoryStatus.TfvcRepositoryStatusResponse> GetTfvcRepositoryStatusAsync(System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetTfvcRepositoryStatusAsync(project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki.WikiSearchResponse> FetchWikiSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki.WikiSearchRequest request, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchWikiSearchResultsAsync(request, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki.WikiSearchResponse> FetchWikiSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki.WikiSearchRequest request, string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchWikiSearchResultsAsync(request, project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki.WikiSearchResponse> FetchWikiSearchResultsAsync(Microsoft.VisualStudio.Services.Search.Shared.WebApi.Contracts.Wiki.WikiSearchRequest request, System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchWikiSearchResultsAsync(request, project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem.WorkItemSearchResponse> FetchWorkItemSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem.WorkItemSearchRequest request, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchWorkItemSearchResultsAsync(request, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem.WorkItemSearchResponse> FetchWorkItemSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem.WorkItemSearchRequest request, string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchWorkItemSearchResultsAsync(request, project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem.WorkItemSearchResponse> FetchWorkItemSearchResultsAsync(Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem.WorkItemSearchRequest request, System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.FetchWorkItemSearchResultsAsync(request, project, userState, cancellationToken);
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