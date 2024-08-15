using Microsoft.TeamFoundation.Work.WebApi;

namespace TfsCmdlets.HttpClients
{

    [HttpClient(typeof(WorkHttpClient))]
    partial interface IWorkHttpClient
    {
    }
}