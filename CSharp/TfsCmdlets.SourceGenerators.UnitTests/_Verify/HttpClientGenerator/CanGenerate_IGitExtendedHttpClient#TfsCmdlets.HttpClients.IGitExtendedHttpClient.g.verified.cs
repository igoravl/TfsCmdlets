//HintName: TfsCmdlets.HttpClients.IGitExtendedHttpClient.g.cs
#pragma warning disable CS8669
using System.Composition;
using Microsoft.VisualStudio.Services.WebApi;
namespace TfsCmdlets.HttpClients
{
    public partial interface IGitExtendedHttpClient: Microsoft.VisualStudio.Services.WebApi.IVssHttpClient
    {
    }
    [Export(typeof(IGitExtendedHttpClient))]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal class IGitExtendedHttpClientImpl: IGitExtendedHttpClient
    {
        private TfsCmdlets.HttpClients.GitExtendedHttpClient _client;
        protected IDataManager Data { get; }
        [ImportingConstructor]
        public IGitExtendedHttpClientImpl(IDataManager data)
        {
            Data = data;
        }
        private TfsCmdlets.HttpClients.GitExtendedHttpClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = (Data.GetCollection() as TfsCmdlets.Services.ITfsServiceProvider)?.GetClient(typeof(TfsCmdlets.HttpClients.GitExtendedHttpClient)) as TfsCmdlets.HttpClients.GitExtendedHttpClient;
                }
                return _client;
            }
        }
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