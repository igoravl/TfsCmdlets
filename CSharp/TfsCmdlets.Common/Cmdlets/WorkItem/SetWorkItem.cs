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
        [Parameter(Position = 1)]
        public Hashtable Fields { get; set; }

        [Parameter()]
        public SwitchParameter BypassRules { get; set; }

        [Parameter()]
        public SwitchParameter SkipSave { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        /// <value></value>
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
    }
}
