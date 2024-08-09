using Microsoft.VisualStudio.Services.Graph.Client;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IDescriptorService)), Shared]
    internal class DescriptorServiceImpl: IDescriptorService
    {
        [Import]
        private IGraphHttpClient Client { get; set; }

        public GraphDescriptorResult GetDescriptor(Guid storageKey)
        {
            return Client.GetDescriptorAsync(storageKey)
                .GetResult("Error getting descriptor");
        }
    }
}