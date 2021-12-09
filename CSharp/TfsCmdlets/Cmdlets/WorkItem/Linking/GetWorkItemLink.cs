// using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Management.Automation;
// using TfsCmdlets.Services;
// using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;

// namespace TfsCmdlets.Cmdlets.WorkItem.Linking
// {
//     /// <summary>
//     /// Gets the links in a work item.
//     /// </summary>
//     [Cmdlet(VerbsCommon.Get, "TfsWorkItemLink")]
//     [OutputType(typeof(WorkItemRelation))]
//     partial class GetWorkItemLink
//     {
//         /// <summary>
//         /// HELP_PARAM_WORKITEM
//         /// </summary>
//         [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
//         [Alias("id")]
//         [ValidateNotNull()]
//         public object WorkItem { get; set; }

//         /// <summary>
//         /// Returns only the specified link types. When omitted, returns all link types.
//         /// </summary>
//         [Parameter]
//         public WorkItemLinkType LinkType { get; set; }

//         /// <summary>
//         /// Includes attachment information alongside links. When omitted, only links are retrieved.
//         /// </summary>
//         [Parameter]
//         public SwitchParameter IncludeAttachments { get; set; }
//     }

//     // TODO

//     //[Exports(typeof(WorkItemRelation))]
//     //internal partial class WorkItemLinkDataService : CollectionLevelController<WorkItemRelation>
//     //{
//     //    protected override IEnumerable<WorkItemRelation> Invoke()
//     //    {
//     //        var wi = this.GetItem<WebApiWorkItem>(new {
//     //            WorkItem = parameters.Get<object>(nameof(GetWorkItemLink.WorkItem)),
//     //            IncludeLinks = true
//     //        });

//     //        var linkType = parameters.Get<WorkItemLinkType>(nameof(GetWorkItemLink.LinkType));
//     //        var includeAll = (linkType == WorkItemLinkType.All);
//     //        var includeAttachments = parameters.Get<bool>(nameof(GetWorkItemLink.IncludeAttachments));

//     //        List<string> filteredTypes = LINK_TYPES.Where(kvp => includeAll || ((kvp.Key & linkType) == kvp.Key)).Select(kvp => kvp.Value).ToList();

//     //        if (includeAttachments)
//     //        {
//     //            filteredTypes.Add("AttachedFile");
//     //        }

//     //        return wi.Relations.Where(l => filteredTypes.Contains(l.Rel));
//     //    }

//     //    private static Dictionary<WorkItemLinkType, string> LINK_TYPES = new Dictionary<WorkItemLinkType, string>()
//     //    {
//     //        [WorkItemLinkType.Parent] = "System.LinkTypes.Hierarchy-Reverse",
//     //        [WorkItemLinkType.Child] = "System.LinkTypes.Hierarchy-Forward",
//     //        [WorkItemLinkType.Related] = "System.LinkTypes.Related",
//     //        [WorkItemLinkType.Predecessor] = "System.LinkTypes.Dependency-Reverse",
//     //        [WorkItemLinkType.Successor] = "System.LinkTypes.Dependency-Forward",
//     //        [WorkItemLinkType.Duplicate] = "System.LinkTypes.Duplicate-Forward",
//     //        [WorkItemLinkType.DuplicateOf] = "System.LinkTypes.Duplicate-Reverse",
//     //        [WorkItemLinkType.Tests] = "System.LinkTypes.TestedBy-Reverse",
//     //        [WorkItemLinkType.TestedBy] = "System.LinkTypes.TestedBy-Forward"
//     //    };
//     //}
// }
