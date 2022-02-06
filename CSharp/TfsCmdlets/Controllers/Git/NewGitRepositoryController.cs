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


            if (Has_ForkFrom)
            {
                string forkFromRepo;
                string forkFromProject;

                switch(ForkFrom)
                {
                    case string s when s.Contains("/"):
                        forkFromRepo = s.Split('/')[1];
                        forkFromProject = s.Split('/')[0];
                        break;
                    case string s:
                        forkFromRepo = s;
                        forkFromProject = Project.Name;
                        break;
                    case GitRepository r:
                        forkFromRepo = r.Name;
                        forkFromProject = r.ProjectReference.Name;
                        break;
                    default:
                        throw new ArgumentException($"Invalid or non-existent source repository {ForkFrom}");
                }

                string sourceRef = null;
                var parentRepo = GetItem<GitRepository>(new { Repository = forkFromRepo, Project = forkFromProject });
                var parentRepoRef = new GitRepositoryRef
                {
                    Id = parentRepo.Id,
                    ProjectReference = parentRepo.ProjectReference
                };

                if (Has_SourceBranch)
                {
                    sourceRef = SourceBranch.Trim('/');

                    if (!sourceRef.StartsWith("refs/heads/"))
                    {
                        sourceRef = $"refs/heads/{sourceRef}";
                    }
                }

                var createOptions = new GitRepositoryCreateOptions
                {
                    Name = Repository,
                    ProjectReference = tpRef,
                    ParentRepository = parentRepoRef
                };

                yield return client.CreateRepositoryAsync(createOptions, Project.Name, sourceRef: sourceRef)
                    .GetResult("Error forking Git repository");

                yield break;
            }

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