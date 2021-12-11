using System.Management.Automation;
using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;

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
}