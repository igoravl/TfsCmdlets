using Microsoft.VisualStudio.Services.Feed.WebApi;

namespace TfsCmdlets.Controllers.Artifact
{
    [CmdletController(typeof(PackageVersion))]
    partial class GetArtifactVersionController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<FeedHttpClient>();

            var package = GetItem<WebApiPackage>();

            var packageId = package.Id.ToString();
            var feedId = package.Links.Links["feed"].ToString();
            var projectId = "";

            foreach (var input in Version)
            {
                var version = input switch
                {
                    string s when string.IsNullOrEmpty(s) => throw new ArgumentException("Version cannot be empty", "Artifact"),
                    _ => input
                };

                switch (version)
                {
                    case PackageVersion pv:
                        {
                            yield return pv;
                            break;
                        }
                    case Guid g:
                        {
                            break;
                        }
                    case string s when s.IsWildcard():
                        {
                            yield return client.GetPackageVersionsAsync(projectId, feedId, package.ProtocolType,
                                    package.NormalizedName, true, IncludeDelisted? null: false, IncludeDeleted? null: false)
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