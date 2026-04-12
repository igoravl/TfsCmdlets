using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(GitHttpClient))]
    public partial interface IGitHttpClient
    {
    }
}