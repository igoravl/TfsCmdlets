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
    [Cmdlet(VerbsCommon.Get, "TfsArtifactFeedView")]
    [OutputType(typeof(FeedView))]
    public class GetArtifactFeedView : GetCmdletBase<FeedView>
    {
        /// <summary>
        /// Specifies the view name. Wildcards are supported. 
        /// When omitted, returns all views.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards]
        public object View { get; set; } = "*";

        /// <summary>
        /// Specifies the parent feed.
        /// </summary>
        [Parameter(Position = 1, ValueFromPipeline = true)]
        public object Feed { get; set; }
    }

    [Exports(typeof(FeedView))]
    internal partial class ArtifactFeedViewDataService : BaseDataService<FeedView>
    {
        protected override IEnumerable<FeedView> DoGetItems()
        {
            var tpc = GetCollection();
            var view = GetParameter<object>(nameof(GetArtifactFeedView.View));
            var feed = GetItem<Feed>(null);
            var client = GetClient<FeedHttpClient>();

            while (true) switch (view)
                {
                    case FeedView fv:
                        {
                            yield return fv;
                            yield break;
                        }
                    case string s when !string.IsNullOrEmpty(s):
                        {
                            foreach (var o in client.GetFeedViewsAsync(feed.Id.ToString())
                                .GetResult("Error getting artifact feed views")
                                .Where(v => v.Name.IsLike(s)))
                            {
                                yield return o;
                            }
                            yield break;
                        }
                    default:
                        {
                            throw new ArgumentException(nameof(GetArtifactFeedView.View));
                        }

                }
        }
    }
}