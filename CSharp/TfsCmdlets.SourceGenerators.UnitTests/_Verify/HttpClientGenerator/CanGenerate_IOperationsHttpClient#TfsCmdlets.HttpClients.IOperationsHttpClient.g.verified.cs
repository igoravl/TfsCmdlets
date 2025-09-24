//HintName: TfsCmdlets.HttpClients.IOperationsHttpClient.g.cs
#pragma warning disable CS8669
using System.Composition;
using Microsoft.VisualStudio.Services.Operations;
namespace TfsCmdlets.HttpClients
{
    public partial interface IOperationsHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.Operation> GetOperation(System.Guid id, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.Operation> GetOperationAsync(Microsoft.VisualStudio.Services.Operations.OperationReference operationReference, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.Operation> GetOperationAsync(System.Guid operationId, System.Guid? pluginId = default(System.Guid?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
    [Export(typeof(IOperationsHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class IOperationsHttpClientImpl: IOperationsHttpClient
    {
        private Microsoft.VisualStudio.Services.Operations.OperationsHttpClient _client;
        protected IDataManager Data { get; }
        [ImportingConstructor]
        public IOperationsHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        private Microsoft.VisualStudio.Services.Operations.OperationsHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(Microsoft.VisualStudio.Services.Operations.OperationsHttpClient)) as Microsoft.VisualStudio.Services.Operations.OperationsHttpClient;
                }
                return _client;
            }
        }
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.Operation> GetOperation(System.Guid id, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetOperation(id, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.Operation> GetOperationAsync(Microsoft.VisualStudio.Services.Operations.OperationReference operationReference, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetOperationAsync(operationReference, userState, cancellationToken);
		public System.Threading.Tasks.Task<Microsoft.VisualStudio.Services.Operations.Operation> GetOperationAsync(System.Guid operationId, System.Guid? pluginId = default(System.Guid?), object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetOperationAsync(operationId, pluginId, userState, cancellationToken);
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