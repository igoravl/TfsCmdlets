using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Search.WebApi;
using Microsoft.VisualStudio.Services.Search.WebApi.Contracts.WorkItem;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Cmdlets.WorkItem;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;

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

            throw new NotImplementedException();

            // return Data.GetItems<WebApiWorkItem>(resp.Results.SelectMany(r => r.Hits).Select(h => h.));
        }
    }
}