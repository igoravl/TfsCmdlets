using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitRepository))]
    partial class NewGitRepositoryController
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
    }
}