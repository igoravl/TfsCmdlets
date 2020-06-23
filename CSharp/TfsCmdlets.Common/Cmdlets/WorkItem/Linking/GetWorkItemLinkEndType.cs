using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Gets the work item link end types of a team project collection.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemLinkEndType")]
    // [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemLinkTypeEnd))]
    public class GetWorkItemLinkEndType : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Position=0, ValueFromPipeline=true)]
        //         public object Collection { get; set; }

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void ProcessRecord()
        //     {
        //         tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})

        //         WriteObject(tpc.WorkItemStore.WorkItemLinkTypes.LinkTypeEnds); return;
        //     }
        // }

    }
}
