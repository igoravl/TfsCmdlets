using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace TfsCmdlets.HttpClients
{

    [HttpClient(typeof(WorkItemTrackingHttpClient))]
    partial interface IWorkItemTrackingHttpClient {
        
    }
}