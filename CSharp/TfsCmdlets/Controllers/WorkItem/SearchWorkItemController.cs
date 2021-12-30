using Microsoft.VisualStudio.Services.Search.WebApi;
using Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem;
using TfsCmdlets.Cmdlets.WorkItem;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class SearchWorkItemController
    {
        public override IEnumerable<WebApiWorkItem> Invoke()
        {
            var text = Parameters.Get<string>(nameof(SearchWorkItem.Query));
            var results = Parameters.Get<int>(nameof(SearchWorkItem.Results));
            var client = Data.GetClient<SearchHttpClient>();

            var req = new WorkItemSearchRequest()
            {
                SearchText = text,
                IncludeFacets = false,
                Top = results
            };

            WorkItemSearchResponse resp;

            if (Parameters.HasParameter(nameof(SearchWorkItem.Project)))
            {
                var tp = Data.GetProject();

                resp = client.FetchWorkItemSearchResultsAsync(req, tp.Name)
                    .GetResult("Error getting search results");
            }
            else
            {
                resp = client.FetchWorkItemSearchResultsAsync(req)
                    .GetResult("Error getting search results");
            }

            return Data.GetItems<WebApiWorkItem>(new { WorkItem = resp.Results.Select(wi => int.Parse(wi.Fields["system.id"])).ToList() });
        }
    }
}