//HintName: TfsCmdlets.HttpClients.IGraphHttpClient.g.cs
#pragma warning disable CS8669
using System.Composition;
using Microsoft.VisualStudio.Services.Graph.Client;
namespace TfsCmdlets.HttpClients
{
    public partial interface IGraphHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
		public System.Threading.Tasks.Task DeleteAvatarAsync(string subjectDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Profile.Avatar> GetAvatarAsync(string subjectDescriptor, Microsoft.VisualStudio.Services.Profile.AvatarSize? size = default(Microsoft.VisualStudio.Services.Profile.AvatarSize?), string format = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task SetAvatarAsync(Microsoft.VisualStudio.Services.Profile.Avatar avatar, string subjectDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphCachePolicies> GetCachePoliciesAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphDescriptorResult> GetDescriptorAsync(System.Guid storageKey, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.GraphFederatedProviderData> GetFederatedProviderDataAsync(Microsoft.VisualStudio.Services.Common.SubjectDescriptor subjectDescriptor, string providerName, long? versionHint = default(long?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphGroup> CreateGroupAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphGroupCreationContext creationContext, string scopeDescriptor = null, System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Common.SubjectDescriptor> groupDescriptors = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task DeleteGroupAsync(string groupDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphGroup> GetGroupAsync(string groupDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.PagedGraphGroups> ListGroupsAsync(string scopeDescriptor = null, System.Collections.Generic.IEnumerable<string> subjectTypes = null, string continuationToken = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphGroup> UpdateGroupAsync(string groupDescriptor, Microsoft.VisualStudio.Services.WebApi.Patch.Json.JsonPatchDocument patchDocument, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Guid> TranslateAsync(string masterId = null, string localId = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyDictionary<Microsoft.VisualStudio.Services.Common.SubjectDescriptor, Microsoft.VisualStudio.Services.Graph.Client.GraphMember>> LookupMembersAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphSubjectLookup memberLookup, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.PagedGraphMembers> ListMembersAsync(string continuationToken = null, System.Collections.Generic.IEnumerable<string> subjectTypes = null, System.Collections.Generic.IEnumerable<string> subjectKinds = null, System.Collections.Generic.IEnumerable<string> metaTypes = null, string scopeDescriptor = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphMember> GetMemberByDescriptorAsync(string memberDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphMembership> AddMembershipAsync(string subjectDescriptor, string containerDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<bool> CheckMembershipExistenceAsync(string subjectDescriptor, string containerDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphMembership> GetMembershipAsync(string subjectDescriptor, string containerDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task RemoveMembershipAsync(string subjectDescriptor, string containerDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.Graph.Client.GraphMembership>> ListMembershipsAsync(string subjectDescriptor, Microsoft.VisualStudio.Services.Graph.GraphTraversalDirection? direction = default(Microsoft.VisualStudio.Services.Graph.GraphTraversalDirection?), int? depth = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphMembershipState> GetMembershipStateAsync(string subjectDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyDictionary<Microsoft.VisualStudio.Services.Common.SubjectDescriptor, Microsoft.VisualStudio.Services.Graph.Client.GraphMembershipTraversal>> LookupMembershipTraversalsAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphSubjectLookup membershipTraversalLookup, Microsoft.VisualStudio.Services.Graph.GraphTraversalDirection? direction = default(Microsoft.VisualStudio.Services.Graph.GraphTraversalDirection?), int? depth = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphMembershipTraversal> TraverseMembershipsAsync(string subjectDescriptor, Microsoft.VisualStudio.Services.Graph.GraphTraversalDirection? direction = default(Microsoft.VisualStudio.Services.Graph.GraphTraversalDirection?), int? depth = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphProviderInfo> GetProviderInfoAsync(string userDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task RequestAccessAsync(Newtonsoft.Json.Linq.JToken jsondocument, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.ResolveDisconnectedUsersResponse> ResolveAsync(Microsoft.VisualStudio.Services.Graph.Client.IdentityMappings mappings, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphScope> CreateScopeAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphScopeCreationContext creationContext, string scopeDescriptor = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task DeleteScopeAsync(string scopeDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphScope> GetScopeAsync(string scopeDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task UpdateScopeAsync(string scopeDescriptor, Microsoft.VisualStudio.Services.WebApi.Patch.Json.JsonPatchDocument patchDocument, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphServicePrincipal> CreateServicePrincipalAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphServicePrincipalCreationContext creationContext, System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Common.SubjectDescriptor> groupDescriptors = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task DeleteServicePrincipalAsync(string servicePrincipalDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphServicePrincipal> GetServicePrincipalAsync(string servicePrincipalDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.PagedGraphServicePrincipals> ListServicePrincipalsAsync(string continuationToken = null, string scopeDescriptor = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphServicePrincipal> UpdateServicePrincipalAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphServicePrincipalUpdateContext updateContext, string servicePrincipalDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphStorageKeyResult> GetStorageKeyAsync(string subjectDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyDictionary<Microsoft.VisualStudio.Services.Common.SubjectDescriptor, Microsoft.VisualStudio.Services.Graph.Client.GraphSubject>> LookupSubjectsAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphSubjectLookup subjectLookup, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.Graph.Client.GraphSubject>> QuerySubjectsAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphSubjectQuery subjectQuery, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphSubject> GetSubjectAsync(string subjectDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphUser> CreateUserAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphUserCreationContext creationContext, System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Common.SubjectDescriptor> groupDescriptors = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task DeleteUserAsync(string userDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphUser> GetUserAsync(string userDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.PagedGraphUsers> ListUsersAsync(System.Collections.Generic.IEnumerable<string> subjectTypes = null, string continuationToken = null, string scopeDescriptor = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphUser> UpdateUserAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphUserUpdateContext updateContext, string userDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.PagedGraphUsers> ListUsersAsync(System.Collections.Generic.IEnumerable<string> subjectTypes = null, string continuationToken = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
    [Export(typeof(IGraphHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class IGraphHttpClientImpl: IGraphHttpClient
    {
        private Microsoft.VisualStudio.Services.Graph.Client.GraphHttpClient _client;
        protected IDataManager Data { get; }
        [ImportingConstructor]
        public IGraphHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        private Microsoft.VisualStudio.Services.Graph.Client.GraphHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(Microsoft.VisualStudio.Services.Graph.Client.GraphHttpClient)) as Microsoft.VisualStudio.Services.Graph.Client.GraphHttpClient;
                }
                return _client;
            }
        }
		public System.Threading.Tasks.Task DeleteAvatarAsync(string subjectDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.DeleteAvatarAsync(subjectDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Profile.Avatar> GetAvatarAsync(string subjectDescriptor, Microsoft.VisualStudio.Services.Profile.AvatarSize? size = default(Microsoft.VisualStudio.Services.Profile.AvatarSize?), string format = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetAvatarAsync(subjectDescriptor, size, format, userState, cancellationToken);
		public System.Threading.Tasks.Task SetAvatarAsync(Microsoft.VisualStudio.Services.Profile.Avatar avatar, string subjectDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.SetAvatarAsync(avatar, subjectDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphCachePolicies> GetCachePoliciesAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetCachePoliciesAsync(userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphDescriptorResult> GetDescriptorAsync(System.Guid storageKey, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetDescriptorAsync(storageKey, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.GraphFederatedProviderData> GetFederatedProviderDataAsync(Microsoft.VisualStudio.Services.Common.SubjectDescriptor subjectDescriptor, string providerName, long? versionHint = default(long?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetFederatedProviderDataAsync(subjectDescriptor, providerName, versionHint, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphGroup> CreateGroupAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphGroupCreationContext creationContext, string scopeDescriptor = null, System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Common.SubjectDescriptor> groupDescriptors = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.CreateGroupAsync(creationContext, scopeDescriptor, groupDescriptors, userState, cancellationToken);
		public System.Threading.Tasks.Task DeleteGroupAsync(string groupDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.DeleteGroupAsync(groupDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphGroup> GetGroupAsync(string groupDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetGroupAsync(groupDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.PagedGraphGroups> ListGroupsAsync(string scopeDescriptor = null, System.Collections.Generic.IEnumerable<string> subjectTypes = null, string continuationToken = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.ListGroupsAsync(scopeDescriptor, subjectTypes, continuationToken, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphGroup> UpdateGroupAsync(string groupDescriptor, Microsoft.VisualStudio.Services.WebApi.Patch.Json.JsonPatchDocument patchDocument, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.UpdateGroupAsync(groupDescriptor, patchDocument, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Guid> TranslateAsync(string masterId = null, string localId = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.TranslateAsync(masterId, localId, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyDictionary<Microsoft.VisualStudio.Services.Common.SubjectDescriptor, Microsoft.VisualStudio.Services.Graph.Client.GraphMember>> LookupMembersAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphSubjectLookup memberLookup, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.LookupMembersAsync(memberLookup, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.PagedGraphMembers> ListMembersAsync(string continuationToken = null, System.Collections.Generic.IEnumerable<string> subjectTypes = null, System.Collections.Generic.IEnumerable<string> subjectKinds = null, System.Collections.Generic.IEnumerable<string> metaTypes = null, string scopeDescriptor = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.ListMembersAsync(continuationToken, subjectTypes, subjectKinds, metaTypes, scopeDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphMember> GetMemberByDescriptorAsync(string memberDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetMemberByDescriptorAsync(memberDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphMembership> AddMembershipAsync(string subjectDescriptor, string containerDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.AddMembershipAsync(subjectDescriptor, containerDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<bool> CheckMembershipExistenceAsync(string subjectDescriptor, string containerDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.CheckMembershipExistenceAsync(subjectDescriptor, containerDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphMembership> GetMembershipAsync(string subjectDescriptor, string containerDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetMembershipAsync(subjectDescriptor, containerDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task RemoveMembershipAsync(string subjectDescriptor, string containerDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.RemoveMembershipAsync(subjectDescriptor, containerDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.Graph.Client.GraphMembership>> ListMembershipsAsync(string subjectDescriptor, Microsoft.VisualStudio.Services.Graph.GraphTraversalDirection? direction = default(Microsoft.VisualStudio.Services.Graph.GraphTraversalDirection?), int? depth = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.ListMembershipsAsync(subjectDescriptor, direction, depth, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphMembershipState> GetMembershipStateAsync(string subjectDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetMembershipStateAsync(subjectDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyDictionary<Microsoft.VisualStudio.Services.Common.SubjectDescriptor, Microsoft.VisualStudio.Services.Graph.Client.GraphMembershipTraversal>> LookupMembershipTraversalsAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphSubjectLookup membershipTraversalLookup, Microsoft.VisualStudio.Services.Graph.GraphTraversalDirection? direction = default(Microsoft.VisualStudio.Services.Graph.GraphTraversalDirection?), int? depth = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.LookupMembershipTraversalsAsync(membershipTraversalLookup, direction, depth, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphMembershipTraversal> TraverseMembershipsAsync(string subjectDescriptor, Microsoft.VisualStudio.Services.Graph.GraphTraversalDirection? direction = default(Microsoft.VisualStudio.Services.Graph.GraphTraversalDirection?), int? depth = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.TraverseMembershipsAsync(subjectDescriptor, direction, depth, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphProviderInfo> GetProviderInfoAsync(string userDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetProviderInfoAsync(userDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task RequestAccessAsync(Newtonsoft.Json.Linq.JToken jsondocument, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.RequestAccessAsync(jsondocument, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.ResolveDisconnectedUsersResponse> ResolveAsync(Microsoft.VisualStudio.Services.Graph.Client.IdentityMappings mappings, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.ResolveAsync(mappings, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphScope> CreateScopeAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphScopeCreationContext creationContext, string scopeDescriptor = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.CreateScopeAsync(creationContext, scopeDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task DeleteScopeAsync(string scopeDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.DeleteScopeAsync(scopeDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphScope> GetScopeAsync(string scopeDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetScopeAsync(scopeDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task UpdateScopeAsync(string scopeDescriptor, Microsoft.VisualStudio.Services.WebApi.Patch.Json.JsonPatchDocument patchDocument, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.UpdateScopeAsync(scopeDescriptor, patchDocument, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphServicePrincipal> CreateServicePrincipalAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphServicePrincipalCreationContext creationContext, System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Common.SubjectDescriptor> groupDescriptors = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.CreateServicePrincipalAsync(creationContext, groupDescriptors, userState, cancellationToken);
		public System.Threading.Tasks.Task DeleteServicePrincipalAsync(string servicePrincipalDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.DeleteServicePrincipalAsync(servicePrincipalDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphServicePrincipal> GetServicePrincipalAsync(string servicePrincipalDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetServicePrincipalAsync(servicePrincipalDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.PagedGraphServicePrincipals> ListServicePrincipalsAsync(string continuationToken = null, string scopeDescriptor = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.ListServicePrincipalsAsync(continuationToken, scopeDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphServicePrincipal> UpdateServicePrincipalAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphServicePrincipalUpdateContext updateContext, string servicePrincipalDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.UpdateServicePrincipalAsync(updateContext, servicePrincipalDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphStorageKeyResult> GetStorageKeyAsync(string subjectDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetStorageKeyAsync(subjectDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyDictionary<Microsoft.VisualStudio.Services.Common.SubjectDescriptor, Microsoft.VisualStudio.Services.Graph.Client.GraphSubject>> LookupSubjectsAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphSubjectLookup subjectLookup, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.LookupSubjectsAsync(subjectLookup, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.Graph.Client.GraphSubject>> QuerySubjectsAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphSubjectQuery subjectQuery, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.QuerySubjectsAsync(subjectQuery, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphSubject> GetSubjectAsync(string subjectDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetSubjectAsync(subjectDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphUser> CreateUserAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphUserCreationContext creationContext, System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Common.SubjectDescriptor> groupDescriptors = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.CreateUserAsync(creationContext, groupDescriptors, userState, cancellationToken);
		public System.Threading.Tasks.Task DeleteUserAsync(string userDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.DeleteUserAsync(userDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphUser> GetUserAsync(string userDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetUserAsync(userDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.PagedGraphUsers> ListUsersAsync(System.Collections.Generic.IEnumerable<string> subjectTypes = null, string continuationToken = null, string scopeDescriptor = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.ListUsersAsync(subjectTypes, continuationToken, scopeDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.GraphUser> UpdateUserAsync(Microsoft.VisualStudio.Services.Graph.Client.GraphUserUpdateContext updateContext, string userDescriptor, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.UpdateUserAsync(updateContext, userDescriptor, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Graph.Client.PagedGraphUsers> ListUsersAsync(System.Collections.Generic.IEnumerable<string> subjectTypes = null, string continuationToken = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.ListUsersAsync(subjectTypes, continuationToken, userState, cancellationToken);
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