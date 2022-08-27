using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Controllers.Git.Branch
{
    [CmdletController(typeof(GitBranchStats))]
    partial class GetGitBranchController 
    {
        protected override IEnumerable Run()
        {
            var repo = GetItem<GitRepository>(new { Repository = Has_Repository? Repository: Project.Name });

            if (repo.Size == 0)
            {
                Logger.Log($"Repository {repo.Name} is empty.");
                yield break;
            }

            var client = GetClient<GitHttpClient>();
            string branchName = null;

            foreach(var branch in Branch)
            {
                switch (branch)
                {
                    case GitBranchStats gbs:
                    {
                        yield return gbs;
                        continue;
                    }
                    case null:
                    case string s when Default:
                    {
                        if (repo.DefaultBranch == null)
                        {
                            throw new Exception($"Repository {repo.Name} does not have a default branch set.");
                        }

                        branchName = repo.DefaultBranch.Substring("refs/heads/".Length);
                        break;
                    }
                    case string s when !string.IsNullOrEmpty(s):
                    {
                        branchName = s;
                        break;
                    }
                    default:
                    {
                        Logger.LogError(new ArgumentException($"Invalid branch '{branch}'", "Branch"));
                        break;
                    }
                }

                IEnumerable<GitBranchStats> result;

                try
                {
                    result = client.GetBranchesAsync(repo.ProjectReference.Name, repo.Id)
                        .GetResult($"Error retrieving branch(es) '{branch}' from repository '{repo.Name}'")
                        .Where(b => b.Name.IsLike(branchName));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException?.Message?.StartsWith("VS403403") ?? false)
                    {
                        result = new List<GitBranchStats>();
                    }
                    else
                    {
                        Logger.LogError(ex);
                        continue;
                    }
                }

                foreach (var b in result) yield return b;
            }
        }
    }
}