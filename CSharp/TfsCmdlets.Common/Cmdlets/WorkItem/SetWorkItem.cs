using System.Collections;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Sets the contents of one or more work items.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "TfsWorkItem", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem))]
    public class SetWorkItem : BaseCmdlet
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
        /// Specifies the names and the corresponding values for the fields to
        /// be set in the work item.
        /// </summary>
        [Parameter(Position = 1, ParameterSetName = "Set work item")]
        public Hashtable Fields { get; set; }

        [Parameter(ParameterSetName = "Set work item")]
        public SwitchParameter BypassRules { get; set; }

        [Parameter(ParameterSetName = "Set work item")]
        public SwitchParameter SkipSave { get; set; }

        [Parameter(ParameterSetName = "Set board status")]
        public object Board { get; set; }

        [Parameter(ParameterSetName = "Set board status")]
        public object Column { get; set; }

        [Parameter(ParameterSetName = "Set board status")]
        public object Lane { get; set; }

        [Parameter(ParameterSetName = "Set board status")]
        [ValidateSet("Doing", "Done")]
        public string ColumnStage { get; set; }

        [Parameter(ParameterSetName = "Set board status")]
        public object Team { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        /// <value></value>
        [Parameter()]
        public object Collection { get; set; }
    }

    partial class WorkItemDataService
    {

    }
    // /// <summary>
    // /// Performs execution of the command
    // /// </summary>
    // protected override void ProcessRecord()
    //     {
    //         if (WorkItem is Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem)
    //         {
    //             tpc = WorkItem.Store.TeamProjectCollection
    //             id = WorkItem.Id
    //         }
    //         else
    //         {
    //             tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
    //             id = (Get-TfsWorkItem -WorkItem WorkItem -Collection Collection).Id
    //         }

    //         if (BypassRules)
    //         {
    //             store = new Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore(tpc, Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStoreFlags.BypassRules)
    //         }
    //         else
    //         {
    //             store = tpc.GetService([type]"Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore")
    //         }

    //         wi = store.GetWorkItem(id)

    //         Fields = _FixAreaIterationValues -Fields Fields -ProjectName wi.Project.Name

    //         if(ShouldProcess($"Set work item fields {{Fields}.Keys -join ", "} to $(Fields.Values -join ", "), respectively"))
    //         {
    //             foreach(fldName in Fields.Keys)
    //             {
    //                 wi.Fields[fldName].Value = Fields[fldName]
    //             }

    //             if(! SkipSave)
    //             {
    //                 wi.Save()
    //             }
    //         }

    //         WriteObject(wi); return;
    //     }
    // }

    /////////////// BOARD STATUS  //////////////////


    // protected override void ProcessRecord()
    //     {
    //         if ((! Column) && (! ColumnStage) && (! Lane))
    //         {
    //             throw new Exception("Supply a value to at least one of the following arguments: Column, ColumnStage, Lane")
    //         }

    //         if (WorkItem is Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem)
    //         {
    //             tp = WorkItem.Project
    //             tpc = WorkItem.Store.TeamProjectCollection
    //         }
    //         else
    //         {
    //             tp = this.GetProject();
    //             tpc = tp.Store.TeamProjectCollection
    //             WorkItem = Get-TfsWorkItem -WorkItem WorkItem -Collection Collection
    //         }

    //         t = Get-TfsTeam -Team Team -Project tp -Collection tpc
    //         id = [int] WorkItem.Id
    //         rev = WorkItem.Revision

    //         # Get the Kanban board column/lane field info

    //         b = Get-TfsBoard -Board Board -Team t -Project tp -Collection tpc

    //         if (! b)
    //         {
    //             throw new Exception($"Invalid or non-existent board "{Board}" in team "Team"")
    //         }

    //         processMessages = @()

    //         ops = @(
    //             @{
    //                 Operation = "Test";
    //                 Path = "/rev";
    //                 Value = rev.ToString()
    //             }

    //         if (Column)
    //         {
    //             ops += @{
    //                 Operation = "Add";
    //                 Path = $"/fields/{{b}.Fields.ColumnField.ReferenceName}";
    //                 Value = Column
    //             }

    //             processMessages += $"Board Column="{Column}""
    //         }

    //         if (Lane)
    //         {
    //             ops += @{
    //                 Operation = "Add";
    //                 Path = $"/fields/{{b}.Fields.RowField.ReferenceName}";
    //                 Value = Lane
    //             }

    //             processMessages += $"Board Lane="{Lane}""
    //         }

    //         if (ColumnStage)
    //         {
    //             ops += @{
    //                 Operation = "Add";
    //                 Path = $"/fields/{{b}.Fields.DoneField.ReferenceName}";
    //                 Value = (ColumnStage = = "Done") 
    //             }

    //             processMessages += $"Board Stage (Doing/Done)="{ColumnStage}""
    //         }

    //         if (ShouldProcess($"{{WorkItem}.WorkItemType} id ("$(WorkItem.Title)")", "Set work item board status: $(processMessages -join ", ")"))
    //         {
    //             patch = _GetJsonPatchDocument ops
    //             var client = GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();
    //             wi = client.UpdateWorkItemAsync(patch, id).Result
    //             WriteObject(wi); return;
    //         }
    //     }
    // }

}