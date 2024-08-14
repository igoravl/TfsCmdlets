using Microsoft.VisualStudio.Services.ServiceHooks.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(ServiceHooksPublisherHttpClient))]
    partial interface IServiceHooksPublisherHttpClient
    {
    }
}