//HintName: TfsCmdlets.HttpClients.ITeamHttpClient.g.cs
#pragma warning disable CS8669
using System.Composition;
using Microsoft.TeamFoundation.Core.WebApi;
namespace TfsCmdlets.HttpClients
{
    public partial interface ITeamHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.CategorizedWebApiTeams> GetProjectTeamsByCategoryAsync(string projectId, bool? expandIdentity = default(bool?), int? top = default(int?), int? skip = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.WebApi.TeamMember>> GetTeamMembersWithExtendedPropertiesAsync(string projectId, string teamId, int? top = default(int?), int? skip = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.WebApiTeam>> GetAllTeamsAsync(bool? mine = default(bool?), int? top = default(int?), int? skip = default(int?), bool? expandIdentity = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTeam> CreateTeamAsync(Microsoft.TeamFoundation.Core.WebApi.WebApiTeam team, string projectId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task DeleteTeamAsync(string projectId, string teamId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTeam> GetTeamAsync(string projectId, string teamId, bool? expandIdentity = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.WebApiTeam>> GetTeamsAsync(string projectId, bool? mine = default(bool?), int? top = default(int?), int? skip = default(int?), bool? expandIdentity = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTeam> UpdateTeamAsync(Microsoft.TeamFoundation.Core.WebApi.WebApiTeam teamData, string projectId, string teamId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
    [Export(typeof(ITeamHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class ITeamHttpClientImpl: ITeamHttpClient
    {
        private Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient _client;
        protected IDataManager Data { get; }
        [ImportingConstructor]
        public ITeamHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        private Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient)) as Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient;
                }
                return _client;
            }
        }
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.CategorizedWebApiTeams> GetProjectTeamsByCategoryAsync(string projectId, bool? expandIdentity = default(bool?), int? top = default(int?), int? skip = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetProjectTeamsByCategoryAsync(projectId, expandIdentity, top, skip, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.WebApi.TeamMember>> GetTeamMembersWithExtendedPropertiesAsync(string projectId, string teamId, int? top = default(int?), int? skip = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetTeamMembersWithExtendedPropertiesAsync(projectId, teamId, top, skip, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.WebApiTeam>> GetAllTeamsAsync(bool? mine = default(bool?), int? top = default(int?), int? skip = default(int?), bool? expandIdentity = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetAllTeamsAsync(mine, top, skip, expandIdentity, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTeam> CreateTeamAsync(Microsoft.TeamFoundation.Core.WebApi.WebApiTeam team, string projectId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.CreateTeamAsync(team, projectId, userState, cancellationToken);
		public System.Threading.Tasks.Task DeleteTeamAsync(string projectId, string teamId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.DeleteTeamAsync(projectId, teamId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTeam> GetTeamAsync(string projectId, string teamId, bool? expandIdentity = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetTeamAsync(projectId, teamId, expandIdentity, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.WebApiTeam>> GetTeamsAsync(string projectId, bool? mine = default(bool?), int? top = default(int?), int? skip = default(int?), bool? expandIdentity = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetTeamsAsync(projectId, mine, top, skip, expandIdentity, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.WebApiTeam> UpdateTeamAsync(Microsoft.TeamFoundation.Core.WebApi.WebApiTeam teamData, string projectId, string teamId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.UpdateTeamAsync(teamData, projectId, teamId, userState, cancellationToken);
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