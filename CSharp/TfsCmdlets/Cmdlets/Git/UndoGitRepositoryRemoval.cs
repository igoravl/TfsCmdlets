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
            var repos = Data.GetItems<GitRepository>(new { Deleted = true }).ToList();

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
