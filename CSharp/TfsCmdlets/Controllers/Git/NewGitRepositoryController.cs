using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Controllers.Git
{
    [CmdletController(typeof(GitRepository))]
    partial class NewGitRepositoryController
    {
        protected override IEnumerable Run()
        {
            if (!PowerShell.ShouldProcess(Project, $"Create Git repository '{Repository}'")) yield break;

            var client = GetClient<GitHttpClient>();

            var tpRef = new TeamProjectReference
            {
                Id = Project.Id,
                Name = Project.Name
            };

            var repoToCreate = new GitRepository
            {
                Name = Repository,
                ProjectReference = tpRef
            };

            yield return client.CreateRepositoryAsync(repoToCreate, Project.Name)
                .GetResult("Error creating Git repository");
        }
    }
}