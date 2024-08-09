using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(ProjectHttpClient))]
    partial interface IProjectHttpClient
    {
    }
}