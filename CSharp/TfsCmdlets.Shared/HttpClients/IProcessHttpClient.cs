using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(ProcessHttpClient))]
    public partial interface IProcessHttpClient {

    }
}