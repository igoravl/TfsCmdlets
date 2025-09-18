using Microsoft.TeamFoundation.Work.WebApi;

namespace TfsCmdlets.HttpClients
{

    [HttpClient(typeof(WorkHttpClient))]
    public partial interface IWorkHttpClient
    {
    }
}