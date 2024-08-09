using Microsoft.TeamFoundation.Policy.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(PolicyHttpClient))]
    partial interface IPolicyHttpClient
    {
    }
}