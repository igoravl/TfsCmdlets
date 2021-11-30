using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Adds a link between two work items.
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "TfsWorkItemLink")]
    public class AddWorkItemLink : CmdletBase
    {
        // TODO

        //         [Parameter(Position=0, Mandatory=true, ValueFromPipeline=true)]
        //         [Alias("Id")]
        //         [Alias("From")]
        //         [ValidateNotNull()]
        //         public object SourceWorkItem { get; set; }

        //         [Parameter(Position=1, Mandatory=true)]
        //         [Alias("To")]
        //         [ValidateNotNull()]
        //         public object TargetWorkItem { get; set; }

        //         [Parameter(Position=2, Mandatory=true)]
        //         [Alias("LinkType")]
        //         [Alias("Type")]
        //         public object EndLinkType { get; set; }

        //         [Parameter()]
        //         public string Comment { get; set; }

        //         public SwitchParameter SkipSave { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void DoProcessRecord()
        //     {
        //         sourceWi = Get-TfsWorkItem -WorkItem SourceWorkItem -Collection Collection -Project Project
        //         targetWi = Get-TfsWorkItem -WorkItem TargetWorkItem -Collection Collection -Project Project


        //         if (EndLinkType -isnot [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemLinkTypeEnd])
        //         {
        //             try
        //             {
        //                 EndLinkType = sourceWi.Store.WorkItemLinkTypes.LinkTypeEnds[EndLinkType]
        //             }
        //             catch
        //             {
        //                 throw new Exception($"Error retrieving work item link type {EndLinkType}`: _")
        //             }
        //         }

        //         link = new Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemLink(EndLinkType, targetWi.Id)
        //         link.Comment = Comment

        //         i = sourceWi.WorkItemLinks.Add(link)

        //         if (! SkipSave)
        //         {
        //             sourceWi.Save()
        //         }

        //         WriteObject(sourceWi.WorkItemLinks[i]        ); return;
        //     }
        // }
    }
}
