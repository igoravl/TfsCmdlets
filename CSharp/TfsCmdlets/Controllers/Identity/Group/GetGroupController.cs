// using TfsCmdlets.Cmdlets.Identity;
// using Microsoft.VisualStudio.Services.Graph.Client;

// namespace TfsCmdlets.Controllers.Identity
// {
//     [CmdletController(typeof(GraphGroup))]
//     partial class GetGroupController
//     {
//         public override IEnumerable<GraphGroup> Invoke()
//         {
//             var group = Parameters.Get<object>(nameof(GetGroup.Group));
//             var scope = Parameters.Get<GroupScope>(nameof(GetGroup.Scope));

//             var client = Data.GetClient<GraphHttpClient>();

//             PagedGraphGroups result = null;

//             switch (scope)
//             {
//                 case GroupScope.Server:
//                 case GroupScope.Collection:
//                     {
//                         foreach (var g in client.ListGroupsAsync(continuationToken: result?.ContinuationToken)
//                                             .GetResult("Error getting groups")
//                                             .Where(g => g.PrincipalName.StartsWith($"[{tp.Name}]\\", StringComparison.OrdinalIgnoreCase)))
//                         {
//                             yield return g;
//                         }
//                     }
//                 case GroupScope.Project:
//                     {
//                         var tp = Data.GetProject();

//                         do
//                         {
//                             foreach (var g in client.ListGroupsAsync(continuationToken: result?.ContinuationToken)
//                                                         .GetResult("Error getting groups")
//                                                         .Where(g => g.PrincipalName.StartsWith($"[{tp.Name}]\\", StringComparison.OrdinalIgnoreCase)))
//                             {
//                                 yield return g;
//                             }
//                         } while (result.ContinuationToken != null);
//                     }
//             }

//             Logger.LogError(new ArgumentException($"Invalid or non-existent group(s) {group}"));
//         }
//     }
// }