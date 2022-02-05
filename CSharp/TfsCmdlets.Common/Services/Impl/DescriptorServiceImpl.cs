using Microsoft.VisualStudio.Services.Graph.Client;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IDescriptorService)), Shared]
    internal class DescriptorServiceImpl: IDescriptorService
    {
        private IDataManager Data {get;set;}

        public GraphDescriptorResult GetDescriptor(Guid storageKey)
        {
            var client = Data.GetClient<GraphHttpClient>();

            return client.GetDescriptorAsync(storageKey)
                .GetResult("Error getting descriptor");
        }

        [ImportingConstructor]
        public DescriptorServiceImpl(IDataManager dataManager)
        {
            Data = dataManager;
        }	
    }
}