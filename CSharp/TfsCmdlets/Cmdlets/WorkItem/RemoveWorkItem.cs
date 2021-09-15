using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Extensions;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using WebApiBoard = Microsoft.TeamFoundation.Work.WebApi.Board;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Deletes a work item from a team project collection.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsWorkItem", SupportsShouldProcess = true)]
    public class RemoveWorkItem : CmdletBase
    {
        /// <summary>
        /// Specifies the work item to remove.
        /// </summary>
        /// <seealso cref="Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem">
        /// A WorkItem object
        /// </seealso>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Permanently deletes the work item, without sending it to the recycle bin.
        /// </summary>
        [Parameter()]
        public SwitchParameter Destroy { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }
    }

    // TODO

    //partial class WorkItemDataService
    //{
    //    protected override void DoRemoveItem(ParameterDictionary parameters)
    //    {
    //        var wis = GetItems<WebApiWorkItem>();
    //        var (tpc, tp) = GetCollectionAndProject();
    //        var destroy = parameters.Get<bool>(nameof(RemoveWorkItem.Destroy));
    //        var force = parameters.Get<bool>(nameof(RemoveWorkItem.Force));
    //        var client = GetClient<WorkItemTrackingHttpClient>();

    //        foreach(var wi in wis)
    //        {
    //            if(!PowerShell.ShouldProcess(tpc, $"{(destroy? "Destroy": "Delete")} work item {wi.Id}")) continue;

    //            if(destroy && !(force || ShouldContinue("Are you sure you want to destroy work item {wi.id}?"))) continue;

    //            client.DeleteWorkItemAsync(tp.Name, (int) wi.Id, destroy)
    //                .GetResult($"Error {(destroy? "destroying": "deleting")} work item {wi.Id}");
    //        }
    //    }
    //}
}