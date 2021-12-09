// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Management.Automation;
// using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
// using TfsCmdlets.Extensions;
// using TfsCmdlets.Services;
// using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
// using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;

// namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
// {
//     /// <summary>
//     /// Gets one or more Work Item Type definitions from a team project.
//     /// </summary>
//     [Cmdlet(VerbsCommon.Get, "TfsWorkItemType", DefaultParameterSetName = "Get by type")]
//     [OutputType(typeof(WebApiWorkItemType))]
//     partial class GetWorkItemType
//     {
//         /// <summary>
//         /// Specifies one or more work item type names to return. Wildcards are supported. 
//         /// When omitted, returns all work item types in the given team project.
//         /// </summary>
//         [Parameter(Position = 0, ParameterSetName = "Get by type")]
//         [SupportsWildcards()]
//         [Alias("Name")]
//         public object Type { get; set; } = "*";

//         /// <summary>
//         /// Speficies a work item whose corresponding type should be returned.
//         /// </summary>
//         [Parameter(ParameterSetName = "Get by work item", Mandatory = true)]
//         public object WorkItem { get; set; }

//         /// <summary>
//         /// HELP_PARAM_PROJECT
//         /// </summary>
//         [Parameter(ValueFromPipeline = true)]
//         public object Project { get; set; }
//     }

//     // TODO

//     //[Exports(typeof(WebApiWorkItemType))]
//     //internal partial class WorkItemTypeDataService : CollectionLevelController<WebApiWorkItemType>
//     //{
//     //    protected override IEnumerable<WebApiWorkItemType> Invoke()
//     //    {
//     //        var type = parameters.Get<object>(nameof(GetWorkItemType.Type));

//     //        if (HasParameter(nameof(GetWorkItemType.WorkItem)))
//     //        {
//     //            var workItem = Data.GetItem<WebApiWorkItem>(nameof(GetWorkItemType.WorkItem));
//     //            type = workItem.Fields["System.WorkItemType"];
//     //        }

//     //        while (true) switch (type)
//     //            {
//     //                case WebApiWorkItemType t:
//     //                    {
//     //                        yield return t;
//     //                        yield break;
//     //                    }
//     //                case string t:
//     //                    {
//     //                        type = new[] { t };
//     //                        continue;
//     //                    }
//     //                case IEnumerable<string> types:
//     //                    {
//     //                        var client = Data.GetClient<WorkItemTrackingHttpClient>();
//     //                        var tp = Data.GetProject();

//     //                        foreach (var t in types)
//     //                        {
//     //                            foreach (var result in client.GetWorkItemTypesAsync(tp.Id)
//     //                                .GetResult($"Error getting type(s) '{string.Join(", ", types)}'")
//     //                                .Where(t1 => t1.Name.IsLike(t)))
//     //                            {
//     //                                yield return result;
//     //                            }
//     //                        }
//     //                        yield break;
//     //                    }
//     //                default:
//     //                    {
//     //                        throw new ArgumentException($"Invalid or non-existent work item type '{type}'");
//     //                    }
//     //            }
//     //    }
//     //}
// }