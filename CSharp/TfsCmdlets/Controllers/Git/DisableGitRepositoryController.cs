using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitRepository))]
    partial class DisableGitRepositoryController
    {
        protected override IEnumerable Run()
        {
            var tp = Data.GetProject();
            var repos = Data.GetItems<GitRepository>();

            var client = Data.GetClient<GitExtendedHttpClient>();

            foreach (var repo in repos)
            {
                if (!PowerShell.ShouldProcess($"Team project '{tp.Name}'", $"Disable Git repository '{repo.Name}'"))
                    continue;

                client.UpdateRepositoryEnabledStatus(tp.Name, repo.Id, false);

                yield return repo;
            }
        }
    }
}
