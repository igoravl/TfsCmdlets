using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
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
        /// <seealso cref="Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem">
        /// A WorkItem object
        /// </seealso>
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Query by revision")]
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Query by date")]
        [Parameter(Position = 0, ParameterSetName = "Get deleted")]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Specifies the title to look up for in a work item. Wildcards are supported. 
        /// When a wildcard is used, matches a portion of the title 
        /// (uses the operator "contains" in the WIQL query). Otherwise, matches the whole field 
        /// with the operator "=", unless -Ever is also specified. In that case, uses the operator 
        /// "was ever".
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string[] Title { get; set; }

        /// <summary>
        /// Specifies the description to look up for in a work item. Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string[] Description { get; set; }

        /// <summary>
        /// Specifies the area path to look up for in a work item. Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string AreaPath { get; set; }

        /// <summary>
        /// Specifies the iteration path to look up for in a work item. Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string IterationPath { get; set; }

        /// <summary>
        /// Specifies the work item type to look up for in a work item. Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [Alias("Type")]
        [SupportsWildcards]
        public string[] WorkItemType { get; set; }

        /// <summary>
        /// Specifies the state (field 'System.State') to look up for in a work item. Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string[] State { get; set; }

        /// <summary>
        /// Specifies the reason (field 'System.Reason') to look up for in a work item. 
        /// Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string[] Reason { get; set; }

        /// <summary>
        /// Specifies the Value Area (field 'Microsoft.VSTS.Common.ValueArea') to look up for in a work item. 
        /// Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string[] ValueArea { get; set; }

        /// <summary>
        /// Specifies the board column to look up for in a work item. 
        /// Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string[] BoardColumn { get; set; }

        /// <summary>
        /// Specifies whether the work item is in the sub-column Doing or Done in a board.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public bool BoardColumnDone { get; set; }

        /// <summary>
        /// Specifies the name or email of the user that created the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public object[] CreatedBy { get; set; }

        /// <summary>
        ///  Specifies the date when the work item was created.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public DateTime[] CreatedDate { get; set; }

        /// <summary>
        /// Specifies the name or email of the user that did the latest change to the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public object ChangedBy { get; set; }

        /// <summary>
        /// Specifies the date of the latest change to the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public DateTime[] ChangedDate { get; set; }

        /// <summary>
        /// Specifies the date of the most recent change to the state of the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public DateTime[] StateChangeDate { get; set; }

        /// <summary>
        /// Specifies the priority of the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public int[] Priority { get; set; }

        /// <summary>
        /// Specifies the tags to look up for in a work item. When multiple tags are supplied, 
        /// they are combined with an OR operator - in other works, returns  work items that 
        /// contain ANY ofthe supplied tags.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public string[] Tags { get; set; }

        /// <summary>
        /// Switches the query to historical query mode, by changing operators to 
        /// "WAS EVER" where possible.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public SwitchParameter Ever { get; set; }

        /// <summary>
        /// Specifies a work item revision number to retrieve. When omitted, returns
        /// the latest revision of the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Query by revision")]
        [Alias("rev")]
        public int Revision { get; set; }

        /// <summary>
        /// Returns the field values as they were defined in the work item revision that
        /// was the latest revision by the date specified.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [Parameter(Mandatory = true, ParameterSetName = "Query by date")]
        public DateTime AsOf { get; set; }

        /// <summary>
        /// Specifies a query written in WIQL (Work Item Query Language)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Query by WIQL")]
        [Alias("WIQL", "QueryText", "SavedQuery", "QueryPath")]
        public string Query { get; set; }

        /// <summary>
        /// Specifies which fields should be retrieved. When omitted, defaults to a set of
        /// standard fields that include Id, Title, Description, some state-related fields and more.
        /// </summary>
        [Parameter()]
        [Parameter(Position = 0, ParameterSetName = "Query by filter")]
        public string[] Fields { get; set; } = DefaultFields;

        /// <summary>
        /// Specifies a filter clause (the portion of a WIQL query after the WHERE keyword).
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Query by filter")]
        public string Where { get; set; }

        /// <summary>
        /// Fetches work items in "time-precision mode": search criteria in WIQL queries 
        /// take into account time information as well, not only dates.
        /// </summary>
        [Parameter(ParameterSetName = "Query by WIQL")]
        [Parameter(ParameterSetName = "Query by filter")]
        [Parameter(ParameterSetName = "Simple query")]
        public SwitchParameter TimePrecision { get; set; }

        /// <summary>
        /// Opens the specified work item in the default web browser.
        /// </summary>
        [Parameter(ParameterSetName = "Query by revision")]
        public SwitchParameter ShowWindow { get; set; }

        /// <summary>
        /// Gets deleted work items.
        /// </summary>
        [Parameter(ParameterSetName = "Get deleted", Mandatory = true)]
        public SwitchParameter Deleted { get; set; }

        /// <summary>
        /// Gets information about all links and attachments in the work item. When omitted, only fields are retrieved.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeLinks { get; set; }

        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter()]
        public object Team { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }

        internal static readonly string[] DefaultFields = new[] {
            "System.AreaPath",
            "System.TeamProject",
            "System.IterationPath",
            "System.WorkItemType",
            "System.State",
            "System.Reason",
            "System.CreatedDate",
            "System.CreatedBy",
            "System.ChangedDate",
            "System.ChangedBy",
            "System.CommentCount",
            "System.Title",
            "System.BoardColumn",
            "System.BoardColumnDone",
            "Microsoft.VSTS.Common.StateChangeDate",
            "Microsoft.VSTS.Common.Priority",
            "Microsoft.VSTS.Common.ValueArea",
            "System.Description",
            "System.Tags" };
    }

    [Exports(typeof(WebApiWorkItem))]
    internal partial class WorkItemDataService : BaseDataService<WebApiWorkItem>
    {
        protected override IEnumerable<WebApiWorkItem> DoGetItems()
        {
            var workItem = GetParameter<object>(nameof(GetWorkItem.WorkItem));
            var revision = GetParameter<int>(nameof(GetWorkItem.Revision));
            var asOf = GetParameter<DateTime?>(nameof(GetWorkItem.AsOf));
            var ever = GetParameter<bool>(nameof(GetWorkItem.Ever));
            var query = GetParameter<string>(nameof(GetWorkItem.Query));
            var filter = GetParameter<string>(nameof(GetWorkItem.Where));
            var fields = GetParameter<string[]>(nameof(GetWorkItem.Fields));
            var showWindow = GetParameter<bool>(nameof(GetWorkItem.ShowWindow));
            var deleted = GetParameter<bool>(nameof(GetWorkItem.Deleted));
            var timePrecision = GetParameter<bool>(nameof(GetWorkItem.TimePrecision));
            var includeLinks = GetParameter<bool>(nameof(GetWorkItem.IncludeLinks));

            WorkItemExpand expand = (includeLinks? WorkItemExpand.Relations: WorkItemExpand.None);

            var (_, tp) = GetCollectionAndProject();
            var client = GetClient<WorkItemTrackingHttpClient>();

            var done = false;

            while (!done) switch (workItem)
                {
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
                                        ["System.State"] = "<Deleted>",
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
                    case WebApiWorkItem wi when showWindow || (includeLinks && wi.Relations == null):
                        {
                            workItem = new[] { (int)wi.Id };
                            continue;
                        }
                    case WebApiWorkItem wi:
                        {
                            yield return wi;
                            continue;
                        }
                    case WorkItemReference wiRef:
                        {
                            workItem = wiRef.Id;
                            continue;
                        }
                    case int[] ids:
                        {
                            foreach (int id in ids) yield return FetchWorkItem(id, revision, asOf, expand, fields, client);

                            yield break;
                        }
                    case null when !string.IsNullOrEmpty(filter):
                        {
                            query = $"SELECT [System.Id] FROM WorkItems WHERE {filter}";
                            filter = null;
                            continue;
                        }
                    case null when !string.IsNullOrEmpty(query) && query.StartsWith("SELECT ", StringComparison.OrdinalIgnoreCase):
                        {
                            var result = client.QueryByWiqlAsync(new Wiql() { Query = query }, tp.Name, timePrecision)
                                .GetResult($"Error running work item query '{query}'");

                            foreach (var wiRef in result.WorkItems)
                            {
                                yield return FetchWorkItem(wiRef.Id, 0, DateTime.MinValue, expand, fields, client);
                            }

                            yield break;
                        }
                    case null when !string.IsNullOrEmpty(query):
                        {
                            var savedQuery = client.GetQueryAsync(tp.Name, query)
                                .GetResult($"Error running work item query '{query}'");
                            query = savedQuery.Wiql;
                            continue;
                        }
                    default:
                        {
                            query = BuildSimpleQuery(timePrecision, ever);
                            continue;
                        }
                }
        }

        private WebApiWorkItem FetchWorkItem(int id, int revision, DateTime? asOf, WorkItemExpand expand, IEnumerable<string> fields, WorkItemTrackingHttpClient client)
        {
            if (expand != WorkItemExpand.None)
            {
                fields = null;
            }
            else
            {
                fields = FixWellKnownFields(fields).ToList();
            }

            try
            {
                if (revision > 0)
                    return client.GetRevisionAsync(id, revision, expand)
                        .GetResult($"Error getting work item '{id}'");
                else if (asOf.HasValue && asOf.Value > DateTime.MinValue)
                    return client.GetWorkItemAsync(id, fields, asOf, expand)
                        .GetResult($"Error getting work item '{id}'");
                else
                    return client.GetWorkItemAsync(id, fields, null, expand)
                        .GetResult($"Error getting work item '{id}'");
            }
            catch (Exception ex)
            {
                Cmdlet.WriteError(new ErrorRecord(ex, ex.InnerException.GetType().Name, ErrorCategory.ReadError, id));
                return null;
            }
        }

        private IEnumerable<string> FixWellKnownFields(IEnumerable<string> fields)
        {
            if (fields == null) yield break;

            foreach (var f in fields)
            {
                if (f.IndexOf('.') > 0)
                {
                    yield return f;
                    continue;
                }

                yield return WellKnownFields.FirstOrDefault(s =>
                    s.Equals($"System.{f}", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals($"Microsoft.VSTS.Common.{f}", StringComparison.OrdinalIgnoreCase)) ?? f;
            }
        }

        private string BuildSimpleQuery(bool timePrecision, bool ever)
        {
            var sb = new StringBuilder();

            sb.Append("SELECT [System.Id] FROM WorkItems Where");

            // TODO: Ever

            var hasCriteria = false;

            foreach (var kvp in SimpleQueryFields)
            {
                if (!HasParameter(kvp.Key)) continue;

                var paramValue = GetParameter<object>(kvp.Key);

                if (hasCriteria)
                {
                    sb.Append(" AND ");
                }
                else
                {
                    sb.Append(" ");
                    hasCriteria = true;
                }

                switch (kvp.Value.Item1)
                {
                    case "Text":
                        {
                            var values = ((IEnumerable<string>)paramValue).ToList();
                            sb.Append("(");
                            for (int i = 0; i < values.Count; i++)
                            {
                                var v = values[i];
                                var op = ever ? "ever" : (v.IsWildcard() ? "contains" : "=");
                                sb.Append($"{(i > 0 ? " OR " : "")}([{kvp.Value.Item2}] {op} '{v}')");
                            }
                            sb.Append(")");
                            break;
                        }
                    case "LongText":
                        {
                            var values = ((IEnumerable<string>)paramValue).ToList();
                            sb.Append("(");
                            for (int i = 0; i < values.Count; i++)
                            {
                                var v = values[i];
                                var op = ever ? "ever contains" : "contains";
                                sb.Append($"{(i > 0 ? " OR " : "")}([{kvp.Value.Item2}] {op} '{v}')");
                            }
                            sb.Append(")");
                            break;
                        }
                    case "Number":
                        {
                            var values = (IEnumerable<int>)paramValue;
                            sb.Append("(");
                            sb.Append(string.Join(" OR ", values.Select(v => $"([{kvp.Value.Item2}] = {v})")));
                            sb.Append(")");
                            break;
                        }
                    case "Date":
                        {
                            var values = (IEnumerable<DateTime>)paramValue;
                            var format = $"yyyy-MM-dd HH:mm:ss{(timePrecision ? "HH:mm:ss" : "")}";
                            sb.Append("(");
                            sb.Append(string.Join(" OR ", values.Select(v => $"([{kvp.Value.Item2}] = {v.ToString(format)})")));
                            sb.Append(")");
                            break;
                        }
                    case "Tree":
                        {
                            var values = (IEnumerable<string>)paramValue;
                            sb.Append("(");
                            sb.Append(string.Join(" OR ", values.Select(v => $"([{kvp.Value.Item2}] UNDER '{v}')")));
                            sb.Append(")");
                            break;
                        }
                    case "Boolean":
                        {
                            var v = (bool)paramValue;
                            sb.Append($"([{kvp.Value.Item2}] = {v})");
                            break;
                        }
                    case "Project":
                        {
                            if (!HasParameter("Project")) continue;

                            var (_, tp) = GetCollectionAndProject();

                            sb.Append($"([System.TeamProject] = '{tp.Name}')");

                            break;
                        }
                }
            }

            // TODO: AsOf

            // TODO: Set Team Context

            var query = sb.ToString().Trim();

            if (query.EndsWith(" FROM WorkItems Where"))
            {
                throw new ArgumentException("No filter arguments have been specified. Unable to perform a simple query.");
            }

            Log($"Simple query generated: \"{query}\"");

            return query;
        }

        private static readonly string[] WellKnownFields = new[] {
            "System.Id",
            "System.History",
            "System.AreaPath",
            "System.TeamProject",
            "System.IterationPath",
            "System.WorkItemType",
            "System.State",
            "System.Reason",
            "System.CreatedDate",
            "System.CreatedBy",
            "System.ChangedDate",
            "System.ChangedBy",
            "System.CommentCount",
            "System.Title",
            "System.BoardColumn",
            "System.BoardColumnDone",
            "Microsoft.VSTS.Common.StateChangeDate",
            "Microsoft.VSTS.Common.Priority",
            "Microsoft.VSTS.Common.ValueArea",
            "System.Description",
            "System.Tags" };

        private static readonly Dictionary<string, Tuple<string, string>> SimpleQueryFields = new Dictionary<string, Tuple<string, string>>()
        {
            ["AreaPath"] = new Tuple<string, string>("Tree", "System.AreaPath"),
            ["BoardColumn"] = new Tuple<string, string>("Text", "System.BoardColumn"),
            ["BoardColumnDone"] = new Tuple<string, string>("Boolean", "System.BoardColumnDone"),
            ["ChangedBy"] = new Tuple<string, string>("Identifier", "System.ChangedBy"),
            ["ChangedDate"] = new Tuple<string, string>("Date", "System.ChangedDate"),
            ["CreatedBy"] = new Tuple<string, string>("Identifier", "System.CreatedBy"),
            ["CreatedDate"] = new Tuple<string, string>("Date", "System.CreatedDate"),
            ["Description"] = new Tuple<string, string>("Text", "System.Description"),
            ["IterationPath"] = new Tuple<string, string>("Tree", "System.IterationPath"),
            ["Priority"] = new Tuple<string, string>("Number", "Microsoft.VSTS.Common.Priority"),
            ["Project"] = new Tuple<string, string>("Project", "System.TeamProject"),
            ["Reason"] = new Tuple<string, string>("Text", "System.Reason"),
            ["State"] = new Tuple<string, string>("Text", "System.State"),
            ["StateChangeDate"] = new Tuple<string, string>("Date", "Microsoft.VSTS.Common.StateChangeDate"),
            ["Tags"] = new Tuple<string, string>("LongText", "System.Tags"),
            ["Title"] = new Tuple<string, string>("Text", "System.Title"),
            ["ValueArea"] = new Tuple<string, string>("Text", "Microsoft.VSTS.Common.ValueArea"),
            ["WorkItemType"] = new Tuple<string, string>("Text", "System.WorkItemType")
        };
    }
}
