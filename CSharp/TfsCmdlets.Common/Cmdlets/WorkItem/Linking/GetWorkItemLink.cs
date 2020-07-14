using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Gets the links in a work item.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemLink")]
    // [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.Client.Link))]
    public class GetWorkItemLink: CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();

//         [Parameter(Position=0, Mandatory=true, ValueFromPipeline=true)]
//         [Alias("id")]
//         [ValidateNotNull()]
//         public object WorkItem { get; set; }

//         [Parameter()]
//         public object Collection { get; set; }

//         /// <summary>
//         /// Performs execution of the command
//         /// </summary>
//         protected override void DoProcessRecord()
//     {
//         wi = Get-TfsWorkItem -WorkItem WorkItem -Collection Collection

//         if (wi)
//         {
//             WriteObject(wi.Links); return;
//         }
//     }
// }
    }
}
