using Microsoft.TeamFoundation.Wiki.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(WikiHttpClient))]
    partial interface IWikiHttpClient
    {
    }
}