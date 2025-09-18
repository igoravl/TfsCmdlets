using Microsoft.VisualStudio.Services.Search.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(SearchHttpClient))]
    public partial interface ISearchHttpClient
    {
    }
}