using System.Management.Automation;
using System.Xml;

namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    /// <summary>
    /// Exports a work item type definition from a team project.
    /// </summary>
    [Cmdlet(VerbsData.Export, "TfsWorkItemType")]
    [OutputType(typeof(XmlDocument))]
    public class ExportWorkItemType : CmdletBase
    {

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter()]
        //         [Alias("Name")]
        //         [SupportsWildcards()]
        //         public string WorkItemType = "*";

        //         /// <summary>
        //         /// Exports the definitions of referenced global lists. If not specified, global list definitions are omitted.
        //         /// </summary>
        //         [Parameter()]
        //         public SwitchParameter IncludeGlobalLists { get; set; }

        //         [Parameter(ValueFromPipeline=true)]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void DoProcessRecord()
        //     {
        //         types = Get-TfsWorkItemType -Name WorkItemType -Project Project -Collection Collection

        //         foreach(type in types)
        //         {
        //             type.Export(IncludeGlobalLists)
        //         }
        //     }
        // }
    }
}