using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitRepository))]
    partial class GetGitRepositoryController 
    {
        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var repository = Parameters.Get<object>(nameof(Cmdlets.Git.GetGitRepository.Repository));

            while (true) switch (repository)
            {
                case null:
                case string s when string.IsNullOrEmpty(s):
                {
                    repository = tp.Name;
                    continue;
                }
                case GitRepository repo:
                {
                    yield return repo;
                    yield break;
                }
                case Guid guid:
                {
                    yield return Data.GetClient<GitHttpClient>()
                        .GetRepositoryAsync(tp.Name, guid)
                        .GetResult($"Error getting repository with ID {guid}");

                    yield break;
                }
                case string s when s.IsGuid():
                {
                    repository = new Guid(s);
                    continue;
                }
                case string s when !s.IsWildcard():
                {
                    GitRepository result;

                    try
                    {
                        result = Data.GetClient<GitHttpClient>()
                            .GetRepositoryAsync(tp.Name, s)
                            .GetResult($"Error getting repository '{s}'");
                    }
                    catch
                    {
                        // Workaround to retrieve disabled repositories
                        result = Data.GetClient<GitHttpClient>()
                            .GetRepositoriesAsync(tp.Name)
                            .GetResult($"Error getting repository(ies) '{s}'")
                            .First(r => r.Name.Equals(s, StringComparison.OrdinalIgnoreCase));
                    }

                    yield return result;
                    yield break;
                }
                case string s:
                {
                    foreach (var repo in Data.GetClient<GitHttpClient>()
                        .GetRepositoriesAsync(tp.Name)
                        .GetResult($"Error getting repository(ies) '{s}'")
                        .Where(r => r.Name.IsLike(s)))
                    {
                        yield return repo;
                    }

                    yield break;
                }
                default:
                {
                    throw new ArgumentException("Repository");
                }
            }
        }
    }
}
