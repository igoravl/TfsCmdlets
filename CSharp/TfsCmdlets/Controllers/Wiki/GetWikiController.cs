using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Cmdlets.Wiki;

namespace TfsCmdlets.Controllers.Wiki
{
    [CmdletController(typeof(WikiV2))]
    partial class GetWikiController
    {
       protected override IEnumerable Run()
       {
           var tp = Data.GetProject();
           var wiki = Parameters.Get<object>("Wiki");
           var projectWiki = Parameters.Get<bool>("ProjectWiki");

           while (true)
           {
               switch (wiki)
               {
                   case null when projectWiki:
                   case string s when string.IsNullOrEmpty(s) && projectWiki:
                       {
                           foreach (var w in TaskExtensions.GetResult<List<WikiV2>>(Data.GetClient<WikiHttpClient>()
                                   .GetAllWikisAsync(tp.Name), $"Error getting project wiki")
                               .Where(r => r.Type == WikiType.ProjectWiki))
                           {
                               yield return w;
                           }
                           yield break;
                       }
                   case WikiV2 w:
                       {
                           yield return w;
                           yield break;
                       }
                   case Guid guid:
                       {
                           yield return TaskExtensions.GetResult<WikiV2>(Data.GetClient<WikiHttpClient>()
                                   .GetWikiAsync(tp.Name, guid), $"Error getting Wiki with ID {guid}");

                           yield break;
                       }
                   case string s when s.IsGuid():
                       {
                           wiki = new Guid(s);
                           continue;
                       }
                   case string s when !s.IsWildcard():
                       {
                           yield return TaskExtensions.GetResult<WikiV2>(Data.GetClient<WikiHttpClient>()
                                   .GetWikiAsync(tp.Name, s), $"Error getting Wiki '{s}'");

                           yield break;
                       }
                   case string s:
                       {
                           foreach (var w in TaskExtensions.GetResult<List<WikiV2>>(Data.GetClient<WikiHttpClient>()
                                   .GetAllWikisAsync(tp.Name), $"Error getting wiki(s) '{s}'")
                               .Where(r => r.Name.IsLike(s)))
                           {
                               yield return w;
                           }
                           yield break;
                       }
                   default:
                       {
                           throw new ArgumentException(nameof(GetWiki.Wiki));
                       }
               }
           }
       }
    }
}