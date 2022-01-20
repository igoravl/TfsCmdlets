using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitRepository))]
    partial class DisableGitRepositoryController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<GitExtendedHttpClient>();

            foreach (var repo in Items)
            {
                if (!PowerShell.ShouldProcess(Project, $"Disable Git repository '{repo.Name}'")) continue;

                client.UpdateRepositoryEnabledStatus(Project.Name, repo.Id, false);

                yield return GetItem<GitRepository>();
            }
        }
    }
}
