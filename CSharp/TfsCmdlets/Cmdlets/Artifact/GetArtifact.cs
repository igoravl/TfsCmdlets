using Microsoft.VisualStudio.Services.Feed.WebApi;

namespace TfsCmdlets.Cmdlets.Artifact
{
    /// <summary>
    /// Gets information from one or more artifact feeds.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(WebApiPackage))]
    partial class GetArtifact
    {
        /// <summary>
        /// Specifies the package (artifact) name. Wildcards are supported. 
        /// When omitted, returns all packages in the specified feed.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        [Alias("Package")]
        public object Artifact { get; set; } = "*";

        /// <summary>
        /// Specifies the feed name. 
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public object Feed { get; set; }

        /// <summary>
        /// Includes deletes packages in the result. 
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeDeleted { get; set; }

        /// <summary>
        /// Includes the package description in the results. 
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeDescription { get; set; }

        /// <summary>
        /// Includes prerelease packages in the results. Applies only to Nuget packages.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludePrerelease { get; set; }

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

        [Parameter()]
        internal bool IncludeAllVersions { get; set; }
    }

    [CmdletController(typeof(WebApiPackage))]
    partial class GetArtifactController
    {
        [Import]
        private IFeedHttpClient Client { get; set; }
        
        protected override IEnumerable Run()
        {
            foreach (var input in Artifact)
            {
                var artifact = input switch
                {
                    null => throw new ArgumentException("Artifact name cannot be empty", "Artifact"),
                    string s when string.IsNullOrEmpty(s) => throw new ArgumentException("Artifact name cannot be empty", "Artifact"),
                    string s when s.IsGuid() => Guid.Parse(s),
                    _ => input
                };

                if (input is WebApiPackage p)
                {
                    yield return p;
                    continue;
                }

                var feed = GetItem<WebApiFeed>();
                var feedId = feed.Id.ToString();
                var projectId = feed.Project?.Id == null ? null : feed.Project.Id.ToString();
                var getTopPackageVersions = (IncludeDeleted || IncludeDelisted || IncludePrerelease) && !IncludeAllVersions;

                switch (artifact)
                {
                    case Guid g:
                        {
                            yield return Client.GetPackageAsync(
                                    packageId: g.ToString(), 
                                    feedId: feedId, 
                                    project: projectId)
                                .GetResult($"Error getting artifact feed(s) '{g}'");
                            break;
                        }
                    case string s when s.IsWildcard():
                        {
                            yield return Client.GetPackagesAsync(
                                    project: projectId, 
                                    feedId: feedId, 
                                    protocolType: ProtocolType,
                                    getTopPackageVersions: getTopPackageVersions, 
                                    includeAllVersions: getTopPackageVersions || IncludeAllVersions,
                                    includeDeleted: IncludeDeleted? true: null, 
                                    includeDescription: !getTopPackageVersions, 
                                    isListed: IncludeDelisted || IncludeDeleted? null: true, 
                                    isRelease: IncludePrerelease? null: true)
                                .GetResult($"Error getting artifact feed(s) '{s}'")
                                .Where(p => p.Name.IsLike(s));
                            break;
                        }
                    case string s:
                        {
                            yield return Client.GetPackagesAsync(
                                    project: projectId, 
                                    feedId: feedId, 
                                    protocolType: ProtocolType, 
                                    getTopPackageVersions: getTopPackageVersions, 
                                    includeAllVersions: getTopPackageVersions || IncludeAllVersions,
                                    includeDeleted: IncludeDeleted? null: true, 
                                    includeDescription: !getTopPackageVersions, 
                                    isListed: IncludeDelisted? null: true, 
                                    isRelease: IncludePrerelease? null: true,
                                    packageNameQuery: s)
                                .GetResult($"Error getting artifact feed(s) '{s}'")
                                .Where(p => p.Name.Equals(s, StringComparison.OrdinalIgnoreCase));
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent artifact '{artifact}'"));
                            break;
                        }
                }
            }
        }
    }
}