using Microsoft.VisualStudio.Services.Feed.WebApi;

namespace TfsCmdlets.Controllers.Artifact
{
    [CmdletController(typeof(WebApiPackage))]
    partial class GetArtifactController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<FeedHttpClient>();

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

                switch (artifact)
                {
                    case Guid g:
                        {
                            yield return client.GetPackageAsync(packageId: g.ToString(), feedId: feedId, project: projectId)
                                .GetResult($"Error getting artifact feed(s) '{g}'");
                            break;
                        }
                    case string s when s.IsWildcard():
                        {
                            yield return client.GetPackagesAsync(projectId, feedId, protocolType: ProtocolType,
                                    includeDeleted: IncludeDeleted, includeDescription: IncludeDescription, includeAllVersions: true,
                                    isListed: !IncludeDelisted, getTopPackageVersions: true, isRelease: !IncludePrerelease)
                                .GetResult($"Error getting artifact feed(s) '{s}'")
                                .Where(p => p.Name.IsLike(s));
                            break;
                        }
                    case string s:
                        {
                            yield return client.GetPackageAsync(project: projectId, feedId: feedId, packageId: s,
                                    includeDeleted: IncludeDeleted, includeDescription: IncludeDescription, includeAllVersions: true,
                                    isListed: !IncludeDelisted, isRelease: !IncludePrerelease)
                                .GetResult($"Error getting artifact feed(s) '{s}'");
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