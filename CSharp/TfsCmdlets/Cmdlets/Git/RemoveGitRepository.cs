using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Deletes one or more Git repositories from a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true)]
    partial class RemoveGitRepository
    {
        /// <summary>
        /// Specifies the repository to be deleted. Value can be the name or ID of a Git repository, 
        /// as well as a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object representing a Git
        /// repository.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; }

        /// <summary>
        /// Permanently deletes the repository instead of moving it to the recycle bin.
        /// When omitted, the repository is moved to a recycle bin and can be recovered.
        /// When specified, the deletion is irreversible.
        /// </summary>
        [Parameter]
        public SwitchParameter Hard { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(GitRepository), Client = typeof(IGitHttpClient))]
    partial class RemoveGitRepositoryController
    {
        protected override IEnumerable Run()
        {
            var items = Items.ToList();
            var isDeleted = false;

            if (items.Count == 0)
            {
                // When no active repos were found and -Hard is specified, try the recycle bin;
                // otherwise, log a clear error

                if (Hard)
                {
                    items = Data.GetItems<GitRepository>(new { Repository, Deleted = true }).ToList();
                }
                else {
                    Logger.LogError($"Invalid or non-existent Git repository '{Repository}'");
                    return null;
                }
            }

            foreach (var repo in items)
            {
                if (!PowerShell.ShouldProcess($"{repo.ProjectReference.Name}/{repo.Name}", "Permanently delete repository from recycle bin")) continue;

                if (Hard && !(Force || PowerShell.ShouldContinue(
                    "You are using the -Hard switch. The repository deletion is IRREVERSIBLE " +
                    $"and may cause DATA LOSS. Are you sure you want to permanently delete Git repository " +
                    $"'{repo.Name}' in project '{repo.ProjectReference.Name}'?"))) continue;

                var isDeleted = string.IsNullOrEmpty(repo.RemoteUrl);

                if (!isDeleted) Client.DeleteRepositoryAsync(repo.ProjectReference.Id.ToString(), repo.Id).Wait();
                if (Hard) Client.DeleteRepositoryFromRecycleBinAsync(repo.ProjectReference.Id.ToString(), repo.Id).Wait();
            }
            
            return null;
        }
    }
}