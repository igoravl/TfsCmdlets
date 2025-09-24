//HintName: TfsCmdlets.HttpClients.IAccountLicensingHttpClient.g.cs
using System.Composition;
using Microsoft.VisualStudio.Services.Licensing.Client;

namespace TfsCmdlets.HttpClients
{
    public partial interface IAccountLicensingHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
		public System.Threading.Tasks.Task<System.Collections.Generic.IDictionary<string, bool>> ComputeExtensionRightsAsync(System.Collections.Generic.IEnumerable<string> extensionIds, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.ExtensionRightsResult> GetExtensionRightsAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Licensing.AccountLicenseUsage>> GetAccountLicensesUsageAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement>> GetAccountEntitlementsAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement>> GetAccountEntitlementsAsync(int top, int skip = 0, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.PagedAccountEntitlements> SearchAccountEntitlementsAsync(string continuation = null, string filter = null, string orderBy = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.PagedAccountEntitlements> SearchMemberAccountEntitlementsAsync(string continuation = null, string filter = null, string orderBy = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement>> GetAccountEntitlementsAsync(System.Collections.Generic.IList<System.Guid> userIds, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.IList<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement>> ObtainAvailableAccountEntitlementsAsync(System.Collections.Generic.IList<System.Guid> userIds, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> GetAccountEntitlementAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> GetAccountEntitlementAsync(System.Guid userId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> GetAccountEntitlementAsync(System.Guid userId, bool determineRights, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> GetAccountEntitlementAsync(System.Guid userId, bool determineRights, bool createIfNotExists, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> AssignEntitlementAsync(System.Guid userId, Microsoft.VisualStudio.Services.Licensing.License license, bool dontNotifyUser = false, Microsoft.VisualStudio.Services.Licensing.LicensingOrigin origin = 0, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> AssignAvailableEntitlementAsync(System.Guid userId, bool dontNotifyUser = false, Microsoft.VisualStudio.Services.Licensing.LicensingOrigin origin = 0, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task DeleteEntitlementAsync(System.Guid userId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task TransferIdentityRightsAsync(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Guid, System.Guid>> userIdTransferMap, bool? validateOnly = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public void SetResourceLocations(Microsoft.VisualStudio.Services.WebApi.ApiResourceLocationCollection resourceLocations);
		public bool IsDisposed();
		public void Dispose();

    }
    
    [Export(typeof(IAccountLicensingHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class IAccountLicensingHttpClientImpl: IAccountLicensingHttpClient
    {
        private Microsoft.VisualStudio.Services.Licensing.Client.AccountLicensingHttpClient _client;
        
        protected IDataManager Data { get; }
        
        [ImportingConstructor]
        public IAccountLicensingHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        
        private Microsoft.VisualStudio.Services.Licensing.Client.AccountLicensingHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(Microsoft.VisualStudio.Services.Licensing.Client.AccountLicensingHttpClient)) as Microsoft.VisualStudio.Services.Licensing.Client.AccountLicensingHttpClient;
                }
                return _client;
            }
        }
        
		public System.Threading.Tasks.Task<System.Collections.Generic.IDictionary<string, bool>> ComputeExtensionRightsAsync(System.Collections.Generic.IEnumerable<string> extensionIds, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.ComputeExtensionRightsAsync(extensionIds, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.ExtensionRightsResult> GetExtensionRightsAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetExtensionRightsAsync(userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Licensing.AccountLicenseUsage>> GetAccountLicensesUsageAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetAccountLicensesUsageAsync(userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement>> GetAccountEntitlementsAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetAccountEntitlementsAsync(userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement>> GetAccountEntitlementsAsync(int top, int skip = 0, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetAccountEntitlementsAsync(top, skip, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.PagedAccountEntitlements> SearchAccountEntitlementsAsync(string continuation = null, string filter = null, string orderBy = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.SearchAccountEntitlementsAsync(continuation, filter, orderBy, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.PagedAccountEntitlements> SearchMemberAccountEntitlementsAsync(string continuation = null, string filter = null, string orderBy = null, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.SearchMemberAccountEntitlementsAsync(continuation, filter, orderBy, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement>> GetAccountEntitlementsAsync(System.Collections.Generic.IList<System.Guid> userIds, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetAccountEntitlementsAsync(userIds, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.IList<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement>> ObtainAvailableAccountEntitlementsAsync(System.Collections.Generic.IList<System.Guid> userIds, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.ObtainAvailableAccountEntitlementsAsync(userIds, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> GetAccountEntitlementAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetAccountEntitlementAsync(userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> GetAccountEntitlementAsync(System.Guid userId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetAccountEntitlementAsync(userId, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> GetAccountEntitlementAsync(System.Guid userId, bool determineRights, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetAccountEntitlementAsync(userId, determineRights, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> GetAccountEntitlementAsync(System.Guid userId, bool determineRights, bool createIfNotExists, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetAccountEntitlementAsync(userId, determineRights, createIfNotExists, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> AssignEntitlementAsync(System.Guid userId, Microsoft.VisualStudio.Services.Licensing.License license, bool dontNotifyUser = false, Microsoft.VisualStudio.Services.Licensing.LicensingOrigin origin = 0, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.AssignEntitlementAsync(userId, license, dontNotifyUser, origin, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Licensing.AccountEntitlement> AssignAvailableEntitlementAsync(System.Guid userId, bool dontNotifyUser = false, Microsoft.VisualStudio.Services.Licensing.LicensingOrigin origin = 0, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.AssignAvailableEntitlementAsync(userId, dontNotifyUser, origin, userState, cancellationToken);
		public System.Threading.Tasks.Task DeleteEntitlementAsync(System.Guid userId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.DeleteEntitlementAsync(userId, userState, cancellationToken);
		public System.Threading.Tasks.Task TransferIdentityRightsAsync(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Guid, System.Guid>> userIdTransferMap, bool? validateOnly = default(bool?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.TransferIdentityRightsAsync(userIdTransferMap, validateOnly, userState, cancellationToken);
		public void SetResourceLocations(Microsoft.VisualStudio.Services.WebApi.ApiResourceLocationCollection resourceLocations)
			=> Client.SetResourceLocations(resourceLocations);
		public bool IsDisposed()
			=> Client.IsDisposed();
		public void Dispose()
			=> Client.Dispose();

    }
}