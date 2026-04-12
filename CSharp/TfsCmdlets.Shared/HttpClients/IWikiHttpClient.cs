using Microsoft.TeamFoundation.Wiki.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(WikiHttpClient))]
    public partial interface IWikiHttpClient
    {
    }
}