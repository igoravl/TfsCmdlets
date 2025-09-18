using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(ProjectHttpClient))]
    public partial interface IProjectHttpClient
    {
    }
}