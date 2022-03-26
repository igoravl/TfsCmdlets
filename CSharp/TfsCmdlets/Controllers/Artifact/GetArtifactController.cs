using Microsoft.VisualStudio.Services.Feed.WebApi;

namespace TfsCmdlets.Controllers.Artifact
{
    [CmdletController(typeof(WebApiPackage))]
    partial class GetArtifactController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<FeedHttpClient>();
            var feed = GetItem<WebApiFeed>();

            var feedId = feed.Id.ToString();
            var projectId = feed.Project?.Id == null ? null : feed.Project.Id.ToString();

            foreach (var input in Artifact)
            {
                var artifact = input switch
                {
                    string s when s.IsGuid() => Guid.Parse(s),
                    _ => input
                };

                switch (artifact)
                {
                    case WebApiPackage p:
                        {
                            yield return p;
                            break;
                        }
                    case Guid g:
                        {
                            yield return client.GetPackageAsync(packageId: g.ToString(), feedId: feedId, project: projectId)
                                .GetResult($"Error getting artifact feed(s) '{g}'");
                            break;
                        }
                    case string s when !string.IsNullOrEmpty(s):
                        {
                            foreach (var pkg in client.GetPackagesAsync(projectId, feedId, packageNameQuery: s, protocolType: ProtocolType, 
                                includeDeleted: IncludeDeleted, includeDescription: IncludeDescription, includeAllVersions: true, 
                                isListed: !IncludeDelisted, getTopPackageVersions: true, isRelease: !IncludePrerelease)
                                .GetResult($"Error getting artifact feed(s) '{s}'"))
                            {
                                yield return pkg;
                            }
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