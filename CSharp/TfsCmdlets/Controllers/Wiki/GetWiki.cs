using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Cmdlets.Wiki;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.Wiki
{
    [CmdletController]
    internal class GetWikiController : ControllerBase<WikiV2>
    {
        public override IEnumerable<WikiV2> Invoke(ParameterDictionary parameters)
        {
            var tp = Data.GetProject(parameters);
            var wiki = parameters.Get<object>("Wiki");
            var projectWiki = parameters.Get<bool>("ProjectWiki");

            while (true)
            {
                switch (wiki)
                {
                    case null when projectWiki:
                    case string s when string.IsNullOrEmpty(s) && projectWiki:
                        {
                            foreach (var w in Data.GetClient<WikiHttpClient>(parameters)
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
                            yield return Data.GetClient<WikiHttpClient>(parameters)
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
                            yield return Data.GetClient<WikiHttpClient>(parameters)
                                .GetWikiAsync(tp.Name, s)
                                .GetResult($"Error getting Wiki '{s}'");

                            yield break;
                        }
                    case string s:
                        {
                            foreach (var w in Data.GetClient<WikiHttpClient>(parameters)
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

        [ImportingConstructor]
        public GetWikiController(IPowerShellService powerShell, IDataManager data, ILogger logger)
            : base(powerShell, data, logger)
        {
        }
    }
}