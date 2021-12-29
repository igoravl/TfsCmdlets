using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Cmdlets.WorkItem;

namespace TfsCmdlets.Controllers.WorkItem
{
    [CmdletController(typeof(WebApiWorkItem))]
    partial class GetWorkItemController
    {
        [Import]
        private IProcessUtil ProcessUtil { get; set; }

        public override IEnumerable<WebApiWorkItem> Invoke()
        {
            var workItem = Parameters.Get<object>(nameof(GetWorkItem.WorkItem));
            var revision = Parameters.Get<int>(nameof(GetWorkItem.Revision));
            var asOf = Parameters.Get<DateTime?>(nameof(GetWorkItem.AsOf));
            var ever = Parameters.Get<bool>(nameof(GetWorkItem.Ever));
            var query = Parameters.Get<string>(nameof(GetWorkItem.Query));
            var filter = Parameters.Get<string>(nameof(GetWorkItem.Where));
            var fields = Parameters.Get<object>(nameof(GetWorkItem.Fields)) as string[];
            var showWindow = Parameters.Get<bool>(nameof(GetWorkItem.ShowWindow));
            var deleted = Parameters.Get<bool>(nameof(GetWorkItem.Deleted));
            var timePrecision = Parameters.Get<bool>(nameof(GetWorkItem.TimePrecision));
            var includeLinks = Parameters.Get<bool>(nameof(GetWorkItem.IncludeLinks));
            var expand = (includeLinks ? WorkItemExpand.All : WorkItemExpand.Fields);

            var tp = Data.GetProject();
            var client = Data.GetClient<WorkItemTrackingHttpClient>();

            var done = false;

            while (!done) switch (workItem)
                {
                    case int id:
                        {
                            workItem = FetchWorkItem(id, revision, asOf, expand, fields, client);
                            continue;
                        }
                    case string s when int.TryParse(s, out var id):
                        {
                            workItem = FetchWorkItem(id, revision, asOf, expand, fields, client);
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
                    case WebApiWorkItem wi when showWindow:
                        {
                            ProcessUtil.OpenInBrowser(((ReferenceLink)wi.Links.Links["html"]).Href);
                            yield break;
                        }
                    case WebApiWorkItem wi when includeLinks && wi.Relations == null:
                        {
                            workItem = new[] { (int)wi.Id };
                            continue;
                        }
                    case WebApiWorkItem wi:
                        {
                            yield return wi;
                            yield break;
                        }
                    case WorkItemReference wiRef:
                        {
                            workItem = wiRef.Id;
                            continue;
                        }
                    case WorkItemRelation rel:
                        {
                            workItem = new Uri(rel.Url);
                            continue;
                        }
                    case Uri url:
                        {
                            if (!url.LocalPath
                                .Substring(0, url.LocalPath.Length - url.Segments[url.Segments.Length - 1].Length)
                                .EndsWith("/_apis/wit/workItems/", StringComparison.OrdinalIgnoreCase)) yield break;

                            if (!int.TryParse(url.Segments[url.Segments.Length - 1], out var id)) yield break;

                            workItem = id;
                            continue;
                        }
                    case int[] ids:
                        {
                            if (showWindow)
                            {
                                Logger.LogWarn("ShowWindow is being ignored, since it cannot be used with multiple work items");
                            }

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
                Logger.LogError(ex, ex.InnerException.GetType().Name, ErrorCategory.ReadError, id);
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
                            if (!Parameters.HasParameter("Project")) continue;

                            var tp = Data.GetProject();

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
