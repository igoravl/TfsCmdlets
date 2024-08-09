using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    /// <summary>
    /// Gets one or more Work Item Type definitions from a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, DefaultParameterSetName = "Get by type", OutputType = typeof(WebApiWorkItemType))]
    partial class GetWorkItemType
    {
        /// <summary>
        /// Specifies one or more work item type names to return. Wildcards are supported. 
        /// When omitted, returns all work item types in the given team project.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by type")]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Type { get; set; } = "*";

        /// <summary>
        /// Speficies a work item whose corresponding type should be returned.
        /// </summary>
        [Parameter(ParameterSetName = "Get by work item", Mandatory = true)]
        public object WorkItem { get; set; }
    }

    [CmdletController(typeof(WebApiWorkItemType), Client = typeof(IWorkItemTrackingHttpClient))]
    partial class GetWorkItemTypeController
    {
        protected override IEnumerable Run()
        {
            foreach (var input in Type)
            {
                switch (input)
                {
                    case object _ when Has_WorkItem:
                        {
                            var wi = Data.GetItem<WebApiWorkItem>(new { WorkItem });
                            if (wi == null)
                            {
                                Logger.LogError(new ArgumentException($"Work item '{WorkItem}' not found"));
                                break;
                            }
                            var type = wi.Fields["System.WorkItemType"].ToString();
                            yield return Client.GetWorkItemTypeAsync(type: type, project: Project.Id)
                                .GetResult($"Error getting type '{type}' from work item '{WorkItem}'");
                            break;
                        }
                    case WebApiWorkItemType t:
                        {
                            yield return t;
                            break;
                        }
                    case string s when s.IsWildcard():
                        {
                            yield return Client.GetWorkItemTypesAsync(Project.Id)
                                .GetResult($"Error getting type(s) '{s}'")
                                .Where(t1 => t1.Name.IsLike(s));
                            break;
                        }
                    case string s:
                        {
                            yield return Client.GetWorkItemTypeAsync(type: s, project: Project.Id)
                                .GetResult($"Error getting type '{s}'");
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent work item type '{Type}'"));
                            break;
                        }
                }
            }
        }
    }
}