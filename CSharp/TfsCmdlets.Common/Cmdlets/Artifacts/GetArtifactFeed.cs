using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Feed.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Gets information from one or more Git repositories in a team project.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsArtifactFeed")]
    [OutputType(typeof(Feed))]
    public class GetArtifactFeed : GetCmdletBase<Feed>
    {
        /// <summary>
        /// Specifies the feed name. Wildcards are supported. 
        /// When omitted, returns all feeds.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [SupportsWildcards]
        public object Feed { get; set; } = "*";

        [Parameter()]
        public ArtifactFeedScope Scope { get; set; } = ArtifactFeedScope.Collection;

        [Parameter()]
        public FeedRole FeedRole { get; set; } = FeedRole.Contributor;
    }

    [Exports(typeof(Feed))]
    internal partial class ArtifactFeedDataService : BaseDataService<Feed>
    {
        protected override IEnumerable<Feed> DoGetItems()
        {
            var tpc = GetCollection();
            var feed = GetParameter<object>(nameof(GetArtifactFeed.Feed));
            var feedRole = GetParameter<FeedRole>(nameof(GetArtifactFeed.FeedRole));
            var scope = GetParameter<ArtifactFeedScope>(nameof(GetArtifactFeed.Scope));
            var client = GetClient<FeedHttpClient>();

            while(true) switch (feed)
            {
                case Feed f:
                    {
                        yield return f;
                        yield break;
                    }
                case string s when !string.IsNullOrEmpty(s):
                    {
                        foreach(var f1 in client.GetFeedsAsync(feedRole)
                            .GetResult("Error getting artifact feeds"))
                            {
                                yield return f1;
                            }
                            yield break;
                    }
                default:
                    {
                        throw new ArgumentException(nameof(GetArtifactFeed.Feed));
                    }

            }
        }
    }
}