using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Gets the contents of one or more work items.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsWorkItem", DefaultParameterSetName = "Query by revision")]
    [OutputType(typeof(WebApiWorkItem))]
    public class GetWorkItem : GetCmdletBase<WebApiWorkItem>
    {
        /// <summary>
        /// HELP_PARAM_WORKITEM
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Query by revision")]
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Query by date")]
        [Parameter(Position = 0, ParameterSetName = "Get deleted")]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Specifies a work item revision number to retrieve. When omitted, returns
        /// the latest revision of the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Query by revision")]
        [Alias("rev")]
        public int Revision { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Query by date")]
        public DateTime AsOf { get; set; }

        /// <summary>
        /// Specifies a query written in WIQL (Work Item Query Language)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Query by WIQL")]
        [Alias("WIQL", "QueryText", "SavedQuery", "QueryPath")]
        public string Query { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Query by filter")]
        public string Filter { get; set; }

        [Parameter()]
        public Hashtable Macros { get; set; }

        /// <summary>
        /// Opens the specified work item in the default web browser.
        /// </summary>
        [Parameter(ParameterSetName = "Query by revision")]
        public SwitchParameter ShowWindow { get; set; }

        /// <summary>
        /// Gets deleted work items.
        /// </summary>
        [Parameter(ParameterSetName = "Get deleted")]
        public SwitchParameter Deleted { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }

    }

    [Exports(typeof(WebApiWorkItem))]
    internal partial class WorkItemDataService : BaseDataService<WebApiWorkItem>
    {
        protected override IEnumerable<WebApiWorkItem> DoGetItems()
        {
            var workItem = GetParameter<object>(nameof(GetWorkItem.WorkItem));
            var revision = GetParameter<int>(nameof(GetWorkItem.Revision));
            var asOf = GetParameter<DateTime?>(nameof(GetWorkItem.AsOf));
            var query = GetParameter<string>(nameof(GetWorkItem.Query));
            var showWindow = GetParameter<bool>(nameof(GetWorkItem.ShowWindow));
            var deleted = GetParameter<bool>(nameof(GetWorkItem.Deleted));

            var (_, tp) = GetCollectionAndProject();
            var client = GetClient<WorkItemTrackingHttpClient>();

            var done = false;

            while (!done) switch (workItem)
                {
                    case WebApiWorkItem wi when showWindow:
                        {
                            workItem = new[] { (int)wi.Id };
                            continue;
                        }
                    case WorkItemReference wiRef:
                        {
                            workItem = wiRef.Id;
                            continue;
                        }
                    case int id:
                        {
                            workItem = new[] { id };
                            continue;
                        }
                    case object[] wis:
                        {
                            var list = new List<int>();
                            foreach (var o in wis)
                            {
                                switch (o)
                                {
                                    case int i: list.Add(i); break;
                                    case WebApiWorkItem wi: list.Add((int)wi.Id); break;
                                }
                            }
                            workItem = list.ToArray();
                            continue;
                        }
                    case int[] ids:
                        {
                            foreach (int id in ids) yield return FetchWorkItem(id, revision, asOf, client);

                            yield break;
                        }
                    case null when deleted:
                    case object o when deleted:
                        {
                            IEnumerable<WorkItemDeleteReference> result;

                            if (workItem is int[] ids)
                            {
                                result = client.GetDeletedWorkItemsAsync(ids)
                                    .GetResult($"Error getting deleted work items {string.Join(", ", ids)}");
                            }
                            else
                            {
                                var refs = client.GetDeletedWorkItemShallowReferencesAsync(tp.Name)
                                    .GetResult($"Error getting references for deleted work items")
                                    .Select(r => (int)r.Id)
                                    .ToList();

                                if (refs.Count == 0) yield break;

                                result = client.GetDeletedWorkItemsAsync(refs)
                                    .GetResult($"Error getting deleted work items");
                            }

                            foreach (var wi in result)
                            {
                                yield return new WebApiWorkItem()
                                {
                                    Id = wi.Id,
                                    Fields = new Dictionary<string, object>()
                                    {
                                        ["System.WorkItemType"] = wi.Type,
                                        ["System.Title"] = wi.Name,
                                        ["System.AreaPath"] = wi.Project,
                                        ["System.IterationPath"] = wi.Project,
                                        ["System.ChangedDate"] = wi.DeletedDate,
                                        ["DeletedBy"] = wi.DeletedBy,
                                        ["DeletedDate"] = wi.DeletedDate,
                                        ["WorkItemDeleteReference"] = wi
                                    },
                                    Url = wi.Url
                                };
                            }
                            yield break;
                        }
                    case null when !string.IsNullOrEmpty(query) && query.StartsWith("SELECT ", StringComparison.OrdinalIgnoreCase):
                        {
                            var result = client.QueryByWiqlAsync(new Wiql() { Query = query }, tp.Name)
                                .GetResult($"Error running work item query '{query}'");

                            foreach (var wiRef in result.WorkItems)
                            {
                                yield return FetchWorkItem(wiRef.Id, 0, DateTime.MinValue, client);
                            }

                            yield break;
                        }
                    case null when !string.IsNullOrEmpty(query):
                        {
                            var savedQuery = client.GetQueryAsync(tp.Name, query, QueryExpand.Wiql)
                                .GetResult($"Error running work item query '{query}'");
                            query = savedQuery.Wiql;
                            continue;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent work item '{workItem}'");
                        }
                }
        }

        private WebApiWorkItem FetchWorkItem(int id, int revision, DateTime? asOf, WorkItemTrackingHttpClient client)
        {
            try{
            if (revision > 0)
                return client.GetRevisionAsync(id, revision)
                    .GetResult($"Error getting work item '{id}'");
            else if (asOf.HasValue && asOf.Value > DateTime.MinValue)
                return client.GetWorkItemAsync(id, null, asOf)
                    .GetResult($"Error getting work item '{id}'");
            else
                return client.GetWorkItemAsync(id)
                    .GetResult($"Error getting work item '{id}'");
            }
            catch(Exception ex)
            {
                Cmdlet.WriteError(new ErrorRecord(ex, ex.InnerException.GetType().Name, ErrorCategory.ReadError, id));
                return null;
            }
        }
    }
}