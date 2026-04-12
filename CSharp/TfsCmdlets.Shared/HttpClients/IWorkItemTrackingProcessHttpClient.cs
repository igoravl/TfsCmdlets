using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(WorkItemTrackingProcessHttpClient))]
    public partial interface IWorkItemTrackingProcessHttpClient
    {
    }
}