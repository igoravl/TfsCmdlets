using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Cmdlets.Git;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitCommitRef))]
    partial class GetGitCommitController
    {
        public override IEnumerable<GitCommitRef> Invoke()
        {
            var tp = Data.GetProject();
            var repository = Data.GetItem<GitRepository>();

            var client = Data.GetClient<GitHttpClient>();

            return null;

            // var criteria = new GitQueryCommitsCriteria() {
            //     Author = Parameters.Get<string>(nameof(GetGitCommit.Author)),
            //     Committer = Parameters.Get<string>(nameof(GetGitCommit.Committer)),
            //     CompareVersion = Parameters.Get<GitVersionDescriptor>(nameof(GetGitCommit.CompareVersion)),
            //     ExcludeDeletes = Parameters.Get<bool>(nameof(GetGitCommit.ExcludeDeletes)),
            //     FromCommitId = Parameters.Get<string>(nameof(GetGitCommit.FromCommit)),
            //     FromDate = Parameters.Get<DateTime>(nameof(GetGitCommit.FromDate)),
            //     ItemPath = Parameters.Get<string>(nameof(GetGitCommit.ItemPath)),
            //     ItemVersion = Parameters.Get<GitVersionDescriptor>(nameof(GetGitCommit.ItemVersion)),
            //     IncludeLinks = Parameters.Get<bool>(nameof(GetGitCommit.IncludeLinks)),
            //     IncludePushData = Parameters.Get<bool>(nameof(GetGitCommit.IncludePushData)),
            //     IncludeUserImageUrl = Parameters.Get<bool>(nameof(GetGitCommit.IncludeUserImageUrl)),
            //     ShowOldestCommitsFirst = Parameters.Get<bool>(nameof(GetGitCommit.ShowOldestCommitsFirst)),
            //     ToCommitId = Parameters.Get<string>(nameof(GetGitCommit.ToCommit)),
            //     ToDate = Parameters.Get<DateTime>(nameof(GetGitCommit.ToDate)),
            //     Skip = Parameters.Get<int>(nameof(GetGitCommit.Skip)),
            //     Top = Parameters.Get<int>(nameof(GetGitCommit.Top)),
            // };

            // client.GetCommitsBatchAsync())

            // while (true) switch (repository)
            // {
            //     case null:
            //     case string s when string.IsNullOrEmpty(s):
            //     {
            //         repository = tp.Name;
            //         continue;
            //     }
            //     case GitRepository repo:
            //     {
            //         yield return repo;
            //         yield break;
            //     }
            //     case Guid guid:
            //     {
            //         yield return Data.GetClient<GitHttpClient>()
            //             .GetRepositoryAsync(tp.Name, guid)
            //             .GetResult($"Error getting repository with ID {guid}");

            //         yield break;
            //     }
            //     case string s when s.IsGuid():
            //     {
            //         repository = new Guid(s);
            //         continue;
            //     }
            //     case string s when !s.IsWildcard():
            //     {
            //         GitRepository result;

            //         try
            //         {
            //             result = Data.GetClient<GitHttpClient>()
            //                 .GetRepositoryAsync(tp.Name, s)
            //                 .GetResult($"Error getting repository '{s}'");
            //         }
            //         catch
            //         {
            //             // Workaround to retrieve disabled repositories
            //             result = Data.GetClient<GitHttpClient>()
            //                 .GetRepositoriesAsync(tp.Name)
            //                 .GetResult($"Error getting repository(ies) '{s}'")
            //                 .First(r => r.Name.Equals(s, StringComparison.OrdinalIgnoreCase));
            //         }

            //         yield return result;
            //         yield break;
            //     }
            //     case string s:
            //     {
            //         foreach (var repo in Data.GetClient<GitHttpClient>()
            //             .GetRepositoriesAsync(tp.Name)
            //             .GetResult($"Error getting repository(ies) '{s}'")
            //             .Where(r => r.Name.IsLike(s)))
            //         {
            //             yield return repo;
            //         }

            //         yield break;
            //     }
            //     default:
            //     {
            //         throw new ArgumentException("Repository");
            //     }
            // }
        }
    }
}
