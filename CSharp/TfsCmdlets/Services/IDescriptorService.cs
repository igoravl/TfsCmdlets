using Microsoft.VisualStudio.Services.Graph.Client;

namespace TfsCmdlets.Services
{
    public interface IDescriptorService
    {
        GraphDescriptorResult GetDescriptor(Guid storageKey);
    }
}