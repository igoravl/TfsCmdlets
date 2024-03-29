using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitRepository))]
    partial class GetGitRepositoryController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<GitHttpClient>();

            foreach (var input in Repository)
            {
                var repository = input switch
                {
                    string s when string.IsNullOrEmpty(s) => Project.Name,
                    string s when s.IsGuid() => new Guid(s),
                    null => Project.Name,
                    _ => input
                };

                switch (repository)
                {
                    case GitRepository repo:
                        {
                            yield return repo;
                            break;
                        }
                    case Guid guid:
                        {
                            yield return client
                                .GetRepositoryAsync(Project.Name, guid, includeParent: IncludeParent)
                                .GetResult($"Error getting repository with ID {guid}");
                            break;
                        }
                    case { } when Default:
                        {
                            yield return client
                                .GetRepositoryAsync(Project.Name, Project.Name, includeParent: IncludeParent)
                                .GetResult($"Error getting repository '{Project.Name}'");
                            break;
                        }
                    case string s when !s.IsWildcard():
                        {
                            GitRepository result;

                            try
                            {
                                result = client
                                    .GetRepositoryAsync(Project.Name, s, includeParent: IncludeParent)
                                    .GetResult($"Error getting repository '{s}'");
                            }
                            catch
                            {
                                // Workaround to retrieve disabled repositories
                                result = client
                                    .GetRepositoriesAsync(Project.Name, includeLinks: true, includeHidden: true)
                                    .GetResult($"Error getting repository(ies) '{s}'")
                                    .First(r => r.Name.Equals(s, StringComparison.OrdinalIgnoreCase));

                                if (IncludeParent)
                                {
                                    result = client
                                        .GetRepositoryAsync(Project.Name, result.Id, includeParent: true)
                                        .GetResult($"Error getting repository(ies) '{s}'");
                                }
                            }
                            yield return result;
                            break;
                        }
                    case string s:
                        {
                            foreach (var repo in client
                                .GetRepositoriesAsync(Project.Name, includeLinks: true)
                                .GetResult($"Error getting repository(ies) '{s}'")
                                .Where(r => r.Name.IsLike(s)))
                            {
                                if (IncludeParent)
                                {
                                    yield return client
                                        .GetRepositoryAsync(Project.Name, repo.Id, includeParent: true)
                                        .GetResult($"Error getting repository(ies) '{s}'");
                                }
                                else
                                {
                                    yield return repo;
                                }
                            }
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent repository '{repository}'"));
                            break;
                        }
                }
            }
        }
    }
}
