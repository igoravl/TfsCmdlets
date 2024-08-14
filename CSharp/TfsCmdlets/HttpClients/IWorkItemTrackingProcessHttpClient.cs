using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(WorkItemTrackingProcessHttpClient))]
    partial interface IWorkItemTrackingProcessHttpClient
    {
    }
}