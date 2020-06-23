using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.History
{
    /// <summary>
    /// Gets the history of changes of a work item.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemHistory")]
    [OutputType(typeof(PSCustomObject))]
    public class GetWorkItemHistory : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Position=0, Mandatory=true, ValueFromPipeline=true)]
        //         [Alias("id")]
        //         [ValidateNotNull()]
        //         public object WorkItem { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void ProcessRecord()
        //     {
        //         wi = Get-TfsWorkItem -WorkItem WorkItem -Collection Collection
        //         latestRev = wi.Revisions.Count - 1

        //         0..latestRev | Foreach-Object {
        //             rev = wi.Revisions[_]

        //             [PSCustomObject] @{
        //                 Revision = _ + 1;
        //                 ChangedDate = rev.Fields["System.ChangedDate"].Value
        //                 ChangedBy = rev.Fields["System.ChangedBy"].Value
        //                 Changes = _GetChangedFields wi _
        //             }
        //         }
        //     }
        // }

        // Function _GetChangedFields([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem] wi, [int] rev)
        // {
        //     result = @{}

        //     wi.Revisions[rev].Fields | Where-Object IsChangedInRevision == true | Foreach-Object {
        //         result[_.ReferenceName] =  [PSCustomObject] @{
        //             NewValue = _.Value;
        //             OriginalValue = _.OriginalValue
        //         }
        //     }

        //     WriteObject(result); return;
        // }
    }
}
