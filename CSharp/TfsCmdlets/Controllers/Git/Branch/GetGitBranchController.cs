using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Controllers.Git.Branch
{
    [CmdletController(typeof(GitBranchStats))]
    partial class GetGitBranchController 
    {
        public override IEnumerable<GitBranchStats> Invoke()
        {

            var branch = Parameters.Get<object>("Branch");
            var defaultBranch = Parameters.Get<bool>("Default");
            var repo = Data.GetItem<GitRepository>();

            if (repo.Size == 0)
            {
                Logger.Log($"Repository {repo.Name} is empty. Skipping.");
                yield break;
            }

            var client = Data.GetClient<GitHttpClient>();
            string branchName = null;
            var done = false;

            while (!done) switch (branch)
            {
                case string s when defaultBranch:
                case null:
                {
                    if (repo.DefaultBranch == null)
                    {
                        throw new Exception($"Repository {repo.Name} does not have a default branch set.");
                    }

                    branchName = repo.DefaultBranch.Substring("refs/heads/".Length);
                    done = true;
                    break;
                }
                case string s when string.IsNullOrEmpty(s):
                {
                    throw new ArgumentNullException("Branch", "Branch argument is required.");
                }
                case GitBranchStats gbs:
                {
                    branchName = gbs.Name;
                    done = true;
                    break;
                }
                case string s:
                {
                    branchName = s;
                    done = true;
                    break;
                }
                default:
                {
                    throw new ArgumentException($"Invalid branch '{branch.ToString()}'", "Branch");
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
                    throw;
                }
            }

            foreach (var b in result)
            {
                yield return b;
            }
        }
    }
}