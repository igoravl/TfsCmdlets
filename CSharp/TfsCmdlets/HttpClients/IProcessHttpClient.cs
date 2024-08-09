using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(ProcessHttpClient))]
    partial interface IProcessHttpClient {

    }
}