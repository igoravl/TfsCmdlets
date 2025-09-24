//HintName: TfsCmdlets.HttpClients.IProcessHttpClient.g.cs
#pragma warning disable CS8669
using System.Composition;
using Microsoft.TeamFoundation.Core.WebApi;
namespace TfsCmdlets.HttpClients
{
    public partial interface IProcessHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.Process> GetProcessByIdAsync(System.Guid processId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.Process>> GetProcessesAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
    [Export(typeof(IProcessHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class IProcessHttpClientImpl: IProcessHttpClient
    {
        private Microsoft.TeamFoundation.Core.WebApi.ProcessHttpClient _client;
        protected IDataManager Data { get; }
        [ImportingConstructor]
        public IProcessHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        private Microsoft.TeamFoundation.Core.WebApi.ProcessHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(Microsoft.TeamFoundation.Core.WebApi.ProcessHttpClient)) as Microsoft.TeamFoundation.Core.WebApi.ProcessHttpClient;
                }
                return _client;
            }
        }
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.Process> GetProcessByIdAsync(System.Guid processId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetProcessByIdAsync(processId, userState, cancellationToken);
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.Process>> GetProcessesAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
			=> Client.GetProcessesAsync(userState, cancellationToken);
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