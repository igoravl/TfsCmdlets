using System.Management.Automation;
using Microsoft.VisualStudio.Services.Search.WebApi;
using Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Gets the contents of one or more work items.
    /// </summary>
    /// <related>https://docs.microsoft.com/en-us/azure/devops/project/search/advanced-work-item-search-syntax</related>
    [TfsCmdlet(CmdletScope.Project, DefaultParameterSetName = "Query by text", OutputType = typeof(WebApiWorkItem))]
    partial class SearchWorkItem : CmdletBase
    {
        /// <summary>
        /// Specifies the text to search for. Supports the Quick Filter syntax described in 
        /// https://docs.microsoft.com/en-us/azure/devops/project/search/advanced-work-item-search-syntax
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNull()]
        public string Query { get; set; }

        /// <summary>
        /// Specifies the maximum quantity of results. Supports between 1 and 1000 results. 
        /// When omitted, defaults to 100. Currently this cmdlet does not support result pagination.
        /// </summary>
        [Parameter]
        [ValidateRange(1, 1000)]
        public int Results { get; set; } = 100;
    }

    [CmdletController(typeof(WebApiWorkItem), Client=typeof(ISearchHttpClient))]
    partial class SearchWorkItemController
    {
        protected override IEnumerable Run()
        {
            var text = Parameters.Get<string>(nameof(SearchWorkItem.Query));
            var results = Parameters.Get<int>(nameof(SearchWorkItem.Results));

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

                resp = Client.FetchWorkItemSearchResultsAsync(req, tp.Name)
                    .GetResult("Error getting search results");
            }
            else
            {
                resp = Client.FetchWorkItemSearchResultsAsync(req)
                    .GetResult("Error getting search results");
            }

            return Data.GetItems<WebApiWorkItem>(new { WorkItem = resp.Results.Select(wi => int.Parse(wi.Fields["system.id"])).ToList() });
        }
    }
}