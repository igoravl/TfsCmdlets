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
using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Creates a new work item.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsWorkItem", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem))]
    public class NewWorkItem : CmdletBase
    {
        /// <summary>
        /// Specifies the type of the new work item.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public object Type { get; set; }

        /// <summary>
        /// Specifies the title of the work item.
        /// </summary>
        [Parameter()]
        public string Title { get; set; }

        /// <summary>
        /// Specifies the description of the work item.
        /// </summary>
        [Parameter()]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the area path of the work item.
        /// </summary>
        [Parameter()]
        public string Area { get; set; }

        /// <summary>
        /// Specifies the iteration path of the work item.
        /// </summary>
        [Parameter()]
        public string Iteration { get; set; }

        /// <summary>
        /// Specifies the user this work item is assigned to.
        /// </summary>
        [Parameter()]
        public object AssignedTo { get; set; }

        /// <summary>
        /// Specifies the state of the work item.
        /// </summary>
        [Parameter()]
        public string State { get; set; }

        /// <summary>
        /// Specifies the reason (field 'System.Reason') of the work item. 
        /// </summary>
        [Parameter()]
        public string Reason { get; set; }

        /// <summary>
        /// Specifies the Value Area of the work item. 
        /// </summary>
        [Parameter()]
        public string ValueArea { get; set; }

        /// <summary>
        /// Specifies the board column of the work item. 
        /// </summary>
        [Parameter()]
        public string BoardColumn { get; set; }

        /// <summary>
        /// Specifies whether the work item is in the sub-column Doing or Done in a board.
        /// </summary>
        [Parameter()]
        public bool BoardColumnDone { get; set; }

        /// <summary>
        /// Specifies the board lane of the work item
        /// </summary>
        [Parameter()]
        public string BoardLane { get; set; }

        /// <summary>
        /// Specifies the priority of the work item.
        /// </summary>
        [Parameter()]
        public int Priority { get; set; }

        /// <summary>
        /// Specifies the tags of the work item.
        /// </summary>
        [Parameter()]
        public string[] Tags { get; set; }

        /// <summary>
        /// Specifies the names and the corresponding values for the fields to be set 
        /// in the work item and whose values were not supplied in the other arguments 
        /// to this cmdlet.
        /// </summary>
        [Parameter()]
        public Hashtable Fields { get; set; }

        /// <summary>
        /// Bypasses any rule validation when saving the work item. Use it with caution, as this 
        /// may leave the work item in an invalid state.
        /// </summary>
        [Parameter()]
        public SwitchParameter BypassRules { get; set; }

        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter()]
        public object Team { get; set; }
    }

    // TODO

    //partial class WorkItemDataService
    //{
    //    protected override WebApiWorkItem DoNewItem()
    //    {
    //        var tp = Data.GetProject();
    //        var type = GetItem<WebApiWorkItemType>();

    //        if (type == null) throw new ArgumentException($"Invalid or non-existent work item type {type}");

    //        if (!PowerShell.ShouldProcess(tpc, $"Create new '{type}' work item")) return null;

    //        var fields = parameters.Get<Hashtable>(nameof(SetWorkItem.Fields), new Hashtable());
    //        var bypassRules = parameters.Get<bool>(nameof(SetWorkItem.BypassRules));
    //        var patch = new JsonPatchDocument();

    //        foreach (var argName in _parameterMap.Keys.Where(f => HasParameter(f) && !fields.ContainsKey(f)))
    //        {
    //            var value = parameters.Get<object>(argName);
    //            patch.Add(new JsonPatchOperation()
    //            {
    //                Operation = Operation.Add,
    //                Path = $"/fields/{_parameterMap[argName]}",
    //                Value = (value is IEnumerable<string> ? string.Join(";", (IEnumerable<string>)value) : value)
    //            });
    //        }

    //        var client = Data.GetClient<WorkItemTrackingHttpClient>(parameters);

    //        return client.CreateWorkItemAsync(patch, tp.Name, type.Name, false, bypassRules)
    //            .GetResult("Error creating work item");

    //        // TODO: Set board column

    //        // var boardColumn = parameters.Get<string>(nameof(SetWorkItem.BoardColumn));
    //        // var boardColumnDone = parameters.Get<bool>(nameof(SetWorkItem.BoardColumnDone));
    //        // var boardLane = parameters.Get<string>(nameof(SetWorkItem.BoardLane));
    //    }
    //}
}