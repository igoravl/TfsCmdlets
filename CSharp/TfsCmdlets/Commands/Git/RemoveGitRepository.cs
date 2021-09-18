using System.Collections.Generic;
using System.Composition;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.Git
{
    [Command]
    internal class RemoveGitRepository : CommandBase<GitRepository>
    {
        public override IEnumerable<GitRepository> Invoke(ParameterDictionary parameters)
        {
            var tp = Data.GetProject();
            var repos = GetItems(parameters);
            var force = parameters.Get<bool>(nameof(Cmdlets.Git.RemoveGitRepository.Force));

            var client = GetClient<GitHttpClient>();

            foreach (var r in repos)
            {
                if (!PowerShell.ShouldProcess(tp, $"Delete Git repository '{r.Name}'")) continue;

                if (!force &&
                    !PowerShell.ShouldContinue($"Are you sure you want to delete Git repository '{r.Name}'?")) continue;

                client.DeleteRepositoryAsync(r.Id).Wait();
            }

            return null;
        }

        [ImportingConstructor]
        public RemoveGitRepository(IPowerShellService powerShell, IDataManager data, ILogger logger) : base(powerShell, data, logger)
        {
        }
    }
}