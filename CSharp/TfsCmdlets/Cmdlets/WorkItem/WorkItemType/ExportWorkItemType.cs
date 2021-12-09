// using System;
// using System.Management.Automation;
// using System.Xml;
// using System.Xml.Linq;
// using System.Linq;
// using TfsCmdlets.Extensions;
// using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;

// namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
// {
//     /// <summary>
//     /// Exports an XML work item type definition from a team project.
//     /// </summary>
//     [Cmdlet(VerbsData.Export, "TfsWorkItemType", DefaultParameterSetName = "Export to output stream", SupportsShouldProcess = true)]
//     [OutputType(typeof(string))]
//     [DesktopOnly]
//     partial class ExportWorkItemType
//     {
//         /// <summary>
//         /// Specifies one or more work item types to export. Wildcards are supported. 
//         /// When omitted, all work item types in the given project are exported
//         /// </summary>
//         [Parameter(Position = 0, ParameterSetName = "Export to output stream")]
//         [Alias("Name")]
//         [SupportsWildcards()]
//         public string Type { get; set; } = "*";

//         /// <summary>
//         /// Exports the definitions of referenced global lists. 
//         /// When omitted, global list definitions are not included in the exported XML document.
//         /// </summary>
//         [Parameter]
//         public SwitchParameter IncludeGlobalLists { get; set; }

//         /// <summary>
//         /// Specifies the path to the folder where exported types are saved.
//         /// </summary>
//         [Parameter(ParameterSetName = "Export to file", Mandatory = true)]
//         public string Destination { get; set; }

//         /// <summary>
//         /// Allows the cmdlet to overwrite an existing file in the destination folder.
//         /// </summary>
//         /// <value></value>
//         [Parameter(ParameterSetName = "Export to file")]
//         public SwitchParameter Force { get; set; }

//         /// <summary>
//         /// Exports the saved query to the standard output stream as a string-encoded 
//         /// XML document.
//         /// </summary>
//         [Parameter(ParameterSetName = "Export to output stream", Mandatory = true)]
//         public SwitchParameter AsXml { get; set; }

//         /// <summary>
//         /// HELP_PARAM_PROJECT
//         /// </summary>
//         [Parameter(ValueFromPipeline = true)]
//         public object Project { get; set; }

//         /// <summary>
//         /// HELP_PARAM_COLLECTION
//         /// </summary>
//         [Parameter]
//         public object Collection { get; set; }

//         // TODO

//         //#if NET471_OR_GREATER
//         //        /// <inheritdoc/>
//         //        protected override void DoProcessRecord()
//         //        {
//         //            if (ParameterSetName.Equals("Export to file"))
//         //            {
//         //                throw new NotImplementedException("Export to file is not implemented");
//         //            }

//         //            var tp = Data.GetProject();
//         //            var types = Data.GetItems<WebApiWorkItemType>();

//         //#pragma warning disable CS0618
//         //            var store = tpc.GetService<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore>();
//         //#pragma warning restore CS0618

//         //            var project = store.Projects[tp.Name];

//         //            foreach (var t in types)
//         //            {
//         //                var type = project.WorkItemTypes[t.Name];
//         //                var xml = type.Export(IncludeGlobalLists).ToXDocument().ToString();

//         //                if (AsXml)
//         //                {
//         //                    WriteObject(xml);
//         //                    return;
//         //                }
//         //            }
//         //        }
//         //#endif
//     }
// }