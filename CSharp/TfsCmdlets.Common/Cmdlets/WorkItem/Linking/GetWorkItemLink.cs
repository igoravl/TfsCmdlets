using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using TfsCmdlets.Services;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Gets the links in a work item.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemLink")]
    [OutputType(typeof(WorkItemRelation))]
    public class GetWorkItemLink : GetCmdletBase<WorkItemRelation>
    {
        /// <summary>
        /// HELP_PARAM_WORKITEM
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Includes attachment information alongside links. When omitted, only links are retrieved.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeAttachments { get; set; }
    }

    [Exports(typeof(WorkItemRelation))]
    internal partial class WorkItemLinkDataService : BaseDataService<WorkItemRelation>
    {
        protected override IEnumerable<WorkItemRelation> DoGetItems()
        {
            var wi = this.GetItem<WebApiWorkItem>(new {
                WorkItem = GetParameter<object>(nameof(GetWorkItemLink.WorkItem)),
                IncludeLinks = true 
            });

            var includeAttachments = GetParameter<bool>(nameof(GetWorkItemLink.IncludeAttachments));

            return wi.Relations.Where(l => includeAttachments || l.Rel != "AttachedFile");
        }
    }
}
