using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Creates a new Git repository in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GitRepository))]
    partial class NewGitRepository
    {
        /// <summary>
        /// Specifies the name of the new repository
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string Repository { get; set; }

        /// <summary>
        /// Forks the specified reposity. To fork a repository from another team project, 
        /// specify the repository name in the form "project/repository" or pass in the result of a 
        /// previous call to Get-TfsGitRepository that returns the source repository.
        /// </summary>
        [Parameter()]
        public object ForkFrom { get; set; }

        /// <summary>
        /// Forks the specified branch in the source repository. When omitted, forks all branches.
        /// </summary>
        [Parameter()]
        public string SourceBranch { get; set; }
    }

    [CmdletController(typeof(GitRepository), Client=typeof(IGitHttpClient))]
    partial class NewGitRepositoryController
    {
        protected override IEnumerable Run()
        {
            if (!PowerShell.ShouldProcess(Project, $"Create Git repository '{Repository}'")) yield break;

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

                yield return Client.CreateRepositoryAsync(createOptions, Project.Name, sourceRef: sourceRef)
                    .GetResult("Error forking Git repository");

                yield break;
            }

            var repoToCreate = new GitRepository
            {
                Name = Repository,
                ProjectReference = tpRef
            };

            yield return Client.CreateRepositoryAsync(repoToCreate, Project.Name)
                .GetResult("Error creating Git repository");
        }
    }
}
