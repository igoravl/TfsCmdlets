using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Work
{
    [Cmdlet(VerbsCommon.Set, "TfsWorkItemBoardStatus", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    //[OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItem))]
    public class SetWorkItemBoardStatus : BaseCmdlet
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(ValueFromPipeline=true, Position=0)]
        //         [Alias("id")]
        //         [ValidateNotNull()]
        //         public object WorkItem { get; set; }

        //         [Parameter()]
        //         public object Board { get; set; }

        //         [Parameter()]
        //         public object Column { get; set; }

        //         [Parameter()]
        //         public object Lane { get; set; }

        //         [Parameter()]
        //         [ValidateSet("Doing", "Done")]
        //         public string ColumnStage { get; set; }

        //         [Parameter()]
        //         public object Team { get; set; }

        //         [Parameter()]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //     protected override void BeginProcessing()
        //     {
        //         #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.Client"
        //         #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.WebApi"
        //     }

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
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
}
