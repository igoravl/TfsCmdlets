using Microsoft.VisualStudio.Services.Identity.Client;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(IdentityHttpClient))]
    partial interface IIdentityHttpClient
    {
    }
}