using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitRepository))]
    partial class RemoveGitRepositoryController 
    {
        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var repos = Data.GetItems<GitRepository>();
            var force = Parameters.Get<bool>(nameof(Cmdlets.Git.RemoveGitRepository.Force));

            var client = Data.GetClient<GitHttpClient>();

            foreach (var r in repos)
            {
                if (!PowerShell.ShouldProcess(tp, $"Delete Git repository '{r.Name}'")) continue;

                if (!force &&
                    !PowerShell.ShouldContinue($"Are you sure you want to delete Git repository '{r.Name}'?")) continue;

                client.DeleteRepositoryAsync(r.Id).Wait();
            }

            return null;
        }
    }
}