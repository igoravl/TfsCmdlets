using Microsoft.VisualStudio.Services.Feed.WebApi;

namespace TfsCmdlets.Cmdlets.Artifact
{
    /// <summary>
    /// Gets information from one or more Git repositories in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, NoAutoPipeline = true, OutputType = typeof(FeedView))]
    partial class GetArtifactFeedView 
    {
        /// <summary>
        /// Specifies the view name. Wildcards are supported. 
        /// When omitted, returns all views.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        public object View { get; set; } = "*";

        /// <summary>
        /// Specifies the parent feed.
        /// </summary>
        [Parameter(Position = 1, ValueFromPipeline = true, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public object Feed { get; set; }

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
        public Microsoft.VisualStudio.Services.Feed.WebApi.FeedRole Role { get; set; } = Microsoft.VisualStudio.Services.Feed.WebApi.FeedRole.Administrator;
    }

    [CmdletController(typeof(FeedView))]
    partial class GetArtifactFeedViewController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<FeedHttpClient>();

            foreach (var input in View)
            {
                var view = input switch
                {
                    _ => input
                };

                var feed = GetItem<Feed>();

                switch (view)
                {
                    case FeedView fv:
                        {
                            yield return fv;
                            break;
                        }
                    case string s when !string.IsNullOrEmpty(s) && feed.Project == null:
                        {
                            yield return client.GetFeedViewsAsync(feed.Id.ToString())
                                .GetResult($"Error getting artifact feed view(s) '{s}'")
                                .Where(fv => fv.Name.IsLike(s));
                            break;
                        }
                    case string s when !string.IsNullOrEmpty(s):
                        {
                            yield return client.GetFeedViewsAsync(feed.Project.Id, feed.Id.ToString())
                                .GetResult($"Error getting artifact feed view(s) '{s}'")
                                .Where(fv => fv.Name.IsLike(s));
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent feed view '{view}'"));
                            break;
                        }
                }
            }
        }
    }
}