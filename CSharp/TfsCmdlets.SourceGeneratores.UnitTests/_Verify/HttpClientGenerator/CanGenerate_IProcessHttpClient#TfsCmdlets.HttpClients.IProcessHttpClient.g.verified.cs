//HintName: TfsCmdlets.HttpClients.IProcessHttpClient.g.cs
using System.Composition;
namespace TfsCmdlets.HttpClients
{
    public partial interface IProcessHttpClient: IVssHttpClient
    {
		public System.Threading.Tasks.Task<Microsoft.TeamFoundation.Core.WebApi.Process> GetProcessByIdAsync(System.Guid processId, object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public System.Threading.Tasks.Task<System.Collections.Generic.List<Microsoft.TeamFoundation.Core.WebApi.Process>> GetProcessesAsync(object userState = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
		public void SetResourceLocations(Microsoft.VisualStudio.Services.WebApi.ApiResourceLocationCollection resourceLocations);
		public void Dispose();
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
		public void SetResourceLocations(Microsoft.VisualStudio.Services.WebApi.ApiResourceLocationCollection resourceLocations)
			=> Client.SetResourceLocations(resourceLocations);
		public void Dispose()
			=> Client.Dispose();
    }
}