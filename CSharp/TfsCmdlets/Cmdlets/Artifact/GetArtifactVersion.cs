namespace TfsCmdlets.Cmdlets.Artifact
{
    /// <summary>
    /// Gets information from one or more artifact feeds.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(WebApiFeed))]
    partial class GetArtifactVersion
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
        public Microsoft.VisualStudio.Services.Feed.WebApi.FeedRole Role { get; set; } = Microsoft.VisualStudio.Services.Feed.WebApi.FeedRole.Administrator;
    }
}