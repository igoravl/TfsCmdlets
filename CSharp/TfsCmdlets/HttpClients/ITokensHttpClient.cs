using Microsoft.VisualStudio.Services.DelegatedAuthorization.Client;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(TokensHttpClient))]
    partial interface ITokensHttpClient
    {
    }
}
