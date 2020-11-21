using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Wiki.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Wiki
{
    /// <summary>
    /// Gets information from one or more Wiki repositories in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsWiki")]
    [OutputType(typeof(WikiV2))]
    public class GetWiki : GetCmdletBase<WikiV2>
    {
        /// <summary>
        /// Specifies the name or ID of a Wiki repository. Wildcards are supported. 
        /// When omitted, all Wiki repositories in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get all wikis")]
        [SupportsWildcards()]
        [Alias("Name", "Id")]
        public object Wiki { get; set; } = "*";


        [Parameter(ParameterSetName = "Get Project Wiki")]
        public SwitchParameter ProjectWiki { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }
    }

    [Exports(typeof(WikiV2))]
    internal partial class WikiDataService : BaseDataService<WikiV2>
    {
        protected override IEnumerable<WikiV2> DoGetItems()
        {
            var (_, tp) = GetCollectionAndProject();
            var wiki = GetParameter<object>("Wiki");
            var projectWiki = GetParameter<bool>("ProjectWiki");

            while (true)
            {
                switch (wiki)
                {
                    case null when projectWiki:
                    case string s when string.IsNullOrEmpty(s) && projectWiki:
                        {
                            foreach (var w in GetClient<WikiHttpClient>()
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
                            yield return GetClient<WikiHttpClient>()
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
                            yield return GetClient<WikiHttpClient>()
                                .GetWikiAsync(tp.Name, s)
                                .GetResult($"Error getting Wiki '{s}'");

                            yield break;
                        }
                    case string s:
                        {
                            foreach (var w in GetClient<WikiHttpClient>()
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