// using System;
// using System.Collections.Generic;
// using System.Management.Automation;
// using Microsoft.TeamFoundation.Core.WebApi;
// using TfsCmdlets.Extensions;
// using TfsCmdlets.Services;

// namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
// {
//     /// <summary>
//     /// Gets one or more work item tags.
//     /// </summary>
//     [Cmdlet(VerbsCommon.Get, "TfsWorkItemTag")]
//     [OutputType(typeof(WebApiTagDefinition))]
//     partial class GetWorkItemTag
//     {
//         /// <summary>
//         /// Specifies one or more tags to returns. Wildcards are supported. 
//         /// When omitted, returns all existing tags in the given project.
//         /// </summary>
//         [Parameter(Position = 0)]
//         [SupportsWildcards()]
//         [Alias("Name")]
//         public object Tag { get; set; } = "*";

//         /// <summary>
//         /// Includes tags not associated to any work items.
//         /// </summary>
//         [Parameter]
//         public SwitchParameter IncludeInactive { get; set; }

//         /// <summary>
//         /// HELP_PARAM_PROJECT
//         /// </summary>
//         /// <value></value>
//         [Parameter(ValueFromPipeline = true)]
//         public object Project { get; set; }
//     }

//     // TODO

//     //[Exports(typeof(WebApiTagDefinition))]
//     //internal partial class WorkItemTagDataService : CollectionLevelController<WebApiTagDefinition>
//     //{
//     //    protected override IEnumerable<WebApiTagDefinition> Invoke()
//     //    {
//     //        var tag = parameters.Get<object>(nameof(GetWorkItemTag.Tag));
//     //        var includeInactive = parameters.Get<bool>(nameof(GetWorkItemTag.IncludeInactive));

//     //        while (true) switch (tag)
//     //            {
//     //                case WebApiTagDefinition t:
//     //                    {
//     //                        yield return t;
//     //                        yield break;
//     //                    }
//     //                case string s:
//     //                    {
//     //                        tag = new[] { s };
//     //                        continue;
//     //                    }
//     //                case IEnumerable<string> tags:
//     //                    {
//     //                        var tp = Data.GetProject();
//     //                        var client = Data.GetClient<Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient>();

//     //                        var result = client.GetTagsAsync(tp.Id, includeInactive)
//     //                            .GetResult($"Error getting work item tag(s) '{string.Join(", ", tags)}'");

//     //                        foreach (var r in result)
//     //                        {
//     //                            foreach (var t in tags)
//     //                            {
//     //                                if (r.Name.IsLike(t)) yield return r;
//     //                            }
//     //                        }

//     //                        yield break;
//     //                    }
//     //                default:
//     //                    {
//     //                        throw new ArgumentException($"Invalid or non-existent tag '{tag}'");
//     //                    }
//     //            }
//     //    }
//     //}
// }