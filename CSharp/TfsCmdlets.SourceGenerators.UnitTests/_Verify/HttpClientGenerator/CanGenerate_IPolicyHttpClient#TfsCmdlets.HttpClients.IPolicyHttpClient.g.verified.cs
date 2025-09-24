//HintName: TfsCmdlets.HttpClients.IPolicyHttpClient.g.cs
#pragma warning disable CS8669
using System.Composition;
using Microsoft.TeamFoundation.Policy.WebApi;
namespace TfsCmdlets.HttpClients
{
    public partial interface IPolicyHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> CreatePolicyConfigurationAsync(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration configuration, string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> CreatePolicyConfigurationAsync(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration configuration, System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task DeletePolicyConfigurationAsync(string project, int configurationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task DeletePolicyConfigurationAsync(System.Guid project, int configurationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> GetPolicyConfigurationAsync(string project, int configurationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> GetPolicyConfigurationAsync(System.Guid project, int configurationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationsAsync(string project, string scope = null, System.Guid? policyType = default(System.Guid?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationsAsync(System.Guid project, string scope = null, System.Guid? policyType = default(System.Guid?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> UpdatePolicyConfigurationAsync(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration configuration, string project, int configurationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> UpdatePolicyConfigurationAsync(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration configuration, System.Guid project, int configurationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyEvaluationRecord> GetPolicyEvaluationAsync(string project, System.Guid evaluationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyEvaluationRecord> GetPolicyEvaluationAsync(System.Guid project, System.Guid evaluationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyEvaluationRecord> RequeuePolicyEvaluationAsync(string project, System.Guid evaluationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyEvaluationRecord> RequeuePolicyEvaluationAsync(System.Guid project, System.Guid evaluationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyEvaluationRecord>> GetPolicyEvaluationsAsync(string project, string artifactId, bool? includeNotApplicable = default(bool?), int? top = default(int?), int? skip = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyEvaluationRecord>> GetPolicyEvaluationsAsync(System.Guid project, string artifactId, bool? includeNotApplicable = default(bool?), int? top = default(int?), int? skip = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> GetPolicyConfigurationRevisionAsync(string project, int configurationId, int revisionId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> GetPolicyConfigurationRevisionAsync(System.Guid project, int configurationId, int revisionId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationRevisionsAsync(string project, int configurationId, int? top = default(int?), int? skip = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationRevisionsAsync(System.Guid project, int configurationId, int? top = default(int?), int? skip = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyType> GetPolicyTypeAsync(string project, System.Guid typeId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyType> GetPolicyTypeAsync(System.Guid project, System.Guid typeId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyType>> GetPolicyTypesAsync(string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyType>> GetPolicyTypesAsync(System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.WebApi.IPagedList<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationsAsync(string project, string scope = null, int? top = default(int?), string continuationToken = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.WebApi.IPagedList<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationsAsync(System.Guid project, string scope = null, int? top = default(int?), string continuationToken = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationsAsync(string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationsAsync(System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> CreatePolicyConfigurationAsync(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration configuration, string project, int? configurationId = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> CreatePolicyConfigurationAsync(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration configuration, System.Guid project, int? configurationId = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
    [Export(typeof(IPolicyHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class IPolicyHttpClientImpl: IPolicyHttpClient
    {
        private Microsoft.TeamFoundation.Policy.WebApi.PolicyHttpClient _client;
        protected IDataManager Data { get; }
        [ImportingConstructor]
        public IPolicyHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        private Microsoft.TeamFoundation.Policy.WebApi.PolicyHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(Microsoft.TeamFoundation.Policy.WebApi.PolicyHttpClient)) as Microsoft.TeamFoundation.Policy.WebApi.PolicyHttpClient;
                }
                return _client;
            }
        }
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> CreatePolicyConfigurationAsync(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration configuration, string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.CreatePolicyConfigurationAsync(configuration, project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> CreatePolicyConfigurationAsync(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration configuration, System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.CreatePolicyConfigurationAsync(configuration, project, userState, cancellationToken);
		public System.Threading.Tasks.Task DeletePolicyConfigurationAsync(string project, int configurationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.DeletePolicyConfigurationAsync(project, configurationId, userState, cancellationToken);
		public System.Threading.Tasks.Task DeletePolicyConfigurationAsync(System.Guid project, int configurationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.DeletePolicyConfigurationAsync(project, configurationId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> GetPolicyConfigurationAsync(string project, int configurationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyConfigurationAsync(project, configurationId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> GetPolicyConfigurationAsync(System.Guid project, int configurationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyConfigurationAsync(project, configurationId, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationsAsync(string project, string scope = null, System.Guid? policyType = default(System.Guid?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyConfigurationsAsync(project, scope, policyType, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationsAsync(System.Guid project, string scope = null, System.Guid? policyType = default(System.Guid?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyConfigurationsAsync(project, scope, policyType, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> UpdatePolicyConfigurationAsync(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration configuration, string project, int configurationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.UpdatePolicyConfigurationAsync(configuration, project, configurationId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> UpdatePolicyConfigurationAsync(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration configuration, System.Guid project, int configurationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.UpdatePolicyConfigurationAsync(configuration, project, configurationId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyEvaluationRecord> GetPolicyEvaluationAsync(string project, System.Guid evaluationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyEvaluationAsync(project, evaluationId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyEvaluationRecord> GetPolicyEvaluationAsync(System.Guid project, System.Guid evaluationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyEvaluationAsync(project, evaluationId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyEvaluationRecord> RequeuePolicyEvaluationAsync(string project, System.Guid evaluationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.RequeuePolicyEvaluationAsync(project, evaluationId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyEvaluationRecord> RequeuePolicyEvaluationAsync(System.Guid project, System.Guid evaluationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.RequeuePolicyEvaluationAsync(project, evaluationId, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyEvaluationRecord>> GetPolicyEvaluationsAsync(string project, string artifactId, bool? includeNotApplicable = default(bool?), int? top = default(int?), int? skip = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyEvaluationsAsync(project, artifactId, includeNotApplicable, top, skip, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyEvaluationRecord>> GetPolicyEvaluationsAsync(System.Guid project, string artifactId, bool? includeNotApplicable = default(bool?), int? top = default(int?), int? skip = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyEvaluationsAsync(project, artifactId, includeNotApplicable, top, skip, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> GetPolicyConfigurationRevisionAsync(string project, int configurationId, int revisionId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyConfigurationRevisionAsync(project, configurationId, revisionId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> GetPolicyConfigurationRevisionAsync(System.Guid project, int configurationId, int revisionId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyConfigurationRevisionAsync(project, configurationId, revisionId, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationRevisionsAsync(string project, int configurationId, int? top = default(int?), int? skip = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyConfigurationRevisionsAsync(project, configurationId, top, skip, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationRevisionsAsync(System.Guid project, int configurationId, int? top = default(int?), int? skip = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyConfigurationRevisionsAsync(project, configurationId, top, skip, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyType> GetPolicyTypeAsync(string project, System.Guid typeId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyTypeAsync(project, typeId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyType> GetPolicyTypeAsync(System.Guid project, System.Guid typeId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyTypeAsync(project, typeId, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyType>> GetPolicyTypesAsync(string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyTypesAsync(project, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyType>> GetPolicyTypesAsync(System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyTypesAsync(project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.WebApi.IPagedList<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationsAsync(string project, string scope = null, int? top = default(int?), string continuationToken = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyConfigurationsAsync(project, scope, top, continuationToken, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.WebApi.IPagedList<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationsAsync(System.Guid project, string scope = null, int? top = default(int?), string continuationToken = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyConfigurationsAsync(project, scope, top, continuationToken, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationsAsync(string project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyConfigurationsAsync(project, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration>> GetPolicyConfigurationsAsync(System.Guid project, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPolicyConfigurationsAsync(project, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> CreatePolicyConfigurationAsync(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration configuration, string project, int? configurationId = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.CreatePolicyConfigurationAsync(configuration, project, configurationId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration> CreatePolicyConfigurationAsync(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration configuration, System.Guid project, int? configurationId = default(int?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.CreatePolicyConfigurationAsync(configuration, project, configurationId, userState, cancellationToken);
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