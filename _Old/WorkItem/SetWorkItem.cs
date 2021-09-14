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
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Sets the contents of one or more work items.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "TfsWorkItem", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem))]
    public class SetWorkItem : SetCmdletBase<WebApiWorkItem>
    {
        /// <summary>
        /// Specifies a work item. Valid values are the work item ID or an instance of
        /// Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem.
        /// </summary>
        [Parameter(ValueFromPipeline = true, Position = 0)]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

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

        // TODO: Implement type-changing logic
        // /// <summary>
        // /// Specifies the work item type of the work item.
        // /// </summary>
        // [Parameter()]
        // [Alias("Type")]
        // public string WorkItemType { get; set; }

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
        ///
        /// </summary>
        [Parameter()]
        public string Reason { get; set; }

        /// <summary>
        /// Specifies the Value Area of the work item. 
        ///
        /// </summary>
        [Parameter()]
        public string ValueArea { get; set; }

        /// <summary>
        /// Specifies the board column of the work item. 
        ///
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

    partial class WorkItemDataService
    {
        private static Dictionary<string, string> _parameterMap = new Dictionary<string, string>()
        {
            ["Area"] = "System.AreaPath",
            ["AssignedTo"] = "System.AssignedTo",
            ["Description"] = "System.Description",
            ["Iteration"] = "System.IterationPath",
            ["Priority"] = "Microsoft.VSTS.Common.Priority",
            ["Reason"] = "System.Reason",
            ["State"] = "System.State",
            ["Tags"] = "System.Tags",
            ["Title"] = "System.Title",
            ["ValueArea"] = "Microsoft.VSTS.Common.ValueArea"
        };

        protected override WebApiWorkItem DoSetItem()
        {
            var wi = GetItem<WebApiWorkItem>();
            var tpc = Collection;
            var fields = GetParameter<Hashtable>(nameof(SetWorkItem.Fields), new Hashtable());
            var bypassRules = GetParameter<bool>(nameof(SetWorkItem.BypassRules));
            var boardColumn = GetParameter<string>(nameof(SetWorkItem.BoardColumn));
            var boardColumnDone = GetParameter<bool>(nameof(SetWorkItem.BoardColumnDone));
            var boardLane = GetParameter<string>(nameof(SetWorkItem.BoardLane));

            foreach (var argName in _parameterMap.Keys.Where(f => HasParameter(f) && !fields.ContainsKey(f)))
            {
                fields.Add(_parameterMap[argName], GetFieldValue(argName, (string)wi.Fields["System.TeamProject"]));
            }

            if (fields.Count > 0)
            {
                var patch = new JsonPatchDocument(){
                    new JsonPatchOperation() {
                        Operation = Operation.Test,
                        Path = "/rev",
                        Value = wi.Rev
                    }
                };

                foreach (DictionaryEntry field in fields)
                {
                    patch.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = $"/fields/{field.Key}",
                        Value = field.Value is IEnumerable<string> ?
                            string.Join(";", (IEnumerable<string>)field.Value) :
                            field.Value
                    });
                }

                var client = GetClient<WorkItemTrackingHttpClient>();

                wi = client.UpdateWorkItemAsync(patch, (int)wi.Id, false, bypassRules)
                    .GetResult("Error updating work item");

            }

            // Change board status

            if (HasParameter(nameof(SetWorkItem.BoardColumn)) ||
                HasParameter(nameof(SetWorkItem.BoardColumnDone)) ||
                HasParameter(nameof(SetWorkItem.BoardLane)))
            {

                var patch = new JsonPatchDocument(){
                    new JsonPatchOperation() {
                        Operation = Operation.Test,
                        Path = "/rev",
                        Value = wi.Rev
                    }
                };

                var (_, tp, t) = GetCollectionProjectAndTeam();
                var board = FindBoard((string)wi.Fields["System.WorkItemType"], tpc, tp, t);

                if (HasParameter(nameof(SetWorkItem.BoardColumn)))
                {
                    patch.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = $"/fields/{board.Fields.ColumnField.ReferenceName}",
                        Value = GetParameter<string>(nameof(SetWorkItem.BoardColumn))
                    });
                }

                if (HasParameter(nameof(SetWorkItem.BoardLane)))
                {
                    patch.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = $"/fields/{board.Fields.RowField.ReferenceName}",
                        Value = GetParameter<string>(nameof(SetWorkItem.BoardLane))
                    });
                }

                if (HasParameter(nameof(SetWorkItem.BoardColumnDone)))
                {
                    patch.Add(new JsonPatchOperation()
                    {
                        Operation = Operation.Add,
                        Path = $"/fields/{board.Fields.DoneField.ReferenceName}",
                        Value = GetParameter<bool>(nameof(SetWorkItem.BoardColumnDone))
                    });
                }

                var client = GetClient<WorkItemTrackingHttpClient>();

                wi = client.UpdateWorkItemAsync(patch, (int)wi.Id, false, bypassRules)
                    .GetResult("Error updating work item");
            }

            return wi;
        }

        private WebApiBoard FindBoard(string workItemType, Models.Connection tpc, WebApiTeamProject tp, WebApiTeam team)
        {
            var boards = GetItems<WebApiBoard>(new
            {
                Board = "*",
                Team = team,
                Project = tp,
                Collection = tpc
            }).ToList();

            foreach (WebApiBoard b in boards)
            {
                var keys = b.AllowedMappings.Values.SelectMany(o => o.Keys).ToList();

                if (keys.Any(t => t.Equals(workItemType, StringComparison.OrdinalIgnoreCase)))
                {
                    return b;
                }
            }

            throw new Exception("Unable to find a board belonging to team " +
                $"'{team.Name}' that contains a mapping to the work item type '{workItemType}'");
        }

        private object GetFieldValue(string argName, string projectName)
        {
            object value = GetParameter<object>(argName);

            if (string.Equals(argName, "Area", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(argName, "Iteration", StringComparison.OrdinalIgnoreCase))
            {
                return NodeUtil.NormalizeNodePath((string)value, projectName, includeTeamProject: true, includeLeadingSeparator: true);
            }

            return value;
        }
    }
}