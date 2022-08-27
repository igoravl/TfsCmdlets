using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitRepository))]
    partial class RemoveGitRepositoryController 
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<GitHttpClient>();

            foreach (var repo in Items)
            {
                if (!PowerShell.ShouldProcess($"[Project: {repo.ProjectReference.Name}]/[Repository: {repo.Name}]", $"Delete repository")) continue;

                if (!(repo.DefaultBranch == null || Force) && !PowerShell.ShouldContinue($"Are you sure you want to delete Git repository '{repo.Name}'?")) continue;

                client.DeleteRepositoryAsync(repo.Id).Wait();
            }

            return null;
        }
    }
}