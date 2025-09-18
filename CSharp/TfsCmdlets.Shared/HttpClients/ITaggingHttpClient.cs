using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(TaggingHttpClient))]
    public partial interface ITaggingHttpClient
    {
    }
}