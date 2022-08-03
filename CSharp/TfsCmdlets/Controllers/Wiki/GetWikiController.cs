using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Cmdlets.Wiki;

namespace TfsCmdlets.Controllers.Wiki
{
    [CmdletController(typeof(WikiV2))]
    partial class GetWikiController
    {
       protected override IEnumerable Run()
       {
           var client = GetClient<WikiHttpClient>();

           foreach(var input in Wiki)
           {
               var wiki = input switch {
                   string s when s.IsGuid() => new Guid(s),
                   _ => input
               };

               switch (wiki)
               {
                   case WikiV2 w:
                       {
                           yield return w;
                           break;
                       }
                   case null when ProjectWiki:
                   case {} when ProjectWiki:
                       {
                           foreach (var w in client.GetAllWikisAsync(Project.Name)
                                                .GetResult($"Error getting project wiki")
                                                .Where(r => r.Type == WikiType.ProjectWiki))
                           {
                               yield return w;
                           }
                           break;
                       }
                   case Guid guid:
                       {
                           yield return client.GetWikiAsync(Project.Name, guid)
                                            .GetResult($"Error getting Wiki with ID {guid}");
                           break;
                       }
                   case string s when !s.IsWildcard():
                       {
                           yield return client.GetWikiAsync(Project.Name, s)
                                            .GetResult($"Error getting Wiki '{s}'");
                           break;
                       }
                   case string s:
                       {
                           foreach (var w in client.GetAllWikisAsync(Project.Name)
                                                .GetResult($"Error getting wiki(s) '{s}'")
                                                .Where(r => r.Name.IsLike(s)))
                           {
                               yield return w;
                           }
                           break;
                       }
                   default:
                       {
                           Logger.LogError(new ArgumentException(nameof(GetWiki.Wiki)));
                           break;
                       }
               }
           }
       }
    }
}