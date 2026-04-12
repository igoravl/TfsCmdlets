using Microsoft.TeamFoundation.Policy.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(PolicyHttpClient))]
    public partial interface IPolicyHttpClient
    {
    }
}