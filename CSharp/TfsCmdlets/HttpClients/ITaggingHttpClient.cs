using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(TaggingHttpClient))]
    partial interface ITaggingHttpClient
    {
    }
}