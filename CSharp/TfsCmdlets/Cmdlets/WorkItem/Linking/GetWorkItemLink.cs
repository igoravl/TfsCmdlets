using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Gets the links in a work item.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WorkItemRelation))]
    partial class GetWorkItemLink
    {
        /// <summary>
        /// HELP_PARAM_WORKITEM
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Returns only the specified link types. When omitted, returns all link types.
        /// </summary>
        [Parameter]
        public WorkItemLinkType LinkType { get; set; }

        /// <summary>
        /// Includes attachment information alongside links. When omitted, only links are retrieved.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeAttachments { get; set; }
    }
}