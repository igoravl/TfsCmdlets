using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Controllers.Git.Branch
{
    [CmdletController(typeof(GitBranchStats))]
    partial class RemoveGitBranchController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<GitHttpClient>();

            foreach (var branch in Items)
            {
                var commitUrl = new Uri(branch.Commit.Url);
                var repoId = commitUrl.Segments[commitUrl.Segments.Length - 3].Trim('/');
                var projectId = commitUrl.Segments[commitUrl.Segments.Length - 7].Trim('/');
                var repo = GetItem<GitRepository>(new { Repository = repoId, Project = projectId });

                if (!PowerShell.ShouldProcess($"[Project: {repo.ProjectReference.Name}]/[Repository: {repo.Name}]/[Branch: {branch.Name}]", $"Delete branch")) continue;

                try
                {
                    client.UpdateRefsAsync(new[]{new GitRefUpdate()
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