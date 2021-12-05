using System.Collections.Generic;
using System.Composition;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.HttpClient;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitRepository))]
    partial class EnableGitRepositoryController 
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

                client.UpdateRepositoryEnabledStatus(tp.Name, repo.Id, true);

                yield return repo;
            }
        }
    }
}