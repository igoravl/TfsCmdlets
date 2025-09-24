//HintName: TfsCmdlets.HttpClients.IProjectHttpClient.g.cs
#pragma warning disable CS8669
using System.Composition;
using Microsoft.TeamFoundation.Core.WebApi;
namespace TfsCmdlets.HttpClients
{
    public partial interface IProjectHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.TeamProject> GetProject(string id, bool? includeCapabilities = default(bool?), bool includeHistory = false, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.WebApi.IPagedList<Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference>> GetProjects(Microsoft.TeamFoundation.Core.WebApi.ProjectState? stateFilter, int? top, int? skip, object userState, string continuationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.WebApi.IPagedList<Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference>> GetProjects(Microsoft.TeamFoundation.Core.WebApi.ProjectState? stateFilter = default(Microsoft.TeamFoundation.Core.WebApi.ProjectState?), int? top = default(int?), int? skip = default(int?), object userState = null, string continuationToken = null, bool? getDefaultTeamImageUrl = default(bool?));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.OperationReference> QueueCreateProject(Microsoft.TeamFoundation.Core.WebApi.TeamProject projectToCreate, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.OperationReference> QueueDeleteProject(System.Guid projectId, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.OperationReference> QueueDeleteProject(System.Guid projectId, bool hardDelete, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.OperationReference> UpdateProject(System.Guid projectToUpdateId, Microsoft.TeamFoundation.Core.WebApi.TeamProject projectUpdate, object userState = null);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.WebApi.ApiResourceLocation> Options();
		public System.Threading.Tasks.Task RemoveProjectAvatarAsync(string projectId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task SetProjectAvatarAsync(Microsoft.TeamFoundation.Core.WebApi.ProjectAvatar avatarBlob, string projectId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.ProjectInfo>> GetProjectHistoryEntriesAsync(long? minRevision = default(long?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.ProjectProperties>> GetProjectsPropertiesAsync(System.Collections.Generic.IEnumerable<System.Guid> projectIds, System.Collections.Generic.IEnumerable<string> properties = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.ProjectProperty>> GetProjectPropertiesAsync(System.Guid projectId, System.Collections.Generic.IEnumerable<string> keys = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task SetProjectPropertiesAsync(System.Guid projectId, Microsoft.VisualStudio.Services.WebApi.Patch.Json.JsonPatchDocument patchDocument, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
    [Export(typeof(IProjectHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class IProjectHttpClientImpl: IProjectHttpClient
    {
        private Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient _client;
        protected IDataManager Data { get; }
        [ImportingConstructor]
        public IProjectHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        private Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient)) as Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient;
                }
                return _client;
            }
        }
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.TeamProject> GetProject(string id, bool? includeCapabilities = default(bool?), bool includeHistory = false, object userState = null)
			=> Client.GetProject(id, includeCapabilities, includeHistory, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.WebApi.IPagedList<Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference>> GetProjects(Microsoft.TeamFoundation.Core.WebApi.ProjectState? stateFilter, int? top, int? skip, object userState, string continuationToken)
			=> Client.GetProjects(stateFilter, top, skip, userState, continuationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.WebApi.IPagedList<Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference>> GetProjects(Microsoft.TeamFoundation.Core.WebApi.ProjectState? stateFilter = default(Microsoft.TeamFoundation.Core.WebApi.ProjectState?), int? top = default(int?), int? skip = default(int?), object userState = null, string continuationToken = null, bool? getDefaultTeamImageUrl = default(bool?))
			=> Client.GetProjects(stateFilter, top, skip, userState, continuationToken, getDefaultTeamImageUrl);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.OperationReference> QueueCreateProject(Microsoft.TeamFoundation.Core.WebApi.TeamProject projectToCreate, object userState = null)
			=> Client.QueueCreateProject(projectToCreate, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.OperationReference> QueueDeleteProject(System.Guid projectId, object userState = null)
			=> Client.QueueDeleteProject(projectId, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.OperationReference> QueueDeleteProject(System.Guid projectId, bool hardDelete, object userState = null)
			=> Client.QueueDeleteProject(projectId, hardDelete, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.OperationReference> UpdateProject(System.Guid projectToUpdateId, Microsoft.TeamFoundation.Core.WebApi.TeamProject projectUpdate, object userState = null)
			=> Client.UpdateProject(projectToUpdateId, projectUpdate, userState);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.WebApi.ApiResourceLocation> Options()
			=> Client.Options();
		public System.Threading.Tasks.Task RemoveProjectAvatarAsync(string projectId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.RemoveProjectAvatarAsync(projectId, userState, cancellationToken);
		public System.Threading.Tasks.Task SetProjectAvatarAsync(Microsoft.TeamFoundation.Core.WebApi.ProjectAvatar avatarBlob, string projectId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.SetProjectAvatarAsync(avatarBlob, projectId, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.ProjectInfo>> GetProjectHistoryEntriesAsync(long? minRevision = default(long?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetProjectHistoryEntriesAsync(minRevision, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.ProjectProperties>> GetProjectsPropertiesAsync(System.Collections.Generic.IEnumerable<System.Guid> projectIds, System.Collections.Generic.IEnumerable<string> properties = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetProjectsPropertiesAsync(projectIds, properties, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.ProjectProperty>> GetProjectPropertiesAsync(System.Guid projectId, System.Collections.Generic.IEnumerable<string> keys = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetProjectPropertiesAsync(projectId, keys, userState, cancellationToken);
		public System.Threading.Tasks.Task SetProjectPropertiesAsync(System.Guid projectId, Microsoft.VisualStudio.Services.WebApi.Patch.Json.JsonPatchDocument patchDocument, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.SetProjectPropertiesAsync(projectId, patchDocument, userState, cancellationToken);
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