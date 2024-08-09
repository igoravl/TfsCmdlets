using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Branch
{
    /// <summary>
    /// Removes from one or more branches from a remote Git repository.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(GitBranchStats), SupportsShouldProcess = true)]
    partial class RemoveGitBranch
    {
        /// <summary>
        /// Specifies the name of a branch in the supplied Git repository. Wildcards are supported. 
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        [Alias("RefName")]
        [SupportsWildcards()]
        public object Branch { get; set; }

        /// <summary>
        /// HELP_PARAM_GIT_REPOSITORY
        /// </summary>
        [Parameter()]
        public object Repository { get; set; }
    }

    [CmdletController(typeof(GitBranchStats), Client=typeof(IGitHttpClient))]
    partial class RemoveGitBranchController
    {
        protected override IEnumerable Run()
        {
            foreach (var branch in Items)
            {
                var commitUrl = new Uri(branch.Commit.Url);
                var repoId = commitUrl.Segments[commitUrl.Segments.Length - 3].Trim('/');
                var projectId = commitUrl.Segments[commitUrl.Segments.Length - 7].Trim('/');
                var repo = GetItem<GitRepository>(new { Repository = repoId, Project = projectId });

                if (!PowerShell.ShouldProcess($"[Project: {repo.ProjectReference.Name}]/[Repository: {repo.Name}]/[Branch: {branch.Name}]", $"Delete branch")) continue;

                try
                {
                    Client.UpdateRefsAsync(new[]{new GitRefUpdate()
                    {
                        Name = $"refs/heads/{branch.Name}",
                        OldObjectId = branch.Commit.CommitId,
                        NewObjectId = "0000000000000000000000000000000000000000",
                    }}, repositoryId: repoId, projectId: projectId)
                    .Wait($"Error deleting branch '{branch.Name}' from Git repository '{repo.Name}'");
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }

            return null;
        }
    }
}