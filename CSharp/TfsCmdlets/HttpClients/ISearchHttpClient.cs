using Microsoft.VisualStudio.Services.Search.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(SearchHttpClient))]
    partial interface ISearchHttpClient
    {
    }
}