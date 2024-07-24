using Microsoft.VisualStudio.Services.Feed.WebApi;

namespace TfsCmdlets.Cmdlets.Artifact
{
    /// <summary>
    /// Gets information from one or more artifact feeds.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(WebApiFeed))]
    partial class GetArtifactFeed
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
        public ProjectOrCollectionScope Scope { get; set; } = ProjectOrCollectionScope.All;

        /// <summary>
        /// Filters by role. Returns only those feeds where the currently logged on user
        /// has one of the specified roles: either Administrator, Contributor, 
        /// or Reader level permissions. When omitted, filters by Administrator role.
        /// </summary>
        [Parameter()]
        [ValidateSet("Administrator", "Contributor", "Reader")]
        public Microsoft.VisualStudio.Services.Feed.WebApi.FeedRole Role { get; set; } = Microsoft.VisualStudio.Services.Feed.WebApi.FeedRole.Reader;
    }

    [CmdletController(typeof(Feed))]
    partial class GetArtifactFeedController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<FeedHttpClient>();

            foreach (var input in Feed)
            {
                var feed = input switch
                {
                    null => throw new ArgumentException("Feed name cannot be empty", "Feed"),
                    string s when string.IsNullOrEmpty(s) => throw new ArgumentException("Feed name cannot be empty", "Feed"),
                    string s when s.IsGuid() => Guid.Parse(s),
                    _ => input
                };

                switch (feed)
                {
                    case Feed f:
                        {
                            yield return f;
                            break;
                        }
                    case Guid g:
                        {
                            yield return client.GetFeedsAsync(Role)
                                .GetResult($"Error getting artifact feed(s) '{g}'")
                                .Where(f => f.Id == g);
                            break;
                        }
                    case string s when !string.IsNullOrEmpty(s):
                        {
                            yield return client.GetFeedsAsync(Role)
                                .GetResult($"Error getting artifact feed(s) '{s}'")
                                .Where(f1 => f1.Name.IsLike(s) && (
                                    (string.IsNullOrEmpty(f1.Project?.Name) && ((Scope & ProjectOrCollectionScope.Collection) > 0)) ||
                                    (!string.IsNullOrEmpty(f1.Project?.Name) && ((Scope & ProjectOrCollectionScope.Project) > 0))));
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent feed '{feed}'"));
                            break;
                        }
                }
            }
        }
    }
}