// using System.Management.Automation;
// using Microsoft.TeamFoundation.Core.WebApi;
// using TfsCmdlets.Extensions;

// namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
// {
//     /// <summary>
//     /// Creates a new work item tag.
//     /// </summary>
//     [Cmdlet(VerbsCommon.New, "TfsWorkItemTag", SupportsShouldProcess = true)]
//     [OutputType(typeof(WebApiTagDefinition))]
//     partial class NewWorkItemTag
//     {
//         /// <summary>
//         /// Specifies the name of the new tag.
//         /// </summary>
//         /// <value></value>
//         [Parameter(Position = 0, ValueFromPipeline = true)]
//         [Alias("Name")]
//         public string Tag { get; set; }
//     }

//     // TODO

//     //partial class WorkItemTagDataService
//     //{
//     //    protected override WebApiTagDefinition DoNewItem()
//     //    {
//     //        var tag = parameters.Get<string>(nameof(NewWorkItemTag.Tag));
//     //        var tp = Data.GetProject();

//     //        if (!PowerShell.ShouldProcess(tp, $"Create work item tag '{tag}'")) return null;

//     //        var client = Data.GetClient<TaggingHttpClient>();

//     //        return client.CreateTagAsync(tp.Id, tag)
//     //            .GetResult($"Error creating work item tag '{tag}'");
//     //    }
//     //}
// }