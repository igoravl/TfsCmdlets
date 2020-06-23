using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Creates a copy of a work item, optionally changing its type.
    /// </summary>
    /// <remarks>
    /// Use this cmdlet to create a copy of a work item (using its latest saved state/revision data) 
    /// that is of the specified work item type. By default, the copy retains the same type of the 
    /// original work item, unless the Type argument is specified
    /// </remarks>
    [Cmdlet(VerbsCommon.Copy, "TfsWorkItem")]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem))]
    public class CopyWorkItem : CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_WORKITEM
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        [Parameter()]
        public object Type { get; set; }

        [Parameter()]
        public SwitchParameter IncludeAttachments { get; set; }

        [Parameter()]
        public SwitchParameter IncludeLinks { get; set; }

        [Parameter()]
        public SwitchParameter SkipSave { get; set; }

        /// <summary>
        /// Specified the team project where the work item will be copied into. When omitted, 
		/// the copy will be created in the same team project of the source work item. 
		/// The value provided to this argument takes precedence over both the source team project 
		/// and the team project of an instance of WorkItemType provided to the Type argument.
        /// </summary>
        [Parameter()]
        public object DestinationProject { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        /// <value></value>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Returns the results of the command. It takes one of the following values: 
        /// Original (returns the original work item), Copy (returns the newly created work item copy) 
        /// or None.
        /// </summary>
        [Parameter()]
        [ValidateSet("Original", "Copy", "None")]
        public string Passthru { get; set; } = "Copy";

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void ProcessRecord()
        //     {
        //         wi = Get-TfsWorkItem -WorkItem WorkItem -Collection Collection
        //         #store = wi.Store

        //         if(Type)
        //         {
        //             if (Project)
        //             {
        //                 tp = Project
        //             }
        //             else
        //             {
        //                 tp = wi.Project
        //             }
        //             witd = Get-TfsWorkItemType -Type Type -Project tp -Collection wi.Store.TeamProjectCollection
        //         }
        //         else
        //         {
        //             witd = wi.Type
        //         }

        //         flags = Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags.None

        //         if (IncludeAttachments)
        //         {
        //             flags = flags -bor Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags.CopyFiles
        //         }

        //         if (IncludeLinks)
        //         {
        //             flags = flags -bor Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags.CopyLinks
        //         }

        //         copy = wi.Copy(witd, flags)

        //         if(! SkipSave)
        //         {
        //             copy.Save()
        //         }

        //         if (Passthru = = "Original")
        //         {
        //             WriteObject(wi); return;
        //         }

        //         if(Passthru = = "Copy")
        //         {
        //             WriteObject(copy); return;
        //         }
        //     }
        // }
    }
}
