using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Restores one or more deleted Git repositories from the recycle bin.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(GitRepository))]
    partial class UndoGitRepositoryRemoval
    {
        /// <summary>
        /// Specifies the name or ID of a deleted Git repository to restore. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; }
    }

    [CmdletController(typeof(GitRepository), Client = typeof(IGitHttpClient))]
    partial class UndoGitRepositoryRemovalController
    {
        protected override IEnumerable Run()
        {
            var repos = new List<GitDeletedRepository>();

            switch (Repository)
            {
                case GitDeletedRepository deletedRepo:
                {
                    repos.Add(deletedRepo);
                    break;
                }
                case GitRepository repo:
                {
                    repos.Add(new GitDeletedRepository { Id = repo.Id, Name = repo.Name, ProjectReference = repo.ProjectReference });
                    break;
                }
                case string s:
                {
                    repos.AddRange(Client
                        .GetRecycleBinRepositoriesAsync(Project.Name)
                        .GetResult("Error getting deleted Git repository(ies)")
                        .Where(r => r.Name.IsLike(s)));
                    break;
                }
                default:
                {
                    throw new ArgumentException($"Invalid or non-existent Git repository '{Repository}'", nameof(Repository));
                }
            }

            foreach (var repo in repos)
            {
                if (!PowerShell.ShouldProcess($"{Project.Name}/{repo.Name}", "Restore deleted Git repository")) continue;

                yield return Client
                    .RestoreRepositoryFromRecycleBinAsync(
                        new GitRecycleBinRepositoryDetails { Deleted = false },
                        repo.ProjectReference.Id.ToString(),
                        repo.Id)
                    .GetResult($"Error restoring Git repository '{repo.Name}'");
            }
        }
    }
}
