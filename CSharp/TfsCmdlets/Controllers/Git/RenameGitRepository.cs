using System.Collections.Generic;
using System.Composition;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController]
    internal class RenameGitRepository : ControllerBase<GitRepository>
    {
        public override IEnumerable<GitRepository> Invoke()
        {
            var tp = Data.GetProject();
            var repoToRename = GetItem();
            var newName = Parameters.Get<string>(nameof(Cmdlets.Git.RenameGitRepository.NewName));

            if (!PowerShell.ShouldProcess(tp, $"Rename Git repository [{repoToRename.Name}] to '{newName}'"))
                yield break;

            var client = Data.GetClient<GitHttpClient>();

            yield return client.RenameRepositoryAsync(repoToRename, newName)
                .GetResult("Error renaming repository");
        }

        [ImportingConstructor]
        public RenameGitRepository(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger) 
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}