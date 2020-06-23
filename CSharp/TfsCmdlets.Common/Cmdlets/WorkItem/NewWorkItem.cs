using System.Collections;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Creates a new work item in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsWorkItem", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem))]
    public class NewWorkItem : CmdletBase
    {
        /// <summary>
        /// Specifies the type of the work item.
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 0)]
        public object Type { get; set; }

        /// <summary>
        /// Specifies the title of the work item.
        /// </summary>
        [Parameter(Position = 1)]
        public string Title { get; set; }

        /// <summary>
        /// Specifies the names and the corresponding values for the fields to
        /// be set in the work item.
        /// </summary>
        [Parameter()]
        public Hashtable Fields { get; set; }

        [Parameter()]
        public SwitchParameter SkipSave { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void DoProcessRecord()
        //     {
        //         if(ShouldProcess(Type, "Create work item of specified type"))
        //         {
        //             wit = Get-TfsWorkItemType -Type Type -Project Project -Collection Collection

        //             wi = wit.NewWorkItem()

        //             if (Title)
        //             {
        //                 wi.Title = Title
        //             }

        //             foreach(field in Fields)
        //             {
        //                 wi.Fields[field.Key] = field.Value
        //             }

        //             if (! SkipSave.IsPresent)
        //             {
        //                 wi.Save()
        //             }

        //             if (Passthru)
        //             {
        //                 WriteObject(wi); return;
        //             }
        //         }
        //     }
        // }
    }
}
