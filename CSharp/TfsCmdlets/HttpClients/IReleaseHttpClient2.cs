using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(ReleaseHttpClient2))]
    partial interface IReleaseHttpClient2
    {
    }
}