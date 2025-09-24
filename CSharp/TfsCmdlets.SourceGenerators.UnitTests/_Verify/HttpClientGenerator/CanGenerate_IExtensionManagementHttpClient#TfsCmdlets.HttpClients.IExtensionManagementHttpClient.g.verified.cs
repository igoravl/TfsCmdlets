//HintName: TfsCmdlets.HttpClients.IExtensionManagementHttpClient.g.cs
#pragma warning disable CS8669
using System.Composition;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;
namespace TfsCmdlets.HttpClients
{
    public partial interface IExtensionManagementHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.AcquisitionOptions> GetAcquisitionOptionsAsync(string itemId, bool? testCommerce = default(bool?), bool? isFreeOrTrialInstall = default(bool?), bool? isAccountOwner = default(bool?), bool? isLinked = default(bool?), bool? isConnectedServer = default(bool?), bool? isBuyOperationValid = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.AcquisitionRequest.ExtensionAcquisitionRequest> RequestAcquisitionAsync(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.AcquisitionRequest.ExtensionAcquisitionRequest acquisitionRequest, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionAuditLog> GetAuditLogAsync(string publisherName, string extensionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionAuthorization> RegisterAuthorizationAsync(string publisherName, string extensionName, System.Guid registrationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Newtonsoft.Json.Linq.JObject> CreateDocumentByNameAsync(Newtonsoft.Json.Linq.JObject doc, string publisherName, string extensionName, string scopeType, string scopeValue, string collectionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task DeleteDocumentByNameAsync(string publisherName, string extensionName, string scopeType, string scopeValue, string collectionName, string documentId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Newtonsoft.Json.Linq.JObject> GetDocumentByNameAsync(string publisherName, string extensionName, string scopeType, string scopeValue, string collectionName, string documentId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Newtonsoft.Json.Linq.JObject>> GetDocumentsByNameAsync(string publisherName, string extensionName, string scopeType, string scopeValue, string collectionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Newtonsoft.Json.Linq.JObject> SetDocumentByNameAsync(Newtonsoft.Json.Linq.JObject doc, string publisherName, string extensionName, string scopeType, string scopeValue, string collectionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Newtonsoft.Json.Linq.JObject> UpdateDocumentByNameAsync(Newtonsoft.Json.Linq.JObject doc, string publisherName, string extensionName, string scopeType, string scopeValue, string collectionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionDataCollection>> QueryCollectionsByNameAsync(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionDataCollectionQuery collectionQuery, string publisherName, string extensionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionState>> GetStatesAsync(bool? includeDisabled = default(bool?), bool? includeErrors = default(bool?), bool? includeInstallationIssues = default(bool?), bool? forceRefresh = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension>> QueryExtensionsAsync(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtensionQuery query, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension>> GetInstalledExtensionsAsync(bool? includeDisabledExtensions = default(bool?), bool? includeErrors = default(bool?), System.Collections.Generic.IEnumerable<string> assetTypes = null, bool? includeInstallationIssues = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension> UpdateInstalledExtensionAsync(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension extension, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension> GetInstalledExtensionByNameAsync(string publisherName, string extensionName, System.Collections.Generic.IEnumerable<string> assetTypes = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension> InstallExtensionByNameAsync(string publisherName, string extensionName, string version = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task UninstallExtensionByNameAsync(string publisherName, string extensionName, string reason = null, string reasonCode = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Gallery.WebApi.UserExtensionPolicy> GetPoliciesAsync(string userId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<int> ResolveRequestAsync(string rejectMessage, string publisherName, string extensionName, string requesterId, Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionRequestState state, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.RequestedExtension>> GetRequestsAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<int> ResolveAllRequestsAsync(string rejectMessage, string publisherName, string extensionName, Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionRequestState state, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task DeleteRequestAsync(string publisherName, string extensionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.RequestedExtension> RequestExtensionAsync(string publisherName, string extensionName, string requestMessage, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<string> GetTokenAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
    [Export(typeof(IExtensionManagementHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class IExtensionManagementHttpClientImpl: IExtensionManagementHttpClient
    {
        private Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionManagementHttpClient _client;
        protected IDataManager Data { get; }
        [ImportingConstructor]
        public IExtensionManagementHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        private Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionManagementHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionManagementHttpClient)) as Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionManagementHttpClient;
                }
                return _client;
            }
        }
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.AcquisitionOptions> GetAcquisitionOptionsAsync(string itemId, bool? testCommerce = default(bool?), bool? isFreeOrTrialInstall = default(bool?), bool? isAccountOwner = default(bool?), bool? isLinked = default(bool?), bool? isConnectedServer = default(bool?), bool? isBuyOperationValid = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetAcquisitionOptionsAsync(itemId, testCommerce, isFreeOrTrialInstall, isAccountOwner, isLinked, isConnectedServer, isBuyOperationValid, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.AcquisitionRequest.ExtensionAcquisitionRequest> RequestAcquisitionAsync(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.AcquisitionRequest.ExtensionAcquisitionRequest acquisitionRequest, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.RequestAcquisitionAsync(acquisitionRequest, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionAuditLog> GetAuditLogAsync(string publisherName, string extensionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetAuditLogAsync(publisherName, extensionName, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionAuthorization> RegisterAuthorizationAsync(string publisherName, string extensionName, System.Guid registrationId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.RegisterAuthorizationAsync(publisherName, extensionName, registrationId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Newtonsoft.Json.Linq.JObject> CreateDocumentByNameAsync(Newtonsoft.Json.Linq.JObject doc, string publisherName, string extensionName, string scopeType, string scopeValue, string collectionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.CreateDocumentByNameAsync(doc, publisherName, extensionName, scopeType, scopeValue, collectionName, userState, cancellationToken);
		public System.Threading.Tasks.Task DeleteDocumentByNameAsync(string publisherName, string extensionName, string scopeType, string scopeValue, string collectionName, string documentId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.DeleteDocumentByNameAsync(publisherName, extensionName, scopeType, scopeValue, collectionName, documentId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Newtonsoft.Json.Linq.JObject> GetDocumentByNameAsync(string publisherName, string extensionName, string scopeType, string scopeValue, string collectionName, string documentId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetDocumentByNameAsync(publisherName, extensionName, scopeType, scopeValue, collectionName, documentId, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Newtonsoft.Json.Linq.JObject>> GetDocumentsByNameAsync(string publisherName, string extensionName, string scopeType, string scopeValue, string collectionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetDocumentsByNameAsync(publisherName, extensionName, scopeType, scopeValue, collectionName, userState, cancellationToken);
		public System.Threading.Tasks.Task<Newtonsoft.Json.Linq.JObject> SetDocumentByNameAsync(Newtonsoft.Json.Linq.JObject doc, string publisherName, string extensionName, string scopeType, string scopeValue, string collectionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.SetDocumentByNameAsync(doc, publisherName, extensionName, scopeType, scopeValue, collectionName, userState, cancellationToken);
		public System.Threading.Tasks.Task<Newtonsoft.Json.Linq.JObject> UpdateDocumentByNameAsync(Newtonsoft.Json.Linq.JObject doc, string publisherName, string extensionName, string scopeType, string scopeValue, string collectionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.UpdateDocumentByNameAsync(doc, publisherName, extensionName, scopeType, scopeValue, collectionName, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionDataCollection>> QueryCollectionsByNameAsync(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionDataCollectionQuery collectionQuery, string publisherName, string extensionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.QueryCollectionsByNameAsync(collectionQuery, publisherName, extensionName, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionState>> GetStatesAsync(bool? includeDisabled = default(bool?), bool? includeErrors = default(bool?), bool? includeInstallationIssues = default(bool?), bool? forceRefresh = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetStatesAsync(includeDisabled, includeErrors, includeInstallationIssues, forceRefresh, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension>> QueryExtensionsAsync(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtensionQuery query, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.QueryExtensionsAsync(query, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension>> GetInstalledExtensionsAsync(bool? includeDisabledExtensions = default(bool?), bool? includeErrors = default(bool?), System.Collections.Generic.IEnumerable<string> assetTypes = null, bool? includeInstallationIssues = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetInstalledExtensionsAsync(includeDisabledExtensions, includeErrors, assetTypes, includeInstallationIssues, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension> UpdateInstalledExtensionAsync(Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension extension, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.UpdateInstalledExtensionAsync(extension, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension> GetInstalledExtensionByNameAsync(string publisherName, string extensionName, System.Collections.Generic.IEnumerable<string> assetTypes = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetInstalledExtensionByNameAsync(publisherName, extensionName, assetTypes, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.InstalledExtension> InstallExtensionByNameAsync(string publisherName, string extensionName, string version = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.InstallExtensionByNameAsync(publisherName, extensionName, version, userState, cancellationToken);
		public System.Threading.Tasks.Task UninstallExtensionByNameAsync(string publisherName, string extensionName, string reason = null, string reasonCode = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.UninstallExtensionByNameAsync(publisherName, extensionName, reason, reasonCode, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Gallery.WebApi.UserExtensionPolicy> GetPoliciesAsync(string userId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetPoliciesAsync(userId, userState, cancellationToken);
		public System.Threading.Tasks.Task<int> ResolveRequestAsync(string rejectMessage, string publisherName, string extensionName, string requesterId, Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionRequestState state, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.ResolveRequestAsync(rejectMessage, publisherName, extensionName, requesterId, state, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.RequestedExtension>> GetRequestsAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetRequestsAsync(userState, cancellationToken);
		public System.Threading.Tasks.Task<int> ResolveAllRequestsAsync(string rejectMessage, string publisherName, string extensionName, Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.ExtensionRequestState state, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.ResolveAllRequestsAsync(rejectMessage, publisherName, extensionName, state, userState, cancellationToken);
		public System.Threading.Tasks.Task DeleteRequestAsync(string publisherName, string extensionName, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.DeleteRequestAsync(publisherName, extensionName, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.ExtensionManagement.WebApi.RequestedExtension> RequestExtensionAsync(string publisherName, string extensionName, string requestMessage, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.RequestExtensionAsync(publisherName, extensionName, requestMessage, userState, cancellationToken);
		public System.Threading.Tasks.Task<string> GetTokenAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetTokenAsync(userState, cancellationToken);
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