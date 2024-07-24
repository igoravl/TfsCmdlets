// using System.Management.Automation;

// namespace TfsCmdlets.Cmdlets.WorkItem
// {
//     /// <summary>
//     /// Creates a copy of a work item, optionally changing its type.
//     /// </summary>
//     /// <remarks>
//     /// Use this cmdlet to create a copy of a work item (using its latest saved state/revision data) 
//     /// that is of the specified work item type.
//     /// <br/>
//     /// By default, the copy retains the same type of the original work item, 
//     /// unless the Type argument is specified
//     /// </remarks>
//     [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WebApiWorkItem))]
//     partial class CopyWorkItem
//     {
//         /// <summary>
//         /// HELP_PARAM_WORKITEM
//         /// </summary>
//         [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 0)]
//         [Alias("id")]
//         [ValidateNotNull()]
//         public object WorkItem { get; set; }

//         /// <summary>
//         /// Specifies the type of the new work item. When omitted, the type of the original 
//         /// work item is preserved.
//         /// </summary>
//         [Parameter]
//         public object NewType { get; set; }

//         /// <summary>
//         /// Creates a duplicate of all attachments present in the source work item and 
//         /// adds them to the new work item.
//         /// </summary>
//         [Parameter]
//         public SwitchParameter IncludeAttachments { get; set; }

//         /// <summary>
//         /// Creates a copy of all links present in the source work item and adds them to the new work item.
//         /// Only the links are copied; linked artifacts themselves are not copied. 
//         /// In other words, both the original and the copy work items point to the same linked
//         /// artifacts.
//         /// </summary>
//         [Parameter]
//         public SwitchParameter IncludeLinks { get; set; }

//         /// <summary>
//         /// Specifies the team project where the work item will be copied into. When omitted, 
// 		/// the copy will be created in the same team project of the source work item. 
//         /// </summary>
//         [Parameter]
//         public object DestinationProject { get; set; }

//         /// <summary>
//         /// Specifies the source team project from where the work item will be copied. 
//         /// When omitted, it defaults to the team project of the piped work item (if any),
//         /// or to the connection set by Connect-TfsTeamProject.
//         /// </summary>
//         [Parameter]
//         public object Project { get; set; }

//         /// <summary>
//         /// Returns the results of the command. It takes one of the following values: 
//         /// Original (returns the original work item), Copy (returns the newly created work item copy) 
//         /// or None.
//         /// </summary>
//         [Parameter]
//         [ValidateSet("Original", "Copy", "None")]
//         public string Passthru { get; set; } = "Copy";
//     }
//
//     [CmdletController(typeof(WebApiWorkItem))]
//     partial class CopyWorkItemController
//     {
//         protected override IEnumerable Run()
//         {
//             var wi = Data.GetItem<WebApiWorkItem>();
//             var tp = Data.GetProject(new { Project = (string)wi.Fields["System.TeamProject"] });
//             var destinationProject = Data.GetProject(new { Project = Parameters.Get<string>("DestinationProject", tp.Name) });
//             var wit = Data.GetItem<WebApiWorkItemType>(new { Type = Parameters.Get<object>("NewType", wi.Fields["System.WorkItemType"]) });
//             var fields = Parameters.Get<Hashtable>("Fields");

//             if (!PowerShell.ShouldProcess($"Work item #{wi.Id} (\"{wi.Fields["System.Title"]}\")", "Create a copy of work item")) return null;

//             var patch = new JsonPatchDocument();

//             foreach (var argName in Parameters.Keys.Where(f => Parameters.HasParameter(f) && !fields.ContainsKey(f)))
//             {
//                 var value = Parameters.Get<object>(argName);

//                 patch.Add(new JsonPatchOperation()
//                 {
//                     Operation = Operation.Add,
//                     Path = $"/fields/{Parameters.Get<object>(argName)}",
//                     Value = (value is IEnumerable<string> ? string.Join(";", (IEnumerable<string>)value) : value)
//                 });
//             }

//             // var client = Data.GetClient<WorkItemTrackingHttpClient>();

//             // return client.CreateWorkItemAsync(patch, tp.Name, type.Name, false, bypassRules)
//             //     .GetResult("Error creating work item");

//             // var flags = WorkItemCopyFlags.None;

//             // if (IncludeAttachments)
//             // {
//             //     flags = flags - bor Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags.CopyFiles
//             // }

//             // if (IncludeLinks)
//             // {
//             //     flags = flags - bor Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags.CopyLinks
//             // }

//             // client.CreateWorkItemAsync();

//             // var copy = wi.Copy(witd, flags);

//             // if (!SkipSave) copy.Save();

//             // yield return (passthru.Equals("Original"))? wi: copy;

//             throw new NotImplementedException();
//         }
//     }
// }