using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Feed.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Artifact
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

        /// <summary>
        /// Returns only feeds from the given scope (collection or project). 
        /// When omitted, returns all feeds.
        /// </summary>
        [Parameter()]
        public ArtifactFeedScope Scope { get; set; } = ArtifactFeedScope.Collection | ArtifactFeedScope.Project;

        /// <summary>
        /// Filters by this role, either Administrator, Contributor, or Reader level permissions.
        /// When omitted, filters by Administrator roles.
        /// </summary>
        [Parameter()]
        [ValidateSet("Administrator", "Contributor", "Reader")]
        public FeedRole FeedRole { get; set; } = FeedRole.Administrator;
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
                        foreach (var o in client.GetFeedsAsync(feedRole)
                            .GetResult("Error getting artifact feeds")
                            .Where(f1 => f1.Name.IsLike(s) && (
                                (string.IsNullOrEmpty(f1.Project?.Name) && ((scope & ArtifactFeedScope.Collection) > 0)) || 
                                (!string.IsNullOrEmpty(f1.Project?.Name) && ((scope & ArtifactFeedScope.Project) > 0))
                            )))
                        {
                            yield return o;
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