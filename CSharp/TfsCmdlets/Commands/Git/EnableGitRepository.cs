using System.Collections.Generic;
using System.Composition;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.HttpClient;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.Git
{
    [Command]
    internal class EnableGitRepository : CommandBase<GitRepository>
    {
        public override IEnumerable<GitRepository> Invoke(ParameterDictionary parameters)
        {
            var tp = Data.GetProject();
            var repos = GetItems();

            var client = GetClient<GitExtendedHttpClient>();

            foreach (var repo in repos)
            {
                if (!PowerShell.ShouldProcess($"Team project '{tp.Name}'", $"Disable Git repository '{repo.Name}'"))
                    continue;

                client.UpdateRepositoryEnabledStatus(tp.Name, repo.Id, true);

                yield return repo;
            }
        }

        [ImportingConstructor]
        public EnableGitRepository(IPowerShellService powerShell, IDataManager data, ILogger logger) : base(powerShell, data, logger)
        {
        }
    }
}