using System.Collections.Generic;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitRepository))]
    partial class DisableGitRepositoryController
    {
        public override IEnumerable<GitRepository> Invoke()
        {
            var tp = Data.GetProject();
            var repos = GetItems();

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
