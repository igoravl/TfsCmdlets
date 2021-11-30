// using System.Collections.Generic;
// using System.Management.Automation;
// using Microsoft.VisualStudio.Services.Search.WebApi;
// using Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem;
// using TfsCmdlets.Extensions;
// using TfsCmdlets.Services;
// using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;

// namespace TfsCmdlets.Cmdlets.WorkItem
// {
//     /// <summary>
//     /// Gets the contents of one or more work items.
//     /// </summary>
//     /// <related>https://docs.microsoft.com/en-us/azure/devops/project/search/advanced-work-item-search-syntax</related>
//     [Cmdlet(VerbsCommon.Search, "TfsWorkItem", DefaultParameterSetName="Query by text")]
//     [OutputType(typeof(WebApiWorkItem))]
//     public class SearchWorkItem: CmdletBase
//     {
//         /// <summary>
//         /// Specifies the text to search for. Supports the Quick Filter syntax described in 
//         /// https://docs.microsoft.com/en-us/azure/devops/project/search/advanced-work-item-search-syntax
//         /// </summary>
//         [Parameter(Position=0, Mandatory=true, ValueFromPipeline=true)]
//         [ValidateNotNull()]
//         public string Query { get; set; }

//         /// <summary>
//         /// Specifies the maximum quantity of results. Supports between 1 and 1000 results. 
//         /// When omitted, defaults to 100. Currently this cmdlet does not support result pagination.
//         /// </summary>
//         [Parameter()]
//         [ValidateRange(1, 1000)]
//         public int Results { get; set; } = 100;

//         /// <summary>
//         /// HELP_PARAM_PROJECT
//         /// </summary>
//         [Parameter()]
//         public object Project { get; set; }
//     }

//     // TODO

//     //[Exports(typeof(WorkItemResult))]
//     //internal partial class WorkItemResultDataService : CollectionLevelController<WorkItemResult>
//     //{
//     //    protected override IEnumerable<WorkItemResult> DoGetItems()
//     //    {
//     //        var text = parameters.Get<string>(nameof(SearchWorkItem.Query));
//     //        var results = parameters.Get<int>(nameof(SearchWorkItem.Results));
//     //        var client = Data.GetClient<SearchHttpClient>(parameters);
//     //        var req = new WorkItemSearchRequest() {
//     //            SearchText = text,
//     //            IncludeFacets = false,
//     //            Top = results
//     //        };

//     //        WorkItemSearchResponse resp;

//     //        if(HasParameter(nameof(SearchWorkItem.Project)))
//     //        {
//     //            var tp = Data.GetProject();

//     //            resp = client.FetchWorkItemSearchResultsAsync(req, tp.Name)
//     //                .GetResult("Error getting search results");
//     //        }
//     //        else
//     //        {
//     //            resp = client.FetchWorkItemSearchResultsAsync(req)
//     //                .GetResult("Error getting search results");
//     //        }

//     //        foreach(var r in resp.Results)
//     //        {
//     //            yield return r;
//     //        }

//     //        yield break;
//     //    }
//     //}
// }