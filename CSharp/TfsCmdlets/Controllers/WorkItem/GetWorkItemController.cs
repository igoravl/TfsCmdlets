using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class GetWorkItemController
    {
        [Import]
        private IProcessUtil ProcessUtil { get; set; }

        private const int MAX_WORKITEMS = 200;

        protected override IEnumerable Run()
        {
            var client = GetClient<WorkItemTrackingHttpClient>();
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
                expand = IncludeLinks ? WorkItemExpand.All : (ShowWindow ? WorkItemExpand.Links : WorkItemExpand.None);

                if (Fields.Any(s => s.Equals("*")))
                {
                    fields = IdField;
                    expand = IncludeLinks ? WorkItemExpand.All : WorkItemExpand.Fields;
                }
                else
                {
                    fields = FixWellKnownFields(Fields);
                }
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
                            yield return GetWorkItemById((int)wi.Id, WorkItemExpand.None, null, client);
                            break;
                        }
                    case WebApiWorkItem wi:
                        {
                            yield return wi;
                            break;
                        }
                    case int id when Deleted:
                        {
                            yield return GetWorkItemById(id, WorkItemExpand.None, null, client);
                            break;
                        }
                    case int id:
                        {
                            if (!Has_Revision)
                            {
                                ids.Add(id);
                                continue;
                            }
                            yield return GetWorkItemById(id, expand, fields, client);
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
                            yield return GetWorkItemById(id, expand, fields, client);
                            break;
                        }
                    case null when !string.IsNullOrEmpty(Where):
                        {
                            var fieldList = expand == WorkItemExpand.None ? string.Join(",", fields) : "*";
                            var wiql = $"SELECT {fieldList} FROM WorkItems WHERE {Where}";

                            foreach (var wi in GetWorkItemsByWiql(wiql, expand, client)) yield return wi;

                            break;
                        }
                    case null when !string.IsNullOrEmpty(Wiql):
                        {
                            foreach (var wi in GetWorkItemsByWiql(Wiql, expand, client)) yield return wi;

                            break;
                        }
                    case null when !string.IsNullOrEmpty(SavedQuery):
                        {
                            var wiql = GetItem<QueryHierarchyItem>(new { Query = SavedQuery, ItemType = "Query" }).Wiql;

                            foreach (var wi in GetWorkItemsByWiql(wiql, expand, client)) yield return wi;

                            break;
                        }
                    case null when Deleted:
                        {
                            foreach (var wi in GetDeletedWorkItems(null, client))
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

                            foreach (var wi in GetWorkItemsByWiql(wiql, expand, client)) yield return wi;

                            break;
                        }
                }
            }

            if (ids.Count == 0) yield break;

            foreach (var wi in GetWorkItemsById(ids, Has_AsOf ? AsOf : null, expand, expand != WorkItemExpand.None ? null : fields, client))
            {
                if (ShowWindow)
                {
                    ProcessUtil.OpenInBrowser(((ReferenceLink)wi.Links.Links["html"]).Href);
                    continue;
                }

                yield return wi;
            }

        }

        private WebApiWorkItem GetWorkItemById(int id, WorkItemExpand expand, IEnumerable<string> fields, WorkItemTrackingHttpClient client)
        {
            WebApiWorkItem wi = null;

            try
            {
                if (Deleted)
                {
                    return GetDeletedWorkItems(new[] { id }, client).FirstOrDefault();
                }
                else if (Has_Revision)
                {
                    wi = client.GetRevisionAsync(id, Revision, expand)
                        .GetResult($"Error getting work item '{id}'");
                }
                else if (Has_AsOf)
                {
                    wi = client.GetWorkItemAsync(id, fields, AsOf, expand)
                        .GetResult($"Error getting work item '{id}'");
                }
                else
                {
                    wi = client.GetWorkItemAsync(id, fields, null, expand)
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

        private IEnumerable<WebApiWorkItem> GetWorkItemsById(IEnumerable<int> ids, DateTime? asOf, WorkItemExpand expand, IEnumerable<string> fields, WorkItemTrackingHttpClient client)
        {
            IList<int> idList;

            if ((idList = (ids ?? Enumerable.Empty<int>()).ToList()).Count == 0)
            {
                yield break;
            }

            if(idList.Count <= MAX_WORKITEMS)
            {
                var wis = client.GetWorkItemsAsync(idList, fields, asOf, expand, WorkItemErrorPolicy.Fail)
                    .GetResult("Error getting work items");

                foreach(var wi in wis) yield return wi;
                
                yield break;
            }

            Logger.LogWarn($"Your query resulted in {idList.Count} work items, therefore items must be fetched one at a time. This may take a while. For best performance, write queries that return less than 200 items.");

            foreach(var id in idList)
            {
                yield return GetWorkItemById(id, expand, fields, client);
            }
        }

        private IEnumerable<WebApiWorkItem> GetDeletedWorkItems(IEnumerable<int> ids, WorkItemTrackingHttpClient client)
        {
            IEnumerable<WorkItemDeleteReference> result;

            if (ids != null)
            {
                result = client.GetDeletedWorkItemsAsync(ids)
                    .GetResult($"Error getting deleted work item {ids}");
            }
            else
            {
                IList<WorkItemDeleteShallowReference> refs;
                var projectName = GetItem<WebApiTeamProject>(new { Deleted = false }).Name;

                refs = client.GetDeletedWorkItemShallowReferencesAsync(projectName)
                   .GetResult($"Error getting references for deleted work items");

                if (refs.Count == 0) yield break;

                result = client.GetDeletedWorkItemsAsync(refs.Select(r => (int)r.Id))
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

        private IEnumerable<WebApiWorkItem> GetWorkItemsByWiql(string query, WorkItemExpand expand, WorkItemTrackingHttpClient client)
        {
            TeamContext tc = null;
            ProjectReference pr = null;

            if (Data.TryGetTeam(out var team)) tc = new TeamContext(team.ProjectId, team.Id);
            if (Data.TryGetProject(out var project)) pr = new ProjectReference { Id = project.Id };

            var wiql = new Wiql { Query = query };

            WorkItemQueryResult result;

            if (tc != null)
            {
                result = client.QueryByWiqlAsync(wiql, tc, TimePrecision)
                    .GetResult($"Error querying work items");
            }
            else if (pr != null)
            {
                result = client.QueryByWiqlAsync(wiql, pr.Id, TimePrecision)
                    .GetResult($"Error querying work items");
            }
            else
            {
                result = client.QueryByWiqlAsync(wiql, TimePrecision)
                    .GetResult($"Error querying work items");
            }

            return GetWorkItemsById(result.WorkItems.Select(w => w.Id), result.AsOf, expand, expand != WorkItemExpand.None ? null : result.Columns.Select(f => f.ReferenceName).ToList(), client);
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
            var sb = new StringBuilder();

            sb.Append($"SELECT {string.Join(", ", fields)} FROM WorkItems Where");

            var hasCriteria = false;

            foreach (var kvp in SimpleQueryFields)
            {
                if (!Parameters.HasParameter(kvp.Key)) continue;

                var paramValue = Parameters.Get<object>(kvp.Key);

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
                            var values = ((paramValue as IEnumerable<string>) ?? (IEnumerable<string>)new[] { (string)paramValue }).ToList();
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
                            var op = Ever ? "ever" : "=";
                            sb.Append("(");
                            sb.Append(string.Join(" OR ", values.Select(v => $"([{kvp.Value.Item2}] {op} {v})")));
                            sb.Append(")");
                            break;
                        }
                    case "Date":
                        {
                            var values = ((paramValue as IEnumerable<DateTime>) ?? (IEnumerable<DateTime>)new[] { (DateTime)paramValue }).ToList();
                            var op = Ever ? "ever" : "=";
                            var format = $"yyyy-MM-dd HH:mm:ss{(TimePrecision ? "HH:mm:ss" : "")}";
                            sb.Append("(");
                            sb.Append(string.Join(" OR ", values.Select(v => $"([{kvp.Value.Item2}] {op} {v.ToString(format)})")));
                            sb.Append(")");
                            break;
                        }
                    case "Tree":
                        {
                            var values = ((paramValue as IEnumerable<string>) ?? (IEnumerable<string>)new[] { (string)paramValue }).ToList();
                            sb.Append("(");
                            sb.Append(string.Join(" OR ", values.Select(v => $"([{kvp.Value.Item2}] UNDER '{v}')")));
                            sb.Append(")");
                            break;
                        }
                    case "Boolean":
                        {
                            var values = ((paramValue as IEnumerable<bool>) ?? (IEnumerable<bool>)new[] { (bool)paramValue }).ToList();
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
            }

            if (!hasCriteria)
            {
                throw new ArgumentException("No filter arguments have been specified. Unable to perform a simple query.");
            }

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

        private static readonly string[] IdField = new[] {
            "System.Id" };

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
