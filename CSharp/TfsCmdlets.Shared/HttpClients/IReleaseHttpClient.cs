using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(ReleaseHttpClient))]
    public partial interface IReleaseHttpClient
    {
    }
}