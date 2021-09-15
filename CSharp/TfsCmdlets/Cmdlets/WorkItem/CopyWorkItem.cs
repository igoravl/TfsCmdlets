using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Creates a copy of a work item, optionally changing its type.
    /// </summary>
    /// <remarks>
    /// Use this cmdlet to create a copy of a work item (using its latest saved state/revision data) 
    /// that is of the specified work item type.
    /// <br/>
    /// By default, the copy retains the same type of the original work item, 
    /// unless the Type argument is specified
    /// </remarks>
    [Cmdlet(VerbsCommon.Copy, "TfsWorkItem")]
    [OutputType(typeof(WebApiWorkItem))]
    public class CopyWorkItem : CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_WORKITEM
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory=true, Position = 0)]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Specifies the type of the new work item. When omitted, the type of the original 
        /// work item is preserved.
        /// </summary>
        [Parameter()]
        public object NewType { get; set; }

        /// <summary>
        /// Creates a duplicate of all attachments present in the source work item and 
        /// adds them to the new work item.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeAttachments { get; set; }

        /// <summary>
        /// Creates a copy of all links present in the source work item and adds them to the new work item.
        /// Only the links are copied; linked artifacts themselves are not copied. 
        /// In other words, both the original and the copy work items point to the same linked
        /// artifacts.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeLinks { get; set; }

        /// <summary>
        /// Specifies the team project where the work item will be copied into. When omitted, 
		/// the copy will be created in the same team project of the source work item. 
        /// </summary>
        [Parameter()]
        public object DestinationProject { get; set; }

        /// <summary>
        /// Specifies the source team project from where the work item will be copied. 
        /// When omitted, it defaults to the team project of the piped work item (if any),
        /// or to the connection set by Connect-TfsTeamProject.
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
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

        // TODO

        ///// <summary>
        ///// Performs execution of the command
        ///// </summary>
        //protected override void DoProcessRecord()
        //{
        //    var wi = GetItem<WebApiWorkItem>();
        //    WebApiWorkItemType wit;

        //    Project ??= wi.Fields["System.TeamProject"];
        //    DestinationProject ??= Project;

        //    if(NewType != null)
        //    {
        //        wit = GetItem<WebApiWorkItemType>(new {Type = NewType});
        //    }
        //    else
        //    {
        //        wit = GetItem<WebApiWorkItemType>(new {WorkItem = wi});
        //    }

        //    var client = GetClient<WorkItemTrackingHttpClient>();

        //    // var flags = WorkItemCopyFlags.None;

        //    // if (IncludeAttachments)
        //    // {
        //    //     flags = flags -bor Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags.CopyFiles
        //    // }

        //    // if (IncludeLinks)
        //    // {
        //    //     flags = flags -bor Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags.CopyLinks
        //    // }

        //    // client.CreateWorkItemAsync()


        //    // copy = wi.Copy(witd, flags)

        //    // if(! SkipSave)
        //    // {
        //    //     copy.Save()
        //    // }

        //    // if (Passthru = = "Original")
        //    // {
        //    //     WriteObject(wi); return;
        //    // }

        //    // if(Passthru = = "Copy")
        //    // {
        //    //     WriteObject(copy); return;
        //    // }
        //}
    }
}
