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
    internal class NewGitRepository : ControllerBase<GitRepository>
    {
        public override IEnumerable<GitRepository> Invoke()
        {
            var tp = Data.GetProject();
            var repo = Parameters.Get<string>(nameof(Cmdlets.Git.NewGitRepository.Repository));

            if (!PowerShell.ShouldProcess(tp, $"Create Git repository '{repo}'")) yield break;

            var client = Data.GetClient<GitHttpClient>();

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
        public NewGitRepository(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger) 
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}