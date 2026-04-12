using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(ReleaseHttpClient2))]
    public partial interface IReleaseHttpClient2
    {
    }
}