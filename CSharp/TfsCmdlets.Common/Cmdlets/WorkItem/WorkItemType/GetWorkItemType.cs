using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    /// <summary>
    /// Gets one or more Work Item Type definitions from a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemType")]
    //[OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType))]
    public class GetWorkItemType : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Position=0)]
        //         [SupportsWildcards()]
        //         [Alias("Name")]
        //         public object Type = "*";

        //         [Parameter(ValueFromPipeline=true)]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void DoProcessRecord()
        //     {
        //         if (Type is Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType)
        //         {
        //             WriteObject(Type); return;
        //         }

        //         tp = this.GetProject();

        //         WriteObject(tp.WorkItemTypes | Where-Object Name -Like Type); return;
        //     }
        // }

    }
}