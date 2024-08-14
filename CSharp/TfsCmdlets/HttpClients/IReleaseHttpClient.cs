using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(ReleaseHttpClient))]
    partial interface IReleaseHttpClient
    {
    }
}