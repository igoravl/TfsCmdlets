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
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(GitRepository), Client=typeof(IGitHttpClient))]
    partial class RemoveGitRepositoryController 
    {
        protected override IEnumerable Run()
        {
            foreach (var repo in Items)
            {
                if (!PowerShell.ShouldProcess($"[Project: {repo.ProjectReference.Name}]/[Repository: {repo.Name}]", $"Delete repository")) continue;

                if (!(repo.DefaultBranch == null || Force) && !PowerShell.ShouldContinue($"Are you sure you want to delete Git repository '{repo.Name}'?")) continue;

                Client.DeleteRepositoryAsync(repo.Id).Wait();
            }

            return null;
        }
    }
}