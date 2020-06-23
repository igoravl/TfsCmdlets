using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    [Cmdlet(VerbsCommon.Get, "TfsWorkItem", DefaultParameterSetName = "Query by text")]
    [OutputType(typeof(WebApiWorkItem))]
    public class GetWorkItem : GetCmdletBase<WebApiWorkItem>
    {
        /// <summary>
        /// HELP_PARAM_WORKITEM
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Query by revision")]
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Query by date")]
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

        // # [Parameter(Mandatory=true, ParameterSetName="Query by filter")]
        // # [string[]]
        // # Fields,

        [Parameter(Mandatory = true, ParameterSetName = "Query by filter")]
        public string Filter { get; set; }

        [Parameter()]
        public Hashtable Macros { get; set; }

        /// <summary>
        /// Opens the specified work item in the default web browser.
        /// </summary>
        [Parameter()]
        public SwitchParameter ShowWindow { get; set; }

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

            var (_, tp) = GetCollectionAndProject();
            var client = GetClient<WorkItemTrackingHttpClient>();

            var done = false;

            while (!done) switch (workItem)
                {
                    case WebApiWorkItem wi when showWindow:
                        {
                            workItem = (int)wi.Id;
                            continue;
                        }
                    case WorkItemReference wiRef:
                        {
                            workItem = wiRef.Id;
                            continue;
                        }
                    case int id when asOf > DateTime.MinValue:
                        {
                            yield return client.GetWorkItemAsync(tp.Name, id, null, asOf) //, new[]{"System.AssignedTo"})
                                .GetResult($"Error getting work item '{id}'");
                            yield break;
                        }
                    case int id when showWindow:
                        {
                            var wi = client.GetWorkItemAsync(tp.Name, id)
                                .GetResult($"Error getting work item '{id}'");
                            dynamic link = wi.Links.Links["html"];
                            Process.Start(link.Href);
                            yield break;
                        }
                    case int id:
                        {
                            if (revision > 0)
                                yield return client.GetRevisionAsync(tp.Name, id, revision)
                                    .GetResult($"Error getting work item '{id}'");
                            else
                                yield return client.GetWorkItemAsync(tp.Name, id)
                                    .GetResult($"Error getting work item '{id}'");

                            yield break;
                        }
                    case null when !string.IsNullOrEmpty(query) && query.StartsWith("SELECT ", StringComparison.OrdinalIgnoreCase):
                        {
                            var result = client.QueryByWiqlAsync(new Wiql() { Query = query }, tp.Name)
                                .GetResult($"Error running work item query '{query}'");

                            foreach (var wiRef in result.WorkItems)
                            {
                                yield return client.GetWorkItemAsync(tp.Name, wiRef.Id) //, new[]{"System.AssignedTo"})
                                    .GetResult($"Error getting work item '{wiRef.Id}'");
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

            // switch(ParameterSetName)
            // {
            //     "Query by revision" {
            //         WriteObject(_GetWorkItemByRevision WorkItem Revision store); return;
            //     }

            //     "Query by date" {
            //         WriteObject(_GetWorkItemByDate WorkItem AsOf store); return;
            //     }

            //     "Query by text" {
            //         localMacros = @{TfsQueryText=Text}
            //         Wiql = "SELECT * FROM WorkItems WHERE [System.Title] CONTAINS @TfsQueryText OR [System.Description] CONTAINS @TfsQueryText"
            //         WriteObject(_GetWorkItemByWiql Wiql localMacros tp store ); return;
            //     }

            //     "Query by filter" {
            //         Wiql = $"SELECT * FROM WorkItems WHERE {Filter}"
            //         WriteObject(_GetWorkItemByWiql Wiql Macros tp store ); return;
            //     }

            //     "Query by WIQL" {
            // 		this.Log($"Get-TfsWorkItem: Running query by WIQL. Query: {Query}");
            //         WriteObject(_GetWorkItemByWiql Query Macros tp store ); return;
            //     }

            //     "Query by saved query" {
            //         WriteObject(_GetWorkItemBySavedQuery StoredQueryPath Macros tp store ); return;
            //     }
            // }
        }
    }

    //     }
    // }

    // Function _GetWorkItemByRevision(WorkItem, Revision, store)
    // {
    //     if (WorkItem is Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem)
    //     {
    //         ids = @(WorkItem.Id)
    //     }
    //     elseif (WorkItem is int)
    //     {
    //         ids = @(WorkItem)
    //     }
    //     elseif (WorkItem is int[])
    //     {
    //         ids = WorkItem
    //     }
    //     else
    //     {
    //         throw new Exception($"Invalid work item ""{WorkItem}"". Supply either a WorkItem object or one or more integer ID numbers")
    //     }

    //     if (Revision is int && Revision -gt 0)
    //     {
    //         foreach(id in ids)
    //         {
    //             store.GetWorkItem(id, Revision)
    //         }
    //     }
    //     elseif (Revision is int[])
    //     {
    //         if (ids.Count != Revision.Count)
    //         {
    //             throw new Exception("When supplying a list of IDs and Revisions, both must have the same number of elements")
    //         }
    //         for(i = 0; i -le ids.Count-1; i++)
    //         {
    //             store.GetWorkItem(ids[i], Revision[i])
    //         }
    //     }
    //     else
    //     {
    //         foreach(id in ids)
    //         {
    //             store.GetWorkItem(id)
    //         }
    //     }
    // }

    // Function _GetWorkItemByDate(WorkItem, AsOf, store)
    // {
    //     if (WorkItem is Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem)
    //     {
    //         ids = @(WorkItem.Id)
    //     }
    //     elseif (WorkItem is int)
    //     {
    //         ids = @(WorkItem)
    //     }
    //     elseif (WorkItem is int[])
    //     {
    //         ids = WorkItem
    //     }
    //     else
    //     {
    //         throw new Exception($"Invalid work item ""{WorkItem}"". Supply either a WorkItem object or one or more integer ID numbers")
    //     }

    //     if (AsOf is datetime[])
    //     {
    //         if (ids.Count != AsOf.Count)
    //         {
    //             throw new Exception("When supplying a list of IDs and Changed Dates (AsOf), both must have the same number of elements")
    //         }
    //         for(i = 0; i -le ids.Count-1; i++)
    //         {
    //             store.GetWorkItem(ids[i], AsOf[i])
    //         }
    //     }
    //     else
    //     {
    //         foreach(id in ids)
    //         {
    //             store.GetWorkItem(id, AsOf)
    //         }
    //     }
    // }

    // Function _GetWorkItemByWiql(QueryText, Macros, Project, store)
    // {
    // 	if (QueryText -notlike "select*")
    // 	{
    // 		q = Get-TfsWorkItemQueryItem -ItemType Query -Query QueryText -Project Project

    // 		if (! q)
    // 		{
    // 			throw new Exception($"Work item query "{QueryText}" is invalid or non-existent.")
    // 		}

    // 		if (q.Count -gt 1)
    // 		{
    // 			throw new Exception($"Ambiguous query name "{QueryText}". {q.Count} queries were found matching the specified name/pattern:`n`n - " + (q -join "`n - "))
    // 		}

    // 		QueryText = q.QueryText
    // 	}

    //     if (! Macros && ((QueryText -match $"@project") || ({QueryText} -match "@me")))
    //     {
    //         Macros = @{}
    //     }

    //     if (QueryText -match "@project")
    //     {
    // 		if (! Project)
    // 		{
    // 			Project = Get-TfsTeamProject -Current
    // 		}

    //         if (! Macros.ContainsKey("Project"))
    //         {
    //             Macros["Project"] = Project.Name
    //         }
    //     }

    //     if (QueryText -match "@me")
    //     {
    //         user = null
    //         store.TeamProjectCollection.GetAuthenticatedIdentity([ref] user)
    //         Macros["Me"] = user.DisplayName
    //     }

    // 	this.Log($"Get-TfsWorkItem: Running query {QueryText}");

    //     wis = store.Query(QueryText, Macros)

    //     # foreach(wi in wis)
    //     # {
    //     #     if(Fields)
    //     #     {
    //     #         foreach(f in Fields)
    //     #         {
    //     #             wi | Add-Member -Name (_GetEncodedFieldName f.ReferenceName) -MemberType ScriptProperty -Value `
    //     #                 {f.Value}.GetNewClosure() `
    //     #                 {param(Value) f.Value = Value}.GetNewClosure()
    //     #         }
    //     #     }
    //     # }

    //     WriteObject(wis); return;
    // }
}