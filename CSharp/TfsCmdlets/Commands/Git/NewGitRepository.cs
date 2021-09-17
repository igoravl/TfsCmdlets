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
    internal class NewGitRepository : CommandBase<GitRepository>
    {
        public override IEnumerable<GitRepository> Invoke(ParameterDictionary parameters)
        {
            var tp = Data.GetProject();
            var repo = parameters.Get<string>(nameof(Cmdlets.Git.NewGitRepository.Repository));

            if (!PowerShell.ShouldProcess(tp, $"Create Git repository '{repo}'")) yield break;

            var client = GetClient<GitHttpClient>();

            var tpRef = new TeamProjectReference
            {
                Id = tp.Id,
                Name = tp.Name
            };

            var repoToCreate = new GitRepository
            {
                Name = repo,
                ProjectReference = tpRef
            };

            yield return client.CreateRepositoryAsync(repoToCreate, tp.Name)
                .GetResult("Error creating Git repository");
        }

        [ImportingConstructor]
        public NewGitRepository(IPowerShellService powerShell, IConnectionManager connections, IDataManager data, ILogger logger) : base(powerShell, connections, data, logger)
        {
        }
    }
}