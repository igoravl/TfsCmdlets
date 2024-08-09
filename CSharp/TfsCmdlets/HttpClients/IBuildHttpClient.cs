using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(BuildHttpClient))]
    partial interface IBuildHttpClient
    {
    }
}