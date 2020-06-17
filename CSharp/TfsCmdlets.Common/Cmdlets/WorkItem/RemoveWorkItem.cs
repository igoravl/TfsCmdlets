using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Deletes a work item from a team project collection.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsWorkItem", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public class RemoveWorkItem : BaseCmdlet
    {
        /// <summary>
        /// Specifies a work item. Valid values are the work item ID or an instance of
        /// Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void ProcessRecord()
        //     {
        //         ids = @()

        //         foreach(wi in WorkItem)
        //         {
        //             if (WorkItem is Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem)
        //             {
        //                 id = WorkItem.Id
        //             }
        //             elseif (WorkItem is int)
        //             {
        //                 id = WorkItem
        //             }
        //             else
        //             {
        //                 throw new Exception($"Invalid work item ""{WorkItem}"". Supply either a WorkItem object or one or more integer ID numbers")
        //             }

        //             if (ShouldProcess($"{{wi}.WorkItemType} id ("$(wi.Title)")", "Remove work item"))
        //             {
        //                 ids += id
        //             }
        //         }

        //         if (ids.Count -gt 0)
        //         {
        //             tpc = Get-TfsTeamProjectCollection Collection
        //             store = tpc.GetService([type] "Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore")

        //             errors = store.DestroyWorkItems([int[]] ids)

        //             if (errors && (errors.Count -gt 0))
        //             {
        //                 errors | Write-Error $"Error {{_}.Id}: $(_.Exception.Message)"

        //                 throw new Exception("Error destroying one or more work items")
        //             }
        //         }
        //     }
        // }
    }
}
