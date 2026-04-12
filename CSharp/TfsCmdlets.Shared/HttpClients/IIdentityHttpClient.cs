using Microsoft.VisualStudio.Services.Identity.Client;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(IdentityHttpClient))]
    public partial interface IIdentityHttpClient
    {
    }
}