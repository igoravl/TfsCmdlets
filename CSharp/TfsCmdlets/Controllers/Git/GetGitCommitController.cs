using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Cmdlets.Git;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitCommitRef))]
    partial class GetGitCommitController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<GitHttpClient>();
            var repository = GetItem<GitRepository>();
            string commitSha;

            if (Has_Commit)
            {
                // Get commits by SHA

                foreach (var commit in Commit)
                {
                    switch (commit)
                    {
                        case GitCommitRef gcr:
                            {
                                yield return gcr;
                                yield break;
                            }
                        case string s:
                            {
                                commitSha = s;
                                break;
                            }
                        default:
                            {
                                Logger.LogError(new ArgumentException($"Invalid or non-existent commit '{commit}'"));
                                continue;
                            }
                    }

                    yield return client.GetCommitAsync(repository.ProjectReference.Id.ToString(), commitSha, repository.Id.ToString())
                        .GetResult($"Error getting commit '{commitSha}' in repository '{repository.Name}'");
                }
                yield break;
            }

            GitVersionDescriptor itemVersion = null;

            if (Has_Tag)
            {
                itemVersion = new GitVersionDescriptor()
                {
                    VersionType = GitVersionType.Tag,
                    Version = Tag
                };
            }
            else if (Has_Branch)
            {
                itemVersion = new GitVersionDescriptor()
                {
                    VersionType = GitVersionType.Branch,
                    Version = Branch
                };
            }
            else
            {
                if(repository.DefaultBranch == null)
                {
                    Logger.LogError($"Repository '{repository.Name}' has no default branch set. Please specify a different repository or branch.");
                    yield break;
                }

                itemVersion = new GitVersionDescriptor()
                {
                    VersionType = GitVersionType.Branch,
                    Version = repository.DefaultBranch.Substring(repository.DefaultBranch.LastIndexOf('/') + 1)
                };
            }

            // Search for commits

            var criteria = new GitQueryCommitsCriteria()
            {
                Author = Author,
                Committer = Committer,
                CompareVersion = CompareVersion,
                ExcludeDeletes = ExcludeDeletes,
                FromCommitId = FromCommit,
                ItemPath = ItemPath,
                ItemVersion = itemVersion,
                IncludeLinks = IncludeLinks,
                IncludePushData = IncludePushData,
                IncludeUserImageUrl = IncludeUserImageUrl,
                ShowOldestCommitsFirst = ShowOldestCommitsFirst,
                ToCommitId = ToCommit,
                Skip = Skip,
                Top = Top == 0 ? null : Top,
                FromDate = Has_FromDate ? this.FromDate.ToString("yyyy-MM-ddTHH:mm:ssK") : null,
                ToDate = Has_ToDate ? this.ToDate.ToString("yyyy-MM-ddTHH:mm:ssK") : null,
            };

            var result = client.GetCommitsBatchAsync(criteria, repository.Id)
                .GetResult("Error getting Git commits");

            foreach (var commit in result)
            {
                yield return commit;
            }
        }
    }
}
