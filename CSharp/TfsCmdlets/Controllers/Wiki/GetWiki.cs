using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Wiki
{
    [CmdletController(typeof(WikiV2))]
    partial class GetWikiController
    {
       public override IEnumerable<WikiV2> Invoke()
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
                           foreach (var w in Data.GetClient<WikiHttpClient>()
                               .GetAllWikisAsync(tp.Name)
                               .GetResult($"Error getting project wiki")
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
                           yield return Data.GetClient<WikiHttpClient>()
                               .GetWikiAsync(tp.Name, guid)
                               .GetResult($"Error getting Wiki with ID {guid}");

                           yield break;
                       }
                   case string s when s.IsGuid():
                       {
                           wiki = new Guid(s);
                           continue;
                       }
                   case string s when !s.IsWildcard():
                       {
                           yield return Data.GetClient<WikiHttpClient>()
                               .GetWikiAsync(tp.Name, s)
                               .GetResult($"Error getting Wiki '{s}'");

                           yield break;
                       }
                   case string s:
                       {
                           foreach (var w in Data.GetClient<WikiHttpClient>()
                               .GetAllWikisAsync(tp.Name)
                               .GetResult($"Error getting wiki(s) '{s}'")
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