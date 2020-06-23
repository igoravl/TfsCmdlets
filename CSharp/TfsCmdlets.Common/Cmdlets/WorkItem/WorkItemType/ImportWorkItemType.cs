using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    /// <summary>
    /// Imports a work item type definition into a team project.
    /// </summary>
    [Cmdlet(VerbsData.Import, "TfsWorkItemType", ConfirmImpact = ConfirmImpact.Medium)]
    public class ImportWorkItemType : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        
        //         [Parameter(Position=0, ValueFromPipeline=true)]
        //         [xml] 
        //         Xml,

        //         [Parameter()]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void ProcessRecord()
        //     {
        //         tp = Get-TfsTeamProject Project Collection
        //         tp.WorkItemTypes.Import(Xml.OuterXml)
        //     }
        // }
        
    }
}
