using System.Collections.Generic;
using System.Composition;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.Git
{
    [Command]
    internal class RenameGitRepository : CommandBase<GitRepository>
    {
        public override IEnumerable<GitRepository> Invoke(ParameterDictionary parameters)
        {
            var tp = Data.GetProject();
            var repoToRename = GetItem(parameters);
            var newName = parameters.Get<string>(nameof(Cmdlets.Git.RenameGitRepository.NewName));

            if (!PowerShell.ShouldProcess(tp, $"Rename Git repository [{repoToRename.Name}] to '{newName}'"))
                yield break;

            var client = GetClient<GitHttpClient>();

            yield return client.RenameRepositoryAsync(repoToRename, newName)
                .GetResult("Error renaming repository");
        }

        [ImportingConstructor]
        public RenameGitRepository(IPowerShellService powerShell, IConnectionManager connections, IDataManager data, ILogger logger) : base(powerShell, connections, data, logger)
        {
        }
    }
}