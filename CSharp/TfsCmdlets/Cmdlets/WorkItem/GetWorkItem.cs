using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Gets the contents of one or more work items.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, DefaultParameterSetName = "Query by revision", OutputType = typeof(WebApiWorkItem), NoAutoPipeline = true)]
    partial class GetWorkItem
    {
        /// <summary>
        /// HELP_PARAM_WORKITEM
        /// </summary>
        /// <seealso cref="Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem">
        /// A WorkItem object
        /// </seealso>
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Query by revision", ValueFromPipeline = true)]
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Query by date", ValueFromPipeline = true)]
        [Parameter(Position = 0, ParameterSetName = "Get deleted", ValueFromPipeline = true)]
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
        [Alias("WasEver")]
        public SwitchParameter Ever { get; set; }

        /// <summary>B
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
        [Alias("Query", "QueryText")]
        public string Wiql { get; set; }

        /// <summary>
        /// Specifies the path of a saved query to be executed. 
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Query by saved query")]
        [Alias("QueryPath")]
        public string SavedQuery { get; set; }

        /// <summary>
        /// Specifies the path of a saved query to be executed. 
        /// </summary>
        [Parameter(ParameterSetName = "Query by saved query")]
        public Hashtable QueryParameters { get; set; }

        /// <summary>
        /// Specifies which fields should be retrieved. When omitted, defaults to a set of
        /// standard fields that include Id, Title, Description, some state-related fields and more. 
        /// To retrive all fields, pass an asterisk ('*') to this argument.
        /// </summary>
        [Parameter]
        [Parameter(ParameterSetName = "Query by date")]
        [Parameter(ParameterSetName = "Query by revision")]
        [Parameter(ParameterSetName = "Query by filter")]
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        [ValidateNotNullOrEmpty]
        public string[] Fields { get; set; } = new[] 
            {"System.AreaPath", "System.TeamProject", "System.IterationPath",
             "System.WorkItemType", "System.State", "System.Reason",
             "System.CreatedDate", "System.CreatedBy", "System.ChangedDate",
             "System.ChangedBy", "System.CommentCount", "System.Title",
             "System.BoardColumn", "System.BoardColumnDone", "Microsoft.VSTS.Common.StateChangeDate",
             "Microsoft.VSTS.Common.Priority", "Microsoft.VSTS.Common.ValueArea", "System.Description",
             "System.Tags" };

        /// <summary>
        /// Specifies a filter clause (the portion of a WIQL query after the WHERE keyword).
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Query by filter")]
        public string Where { get; set; }

        /// <summary>
        /// Fetches work items in "time-precision mode": search criteria in WIQL queries 
        /// take into account time information as well, not only dates.
        /// </summary>
        [Parameter]
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
        [Parameter]
        public SwitchParameter IncludeLinks { get; set; }
    }

    [CmdletController(typeof(WebApiWorkItem), Client=typeof(IWorkItemTrackingHttpClient))]
    partial class GetWorkItemController
    {
        [Import]
        private IProcessUtil ProcessUtil { get; set; }

        [Import]
        private INodeUtil NodeUtil { get; set; }

        private const int MAX_WORKITEMS = 200;

        protected override IEnumerable Run()
        {
            var expand = IncludeLinks ? WorkItemExpand.All : WorkItemExpand.Fields;
            IEnumerable<string> fields = null;

            var hasProject = Data.TryGetProject(out var _, new { Deleted = false });

            if (Deleted && !hasProject)
            {
                throw new ArgumentException($"'{Parameters.Get<object>("Project")}' is not a valid project, which is required to get deleted work items. Either supply a valid -Project argument or use Connect-TfsTeamProject prior to invoking this cmdlet.");
            }

            if (Has_SavedQuery && !hasProject)
            {
                throw new ArgumentException($"'{Parameters.Get<object>("Project")}' is not a valid project, which is required to execute a saved query. Either supply a valid -Project argument or use Connect-TfsTeamProject prior to invoking this cmdlet.");
            }

            if (!Deleted && Fields.Length > 0)
            {
                expand = IncludeLinks ? WorkItemExpand.All : (
                    ShowWindow ? WorkItemExpand.Links : Fields[0] == "*" ? WorkItemExpand.Fields : WorkItemExpand.None);
                fields = FixWellKnownFields(Fields).ToList();
            }

            var ids = new List<int>();

            foreach (var input in WorkItem)
            {
                var workItem = input switch
                {
                    string s when int.TryParse(s, out var id) => id,
                    WebApiWorkItem wi when IncludeLinks && wi.Relations == null => wi.Id,
                    WorkItemReference wiRef => wiRef.Id,
                    WorkItemRelation rel => new Uri(rel.Url),
                    _ => input
                };

                switch (workItem)
                {
                    case WebApiWorkItem wi when Deleted:
                        {
                            yield return GetWorkItemById((int)wi.Id, WorkItemExpand.None, null, Client);
                            break;
                        }
                    case WebApiWorkItem wi:
                        {
                            yield return wi;
                            break;
                        }
                    case int id when Deleted:
                        {
                            yield return GetWorkItemById(id, WorkItemExpand.None, null, Client);
                            break;
                        }
                    case int id:
                        {
                            if (!Has_Revision)
                            {
                                ids.Add(id);
                                continue;
                            }
                            yield return GetWorkItemById(id, expand, fields, Client);
                            break;
                        }
                    case Uri url:
                        {
                            var lastSegment = url.Segments[url.Segments.Length - 1];
                            var length = url.LocalPath.Length - lastSegment.Length;

                            if (!url.LocalPath.Substring(0, length).EndsWith("/_apis/wit/workItems/", StringComparison.OrdinalIgnoreCase)) continue;
                            if (!int.TryParse(lastSegment, out var id)) continue;

                            if (!Has_Revision)
                            {
                                ids.Add(id);
                                continue;
                            }
                            yield return GetWorkItemById(id, expand, fields, Client);
                            break;
                        }
                    case null when !string.IsNullOrEmpty(Where):
                        {
                            var fieldList = expand == WorkItemExpand.None ? string.Join(",", fields) : "*";
                            var wiql = $"SELECT {fieldList} FROM WorkItems WHERE {Where}";

                            foreach (var wi in GetWorkItemsByWiql(wiql, expand, Client)) yield return wi;

                            break;
                        }
                    case null when !string.IsNullOrEmpty(Wiql):
                        {
                            foreach (var wi in GetWorkItemsByWiql(Wiql, expand, Client)) yield return wi;

                            break;
                        }
                    case null when !string.IsNullOrEmpty(SavedQuery):
                        {
                            var wiql = GetItem<QueryHierarchyItem>(new { Query = SavedQuery, ItemType = "Query" }).Wiql;

                            foreach (var wi in GetWorkItemsByWiql(wiql, expand, Client)) yield return wi;

                            break;
                        }
                    case null when Deleted:
                        {
                            foreach (var wi in GetDeletedWorkItems(null, Client))
                            {
                                yield return wi;
                            }
                            yield break;
                        }
                    default:
                        {
                            if (!ParameterSetName.Equals("Simple query"))
                            {
                                throw new ArgumentException($"Invalid work item '{workItem}'");
                            }

                            var wiql = BuildSimpleQuery(fields);

                            foreach (var wi in GetWorkItemsByWiql(wiql, expand, Client)) yield return wi;

                            break;
                        }
                }
            }

            if (ids.Count == 0) yield break;

            foreach (var wi in GetWorkItemsById(ids, Has_AsOf ? AsOf : null, expand, expand != WorkItemExpand.None ? null : fields, Client))
            {
                if (ShowWindow)
                {
                    ProcessUtil.OpenInBrowser(((ReferenceLink)wi.Links.Links["html"]).Href);
                    continue;
                }

                yield return wi;
            }

        }

        private WebApiWorkItem GetWorkItemById(int id, WorkItemExpand expand, IEnumerable<string> fields, IWorkItemTrackingHttpClient Client)
        {
            WebApiWorkItem wi = null;

            try
            {
                if (Deleted)
                {
                    return GetDeletedWorkItems(new[] { id }, Client).FirstOrDefault();
                }
                else if (Has_Revision)
                {
                    wi = Client.GetRevisionAsync(id, Revision, expand)
                        .GetResult($"Error getting work item '{id}'");
                }
                else if (Has_AsOf)
                {
                    wi = Client.GetWorkItemAsync(id, fields, AsOf, expand)
                        .GetResult($"Error getting work item '{id}'");
                }
                else
                {
                    wi = Client.GetWorkItemAsync(id, fields, null, expand)
                        .GetResult($"Error getting work item '{id}'");
                }

                if (ShowWindow)
                {
                    ProcessUtil.OpenInBrowser(((ReferenceLink)wi.Links.Links["html"]).Href);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            return wi;
        }

        private IEnumerable<WebApiWorkItem> GetWorkItemsById(IEnumerable<int> ids, DateTime? asOf, WorkItemExpand expand, IEnumerable<string> fields, IWorkItemTrackingHttpClient Client)
        {
            IList<int> idList;

            if ((idList = (ids ?? Enumerable.Empty<int>()).ToList()).Count == 0)
            {
                yield break;
            }

            if (idList.Count <= MAX_WORKITEMS)
            {
                var wis = Client.GetWorkItemsAsync(idList, fields, asOf, expand, WorkItemErrorPolicy.Fail)
                    .GetResult("Error getting work items");

                foreach (var wi in wis) yield return wi;

                yield break;
            }

            Logger.LogWarn($"Your query resulted in {idList.Count} work items, therefore items must be fetched one at a time. This may take a while. For best performance, write queries that return less than 200 items.");

            foreach (var id in idList)
            {
                yield return GetWorkItemById(id, expand, fields, Client);
            }
        }

        private IEnumerable<WebApiWorkItem> GetDeletedWorkItems(IEnumerable<int> ids, IWorkItemTrackingHttpClient Client)
        {
            IEnumerable<WorkItemDeleteReference> result;

            if (ids != null)
            {
                result = Client.GetDeletedWorkItemsAsync(ids)
                    .GetResult($"Error getting deleted work item {ids}");
            }
            else
            {
                IList<WorkItemDeleteShallowReference> refs;
                var projectName = GetItem<WebApiTeamProject>(new { Deleted = false }).Name;

                refs = Client.GetDeletedWorkItemShallowReferencesAsync(projectName)
                   .GetResult($"Error getting references for deleted work items");

                if (refs.Count == 0) yield break;

                result = Client.GetDeletedWorkItemsAsync(refs.Select(r => (int)r.Id))
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
        }

        private IEnumerable<WebApiWorkItem> GetWorkItemsByWiql(string query, WorkItemExpand expand, IWorkItemTrackingHttpClient Client)
        {
            TeamContext tc = null;
            ProjectReference pr = null;

            if (Data.TryGetTeam(out var team)) tc = new TeamContext(team.ProjectId, team.Id);
            if (Data.TryGetProject(out var project)) pr = new ProjectReference { Id = project.Id };

            var wiql = new Wiql { Query = query };

            WorkItemQueryResult result;

            if (tc != null)
            {
                result = Client.QueryByWiqlAsync(wiql, tc, TimePrecision)
                    .GetResult($"Error querying work items");
            }
            else if (pr != null)
            {
                result = Client.QueryByWiqlAsync(wiql, pr.Id, TimePrecision)
                    .GetResult($"Error querying work items");
            }
            else
            {
                result = Client.QueryByWiqlAsync(wiql, TimePrecision)
                    .GetResult($"Error querying work items");
            }

            return GetWorkItemsById(result.WorkItems.Select(w => w.Id), result.AsOf, expand, expand != WorkItemExpand.None ? null : result.Columns.Select(f => f.ReferenceName).ToList(), Client);
        }

        private IEnumerable<string> FixWellKnownFields(IEnumerable<string> fields)
        {
            if (fields == null) yield break;

            foreach (var f in fields)
            {
                yield return f.IndexOf('.') > 0 ?
                f :
                WellKnownFields.FirstOrDefault(s => s.EndsWith(f, StringComparison.OrdinalIgnoreCase)) ?? f;
            }
        }

        private string BuildSimpleQuery(IEnumerable<string> fields)
        {
            var criteria = new List<string>();
            StringBuilder sb;

            foreach (var kvp in SimpleQueryFields)
            {
                if (!Parameters.HasParameter(kvp.Key)) continue;

                sb = new StringBuilder();
                var paramValue = Parameters.Get<object>(kvp.Key);

                switch (kvp.Value.Item1)
                {
                    case "Text":
                        {
                            var values = ((paramValue as IEnumerable<string>) ?? (IEnumerable<string>)new[] { (string)paramValue }).ToList();
                            if (values.Count == 0) continue;

                            sb.Append("(");
                            for (int i = 0; i < values.Count; i++)
                            {
                                var v = values[i];
                                var op = Ever ? "ever" : (v.IsWildcard() ? "contains" : "=");
                                sb.Append($"{(i > 0 ? " OR " : "")}([{kvp.Value.Item2}] {op} '{v}')");
                            }
                            sb.Append(")");
                            break;
                        }
                    case "LongText":
                        {
                            var values = ((paramValue as IEnumerable<string>) ?? (IEnumerable<string>)new[] { (string)paramValue }).ToList();
                            if (values.Count == 0) continue;

                            sb.Append("(");
                            for (int i = 0; i < values.Count; i++)
                            {
                                var v = values[i];
                                var op = Ever ? "ever contains" : "contains";
                                sb.Append($"{(i > 0 ? " OR " : "")}([{kvp.Value.Item2}] {op} '{v}')");
                            }
                            sb.Append(")");
                            break;
                        }
                    case "Number":
                        {
                            var values = ((paramValue as IEnumerable<int>) ?? (IEnumerable<int>)new[] { (int)paramValue }).ToList();
                            if (values.Count == 0) continue;

                            var op = Ever ? "ever" : "=";
                            sb.Append("(");
                            sb.Append(string.Join(" OR ", values.Select(v => $"([{kvp.Value.Item2}] {op} {v})")));
                            sb.Append(")");
                            break;
                        }
                    case "Date":
                        {
                            var values = ((paramValue as IEnumerable<DateTime>) ?? (IEnumerable<DateTime>)new[] { (DateTime)paramValue }).ToList();
                            if (values.Count == 0) continue;

                            var op = Ever ? "ever" : "=";
                            var format = $"yyyy-MM-dd HH:mm:ss{(TimePrecision ? "HH:mm:ss" : "")}";
                            sb.Append("(");
                            sb.Append(string.Join(" OR ", values.Select(v => $"([{kvp.Value.Item2}] {op} {v.ToString(format)})")));
                            sb.Append(")");
                            break;
                        }
                    case "Tree":
                        {
                            var values = ((paramValue as IEnumerable<string>) ?? (IEnumerable<string>)new[] { (string)paramValue })
                                .Where(v => !string.IsNullOrEmpty(v) && !v.Equals("\\")).ToList();
                            if (values.Count == 0) continue;

                            var projectName = Project.Name;

                            sb.Append("(");
                            sb.Append(string.Join(" OR ", values.Select(v => $"([{kvp.Value.Item2}] UNDER '{NodeUtil.NormalizeNodePath(v, projectName, includeTeamProject: true, includeLeadingSeparator: false, includeTrailingSeparator: false)}')")));
                            sb.Append(")");
                            break;
                        }
                    case "Boolean":
                        {
                            var values = ((paramValue as IEnumerable<bool>) ?? (IEnumerable<bool>)new[] { (bool)paramValue }).ToList();
                            if (values.Count == 0) continue;

                            var op = Ever ? "ever" : "=";
                            sb.Append("(");
                            sb.Append(string.Join(" OR ", values.Select(v => $"([{kvp.Value.Item2}] {op} {v})")));
                            sb.Append(")");
                            break;
                        }
                    case "Project":
                        {
                            if (!Has_Project) continue;

                            sb.Append($"([System.TeamProject] = '{Project.Name}')");

                            break;
                        }
                }

                if (sb.Length > 0)
                {
                    criteria.Add(sb.ToString());
                }
            }

            if (criteria.Count == 0)
            {
                throw new ArgumentException("No filter arguments have been specified. Unable to perform a simple query.");
            }

            sb = new StringBuilder($"SELECT {string.Join(", ", fields)} FROM WorkItems WHERE {string.Join(" AND ", criteria)}");

            if (Has_AsOf)
            {
                var asOf = AsOf.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ssZ");
                sb.Append($" ASOF '{asOf}'");
            }

            var query = sb.ToString().Trim();

            Logger.Log($"Simple query generated: \"{query}\"");

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
           "System.AssignedTo",
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

        internal static readonly Dictionary<string, Tuple<string, string>> SimpleQueryFields = new()
        {
            ["AreaPath"] = new Tuple<string, string>("Tree", "System.AreaPath"),
            ["AssignedTo"] = new Tuple<string, string>("Identifier", "System.AssignedTo"),
            ["BoardColumn"] = new Tuple<string, string>("Text", "System.BoardColumn"),
            ["BoardColumnDone"] = new Tuple<string, string>("Boolean", "System.BoardColumnDone"),
            ["ChangedBy"] = new Tuple<string, string>("Identifier", "System.ChangedBy"),
            ["ChangedDate"] = new Tuple<string, string>("Date", "System.ChangedDate"),
            ["CreatedBy"] = new Tuple<string, string>("Identifier", "System.CreatedBy"),
            ["CreatedDate"] = new Tuple<string, string>("Date", "System.CreatedDate"),
            ["Description"] = new Tuple<string, string>("LongText", "System.Description"),
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