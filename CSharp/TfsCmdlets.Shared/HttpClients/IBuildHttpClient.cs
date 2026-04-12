using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(BuildHttpClient))]
    public partial interface IBuildHttpClient
    {
    }
}