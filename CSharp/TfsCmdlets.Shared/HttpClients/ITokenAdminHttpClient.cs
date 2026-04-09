using Microsoft.VisualStudio.Services.TokenAdmin.Client;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(TokenAdminHttpClient))]
    partial interface ITokenAdminHttpClient
    {
    }
}
