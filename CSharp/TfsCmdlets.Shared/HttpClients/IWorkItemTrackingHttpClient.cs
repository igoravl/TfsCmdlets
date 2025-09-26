using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace TfsCmdlets.HttpClients
{

    [HttpClient(typeof(WorkItemTrackingHttpClient))]
    public partial interface IWorkItemTrackingHttpClient {
        
    }
}