using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(ServiceHooksPublisherHttpClient))]
    public partial interface IServiceHooksPublisherHttpClient
    {
    }
}