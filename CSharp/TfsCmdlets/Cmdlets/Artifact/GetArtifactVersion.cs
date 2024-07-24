using Microsoft.VisualStudio.Services.Feed.WebApi;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Cmdlets.Artifact
{
    /// <summary>
    /// Gets information from one or more artifact feeds.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(PackageVersion))]
    partial class GetArtifactVersion
    {
        /// <summary>
        /// Specifies the version to return. Wildcards are supported. 
        /// When omitted, returns all versions of the specified package.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Version { get; set; } = "*";

        /// <summary>
        /// Specifies the package (artifact) name. 
        /// </summary>
        [Parameter(Position = 1, ValueFromPipeline = true, Mandatory = true)]
        [Alias("Package")]
        [ValidateNotNullOrEmpty]
        public object Artifact { get; set; }

        /// <summary>
        /// Specifies the feed name. 
        /// </summary>
        [Parameter()]
        [ValidateNotNullOrEmpty]
        public object Feed { get; set; }

        /// <summary>
        /// Includes deletes packages in the result. 
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeDeleted { get; set; }

        /// <summary>
        /// Includes delisted packages in the results. Applies only to Nuget packages.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeDelisted { get; set; }

        /// <summary>
        /// Returns only packages of the specified protocol type.
        /// </summary>
        [Parameter()]
        public string ProtocolType { get; set; }
    }

    [CmdletController(typeof(PackageVersion))]
    partial class GetArtifactVersionController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<FeedHttpClient>();

            foreach (var input in Version)
            {
                var version = input switch
                {
                    string s when string.IsNullOrEmpty(s) => throw new ArgumentException("Version cannot be empty", "Artifact"),
                    string s when s.IsGuid() => new Guid(s),
                    _ => input
                };

                if (version is PackageVersion pv)
                {
                    yield return pv;
                    continue;
                }

                if(Feed == null && Artifact is not WebApiPackage)
                {
                    throw new ArgumentException("Feed must be specified when Artifact is not a Package object", "Artifact");
                }

                var package = GetItem<WebApiPackage>();
                var packageId = package.Id.ToString();
                var feedUri = new Uri(((ReferenceLink)package.Links.Links["feed"]).Href);
                var feedId = feedUri.Segments[feedUri.Segments.Length - 1].Trim('/');
                var feed = GetItem<WebApiFeed>(new {Feed = feedId});
                var projectId = feed.Project?.Id.ToString();

                switch (version)
                {
                    case Guid g:
                        {
                            break;
                        }
                    case string s when s.IsWildcard():
                        {
                            yield return client.GetPackageVersionsAsync(
                                    project: projectId, 
                                    feedId: feedId, 
                                    packageId: package.Id.ToString(),
                                    isDeleted: IncludeDeleted? null: false,
                                    isListed: IncludeDelisted? null: true)
                                .GetResult($"Error getting artifact version(s) '{s}'")
                                .Where(pv => pv.Version.IsLike(s));
                            break;
                        }
                    case string s:
                        {
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent artifact version '{version}'"));
                            break;
                        }
                }
            }
        }
    }
}