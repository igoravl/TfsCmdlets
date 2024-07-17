using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Branch
{
    /// <summary>
    /// Gets information from one or more branches in a remote Git repository.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, NoAutoPipeline = true, DefaultParameterSetName = "Get by name", 
     OutputType = typeof(GitBranchStats))]
    partial class GetGitBranch
    {
        /// <summary>
        /// Specifies the name of a branch in the supplied Git repository. Wildcards are supported. 
        /// When omitted, all branches are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by name")]
        [ValidateNotNullOrEmpty]
        [Alias("RefName")]
        [SupportsWildcards()]
        public object Branch { get; set; } = "*";

        /// <summary>
        /// Returns the default branch in the given repository.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get default")]
        public SwitchParameter Default { get; set; }

        /// <summary>
        /// HELP_PARAM_GIT_REPOSITORY
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Repository { get; set; }
    }

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