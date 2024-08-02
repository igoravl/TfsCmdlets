using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(GitHttpClient))]
    partial interface IGitHttpClient
    {
    }
}